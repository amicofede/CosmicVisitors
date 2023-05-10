using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Visitor", menuName = "Scriptable Object/Visitor/Base Visitor")]
public class VisitorSO : ScriptableObject
{
    public int initialLifePoint;

    public Sprite itemSprite;

    public GameObject ReturnFirePrefab;
}
