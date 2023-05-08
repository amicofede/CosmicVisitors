using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Laser", menuName = "Scriptable Object/Laser/Base Laser")]
public class LaserSO : ScriptableObject
{
    public Sprite itemSprite;

    public float Speed;
    public int Damage;
    public Vector2 Direction;

    public bool canBounce;
    public bool canSplit;
    public bool canOneShoot;
}
