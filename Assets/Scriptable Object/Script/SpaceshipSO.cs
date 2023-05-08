using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "New Spaceship", menuName = "Scriptable Object/Spaceship/Base Spaceship")]
public class SpaceshipSO : ScriptableObject
{
    
    public float Speed;

    public Sprite itemSprite;
    public InputAction moveAction;
    public InputAction shootAction;
    public RuntimeAnimatorController moveAnimator;

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
