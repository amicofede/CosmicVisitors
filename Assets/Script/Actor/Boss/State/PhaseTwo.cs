using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseTwo : IState
{
    private BossAISM2 boss;

    private Vector3 nextOrbitPosition;

    private Transform orbitCannon;
    private Spaceship spaceship;

    private bool isOrbitShooting;
    private int orbitLaserCount;
    private float orbitSpawnTimer;


    private List<GameObject> orbitLasers = new List<GameObject>();

    public PhaseTwo(BossAISM2 _boss, Transform _orbitCannon, Spaceship _spaceship)
    {
        boss = _boss;
        orbitCannon = _orbitCannon;
        spaceship = _spaceship;
    }
    public void OnEnter()
    {
        isOrbitShooting = false;
        orbitLaserCount = 0;
        orbitSpawnTimer = 0;
        nextOrbitPosition = new Vector3(Random.Range(-3f, 3f), 13f, 0f);
    }

    public void OnExit()
    {
        isOrbitShooting = false;
        orbitLaserCount = 0;
        for (int i = 0; i < orbitLasers.Count; i++)
        {
            Debug.Log("removed: " + orbitLasers[i] + "_" + i);
            Factory.Instance.deactiveBossLaserOrbit(orbitLasers[i]);
            orbitLasers.Remove(orbitLasers[i]);
        }
    }

    public void Tick()
    {
        if (!isOrbitShooting)
        {
            QuickMove();
        }
        else
        {
            OrbitSpawn();
        }
        if (orbitLaserCount == 3)
        {
            OrbitMove();
        }
    }

    public void QuickMove()
    {
        boss.transform.position = Vector2.MoveTowards(boss.transform.position, nextOrbitPosition, 20 * Time.deltaTime);
        if (boss.transform.position == nextOrbitPosition)
        {
            nextOrbitPosition = new Vector3(Random.Range(-3f, 3f), 13f, 0f);
            isOrbitShooting = true;
        }
    }

    public void OrbitSpawn()
    {
        if (orbitLaserCount < 3 && orbitSpawnTimer >= 0.5f)
        {
            orbitSpawnTimer = 0;
            orbitLaserCount++;
            GameObject BossLaserOrbit;
            BossLaserOrbit = Factory.Instance.activateBossLaserOrbit();
            BossLaserOrbit.SetActive(true);
            orbitLasers.Add(BossLaserOrbit);
            BossLaserOrbit.transform.position = new Vector3(orbitCannon.transform.position.x + orbitLaserCount, orbitCannon.transform.position.y, orbitCannon.transform.position.z);
        }
        else
        {
            orbitSpawnTimer += Time.deltaTime;
        }
    }

    private void OrbitMove()
    {
        float spaceshipX = spaceship.gameObject.transform.position.x;
        float spaceshipY = -3;
        float spaceshipZ = spaceship.gameObject.transform.position.z;
        for (int i = 0; i < orbitLasers.Count; i++)
        {
            orbitLasers[i].transform.position = Vector2.MoveTowards(orbitLasers[i].transform.position,
                                                                    new Vector3(spaceshipX, spaceshipY, spaceshipZ),
                                                                    10 * Time.deltaTime);
            if (!orbitLasers[i].activeSelf)
            {
                orbitLasers.Remove(orbitLasers[i]);
                if (orbitLasers.Count == 0)
                {
                    isOrbitShooting = false;
                    orbitLaserCount = 0;
                }
            }
        }
    }
    //{
    //    private Vector3 nextOrbitPosition;

    //    public EnemyAISM Boss { get; set; }

    //    public Phase2(Vector3 _nextOrbitPosition)
    //    {
    //        this.nextOrbitPosition = _nextOrbitPosition;
    //    }

    //    public void EnterState(EnemyAISM _boss)
    //    {
    //        Boss = _boss;
    //    }

    //    public void ExitState()
    //    {
    //    }

    //    public void UpdateState()
    //    {
    //    }
}
