using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Visitor Prototypes", menuName = "Scriptable Object/Visitor Prototype")]
public class VisitorPrototypeSO : ScriptableObject
{
    public List<VisitorSO> VisitorPrototypes;
}
