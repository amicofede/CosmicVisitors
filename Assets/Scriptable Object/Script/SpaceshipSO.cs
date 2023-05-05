using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "New Spaceship", menuName = "Create SO/New Spaceship")]
public class SpaceshipSO : ScriptableObject
{
    public float Speed;

    public InputAction moveAction;
    public InputAction shootAction;

    public GameObject LaserPrefab;

    private void OnEnable()
    {
        moveAction.Enable();
        shootAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
        shootAction.Disable();
    }
}
