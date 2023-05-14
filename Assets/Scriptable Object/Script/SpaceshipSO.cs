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

    public GameObject LaserPrefab;
}
