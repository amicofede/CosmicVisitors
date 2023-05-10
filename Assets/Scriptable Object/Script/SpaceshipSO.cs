using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "New Spaceship", menuName = "Scriptable Object/Spaceship/Base Spaceship")]
public class SpaceshipSO : ScriptableObject
{
    public int initialLifePoint;
    public float Speed;

    public Sprite itemSprite;
    public InputAction moveAction;
    public InputAction shootAction;

    public GameObject LaserPrefab;

    #region UnityMessages
    private void OnEnable()
    {
        EnableInputs();
    }

    private void OnDisable()
    {
        DisableInputs();
    }

    #endregion

    public void EnableInputs()
    {
        moveAction.Enable();
        shootAction.Enable();
    }
    public void DisableInputs()
    {
        moveAction.Disable();
        shootAction.Disable();
    }

}
