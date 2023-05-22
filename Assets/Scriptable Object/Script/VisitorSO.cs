using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LaserSO;

[CreateAssetMenu(fileName = "New Visitor", menuName = "Scriptable Object/Visitor/Base Visitor")]
public class VisitorSO : ScriptableObject
{
    public int initialLifePoint;

    public Sprite itemSprite;

    public VisitorType visitorType;

    public enum VisitorType
    {
        Bomber,
        Fighter
    }

    public void ReturnToFactory(GameObject _obj)
    {
        switch (visitorType)
        {
            case VisitorType.Bomber:
                Factory.Instance.deactiveVisitorBomber(_obj);
                break;
            case VisitorType.Fighter:
                Factory.Instance.deactiveVisitorFighter(_obj);
                break;
        }
    }
}
