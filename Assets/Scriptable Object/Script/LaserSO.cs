using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Laser", menuName = "Scriptable Object/Laser/Base Laser")]
public class LaserSO : ScriptableObject
{
    public Sprite itemSprite;

    public float Speed;
    public Vector2 Direction;

    public shootType type;

    //public bool canBounce;
    //public bool canSplit;
    //public bool canOneShoot;


    public enum shootType
    {
        BossLaser,
        Laser,
        ReturnFireFighter,
        ReturnFireBomber,
        BossLaserOrbit
    }

    public void ReturnToFactory(GameObject _obj)
    {
        switch (type)
        {
            case shootType.Laser:
                Factory.Instance.deactiveLaser(_obj);
                break;
            case shootType.ReturnFireFighter:
                Factory.Instance.deactiveReturnFireFighter(_obj);
                break;
            case shootType.ReturnFireBomber:
                Factory.Instance.deactiveReturnFireBomber(_obj);
                break;
            case shootType.BossLaser:
                Factory.Instance.deactiveBossLaser(_obj);
                break;
            case shootType.BossLaserOrbit:
                Factory.Instance.deactiveBossLaser(_obj);
                break;
        }
    }
}
