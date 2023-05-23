using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PhaseTwo : IState
{
    private BossAISM2 boss;

    private Vector3 nextOrbitPosition;
    private int randomPosition;
    private Vector2 spaceshipPosition;
    private Vector2 laserCurrentPosition;

    private Transform orbitCannon;
    private Spaceship spaceship;

    private bool isOrbitShooting;
    private int orbitLaserInGame;
    private float orbitSpawnTimer;


    private List<Laser> orbitLasers = new List<Laser>();

    public PhaseTwo(BossAISM2 _boss, Transform _orbitCannon, Spaceship _spaceship)
    {
        boss = _boss;
        orbitCannon = _orbitCannon;
        spaceship = _spaceship;
    }
    public void OnEnter()
    {
        isOrbitShooting = false;
        orbitLaserInGame = 0;
        orbitSpawnTimer = 0;
        randomPosition = Random.Range(0, 1) * 2 - 1;
        nextOrbitPosition = new Vector3(Random.Range(-3f, 3f), 13f, 0f);
    }

    public void OnExit()
    {
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
        if (orbitLaserInGame == 3)
        {
            OrbitDespawn();
        }
    }

    public void QuickMove()
    {
        boss.transform.position = Vector2.MoveTowards(boss.transform.position, nextOrbitPosition, 20 * Time.deltaTime);
        if (boss.transform.position == nextOrbitPosition)
        {
            isOrbitShooting = true;
            if (randomPosition < 0)
            {
                randomPosition = 1;
            }
            else
            {
                randomPosition = -1;
            }
            nextOrbitPosition = new Vector3(randomPosition * Random.Range(0, 3f), 13f, 0f);
        }
    }

    public void OrbitSpawn()
    {
        if (orbitLaserInGame < 3 && orbitSpawnTimer >= 0.5f)
        {
            orbitSpawnTimer = 0;
            orbitLaserInGame++;
            GameObject BossLaserOrbit;
            BossLaserOrbit = Factory.Instance.activateBossLaserOrbit();
            BossLaserOrbit.transform.position = new Vector3(orbitCannon.transform.position.x + orbitLaserInGame, orbitCannon.transform.position.y, orbitCannon.transform.position.z);
            orbitLasers.Add(BossLaserOrbit.GetComponent<Laser>());
            BossLaserOrbit.SetActive(true);
        }
        else
        {
            orbitSpawnTimer += Time.deltaTime;
        }
    }
    private void OrbitDespawn()
    {
        for (int i = 0; i < orbitLasers.Count; i++)
        {
            if (!orbitLasers[i].gameObject.activeSelf)
            {
                orbitLasers.RemoveAt(i);
            }
        }
        if (orbitLasers.Count == 0)
        {
            orbitLaserInGame = 0;
            isOrbitShooting = false;
        }
    }
}
//            //if (orbitLasers[i].transform.position.y > 4)
//            //{
//            //    Debug.Log("Position_" + i + "_" + orbitLasersPosition[i]);
//            //    spaceshipPosition = spaceship.transform.position;
//            //    Vector2 movement = (spaceshipPosition - orbitLasersPosition[i]).normalized;
//            //    orbitLasersDirection.Add(movement);
//            //    Debug.Log("Direction_" + i + "_" + orbitLasersDirection[i]);
//            //    orbitLasers[i].gameObject.GetComponent<Rigidbody2D>().MovePosition(orbitLasersPosition[i] + orbitLasersDirection[i] * 10 * Time.fixedDeltaTime);
//            //}
//            //else
//            //{
//            //    orbitLasers[i].gameObject.GetComponent<Rigidbody2D>().MovePosition(orbitLasersPosition[i] + orbitLasersDirection[i] * 10 * Time.fixedDeltaTime);
//            //}
//            //Vector2 laserCurrentPosition = orbitLasers[i].gameObject.transform.position;
//            //Vector2 spaceshipPosition = spaceship.transform.position;
//            //Vector2 movement = (spaceshipPosition - laserCurrentPosition).normalized;
//            //float orbitMoveSpeed = 10 * Time.fixedDeltaTime;
//            //if (orbitLasers[i].transform.position.y > 4)
//            //{
//            //}
//            //rigidBody2D.MovePosition((Vector2)currentPosition + movement * 1 * Time.fixedDeltaTime);


//            //        orbitLasers[i].transform.position = Vector2.MoveTowards(orbitLasers[i].transform.position, spaceshipPosition,
//            //                                                                10 * Time.deltaTime);
//            //        if (orbitLasers[i].transform.position.y < -1)
//            //        {
//            //            orbitLasers[i].transform.position = Vector2.MoveTowards(orbitLasers[i].transform.position,
//            //                                                                    new Vector3(spaceshipX, spaceshipY - 10, spaceshipZ),
//            //                                                                    10 * Time.deltaTime);

//            //            orbitLasers.Remove(orbitLasers[i]);
//            //            if (orbitLasers.Count == 0)
//            //            {
//            //                isOrbitShooting = false;
//            //                orbitLaserCount = 0;
//            //            }
//            //        }
//            //    }
//            //}
//            //{
//            //    private Vector3 nextOrbitPosition;

//            //    public EnemyAISM Boss { get; set; }

//            //    public Phase2(Vector3 _nextOrbitPosition)
//            //    {
//            //        this.nextOrbitPosition = _nextOrbitPosition;
//            //    }

//            //    public void EnterState(EnemyAISM _boss)
//            //    {
//            //        Boss = _boss;
//            //    }

//            //    public void ExitState()
//            //    {
//            //    }

//            //    public void UpdateState()
//            //    {
//            //    }
//            //}
//        }
//}
