using System.Collections.Generic;
using UnityEngine;

public class Factory : Utility.MonoSingleton<Factory>
{
    [SerializeField] private GameObject laserPrefab;
    [Header("")]
    [SerializeField] private GameObject bossLaserPrefab;
    [SerializeField] private GameObject bossLaserOrbitPrefab;
    [Header("")]
    [SerializeField] private GameObject returnFireFighterPrefab;
    [SerializeField] private GameObject visitorFighterPrefab;
    [Header("")]
    [SerializeField] private GameObject visitorBomberPrefab;
    [SerializeField] private GameObject returnFireBomberPrefab;

    private Utility.ObjectPool lasers;
    private Utility.ObjectPool bossLasers;
    private Utility.ObjectPool bossLasersOrbit;
    private Utility.ObjectPool returnFiresFighter;
    private Utility.ObjectPool returnFiresBomber;
    private Utility.ObjectPool visitorsFighter;
    private Utility.ObjectPool visitorsBomber;

    #region UnityMessages
    private new void Awake()
    {
        base.Awake();

        GameObject lasersPool = new GameObject("LasersPool");
        lasersPool.transform.SetParent(gameObject.transform);
        lasers = new Utility.ObjectPool(laserPrefab, lasersPool.gameObject.transform, 10);

        GameObject bossLasersPool = new GameObject("BossLasersPool");
        bossLasersPool.transform.SetParent(gameObject.transform);
        bossLasers = new Utility.ObjectPool(bossLaserPrefab, bossLasersPool.gameObject.transform, 30);

        GameObject bossLasersOrbitPool = new GameObject("BossLasersOrbitPool");
        bossLasersOrbitPool.transform.SetParent(gameObject.transform);
        bossLasersOrbit = new Utility.ObjectPool(bossLaserOrbitPrefab, bossLasersOrbitPool.gameObject.transform, 9);

        GameObject visitorsFighterPool = new GameObject("VisitorsFighterPool");
        visitorsFighterPool.transform.SetParent(gameObject.transform);
        visitorsFighter = new Utility.ObjectPool(visitorFighterPrefab, visitorsFighterPool.gameObject.transform, 20);

        GameObject returnFiresFighterPool = new GameObject("ReturnsFiresFighterPool");
        returnFiresFighterPool.transform.SetParent(gameObject.transform);
        returnFiresFighter = new Utility.ObjectPool(returnFireFighterPrefab, returnFiresFighterPool.gameObject.transform, 100);

        GameObject visitorsBomberPool = new GameObject("VisitorsBomberPool");
        visitorsBomberPool.transform.SetParent(gameObject.transform);
        visitorsBomber = new Utility.ObjectPool(visitorBomberPrefab, visitorsBomberPool.gameObject.transform, 49);

        GameObject returnFiresBomberPool = new GameObject("ReturnsFiresBomberPool");
        returnFiresBomberPool.transform.SetParent(gameObject.transform);
        returnFiresBomber = new Utility.ObjectPool(returnFireBomberPrefab, returnFiresBomberPool.gameObject.transform, 100);
    }
    #endregion

    #region Active Object
    public GameObject activateLaser()
    {
        return lasers.ActivateObject();
    }
    public GameObject activateBossLaser()
    {
        return bossLasers.ActivateObject();
    }
    public GameObject activateBossLaserOrbit()
    {
        return bossLasersOrbit.ActivateObject();
    }

    public GameObject activateReturnFireFighter()
    {
        return returnFiresFighter.ActivateObject();
    }
    public GameObject activateReturnFireBomber()
    {
        return returnFiresBomber.ActivateObject();
    }

    //public GameObject activateVisitorFighter()
    //{
    //    return visitorsFighter.ActivateObject();
    //}
    //public GameObject activateVisitorBomber()
    //{
    //    return visitorsBomber.ActivateObject();
    //}

    public GameObject ActivateRandomVisitors()
    {
        if (Random.Range(1, 100) <= (20 * StageController.Instance.Level))
        {
            return visitorsBomber.ActivateObject();
        }
        else
        {
            return visitorsFighter.ActivateObject();
        }
    }
    #endregion

    #region Deactive Object
    public void deactiveLaser(GameObject _obj)
    {
        lasers.DeactiveObject(_obj);
    }
    public void deactiveBossLaser(GameObject _obj)
    {
        bossLasers.DeactiveObject(_obj);
    }
    public void deactiveBossLaserOrbit(GameObject _obj)
    {
        bossLasersOrbit.DeactiveObject(_obj);
    }
    public void deactiveReturnFireFighter(GameObject _obj)
    {
        returnFiresFighter.DeactiveObject(_obj);
    }
    public void deactiveReturnFireBomber(GameObject _obj)
    {
        returnFiresBomber.DeactiveObject(_obj);
    }
    public void deactiveVisitorFighter(GameObject _obj)
    {
        visitorsFighter.DeactiveObject(_obj);
    }
    public void deactiveVisitorBomber(GameObject _obj)
    {
        visitorsBomber.DeactiveObject(_obj);
    }
    #endregion
}
