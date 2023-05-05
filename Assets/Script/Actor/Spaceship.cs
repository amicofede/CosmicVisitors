using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;

public class Spaceship : MonoBehaviour, IDamageable
{
    [SerializeField]
    private SpaceshipSO spaceshipData;

    private IEnumerator moveCoroutine;

    #region UnityMessages
    private void OnEnable()
    {
        spaceshipData.moveAction.started += OnMoveStarted;
        spaceshipData.moveAction.canceled += OnMoveFinished;
        spaceshipData.shootAction.started += OnShoot;
    }

    private void OnDisable()
    {
        spaceshipData.moveAction.started -= OnMoveStarted;
        spaceshipData.moveAction.canceled -= OnMoveFinished;
    }
    #endregion

    #region Movement

    private void OnMoveStarted(InputAction.CallbackContext context)
    {
        moveCoroutine = OnMove(context.ReadValue<float>());
        StartCoroutine(moveCoroutine);
    }

    private void OnMoveFinished(InputAction.CallbackContext context)
    {
        StopCoroutine(moveCoroutine);
        moveCoroutine = null;
    }

    private IEnumerator OnMove(float _input)
    {
        while (true)
        {
            Vector2 direction = Vector2.right * _input;
            float movementMagnitude = spaceshipData.Speed * Time.deltaTime;

            transform.position += (Vector3)direction * movementMagnitude;
            yield return null;
        }
    }
    #endregion

    #region Shoot
    private void OnShoot(InputAction.CallbackContext context)
    {
        GameObject.Instantiate(spaceshipData.LaserPrefab, new Vector3(transform.position.x - 0.29f, transform.position.y + 0.75f, 0f), Quaternion.Euler(0f,0f,90f));
        GameObject.Instantiate(spaceshipData.LaserPrefab, new Vector3(transform.position.x + 0.29f, transform.position.y + 0.75f, 0f), Quaternion.Euler(0f, 0f, 90f));
    }
    #endregion
}
