using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PhaseTwo : IState
{
    private BossAISM boss;

    private Vector3 playingPosition;

    private Vector3 nextOrbitPosition;
    private int randomPosition;

    private Transform orbitCannon;

    private bool isOrbitShooting;
    private int orbitLaserInGame;
    private float orbitSpawnTimer;


    private List<Laser> orbitLasers = new List<Laser>();

    public PhaseTwo(BossAISM _boss, Vector3 _playingPosition, Transform _orbitCannon, Spaceship _spaceship)
    {
        boss = _boss;
        orbitCannon = _orbitCannon;
        playingPosition = _playingPosition;
    }
    public void OnEnter()
    {
        boss.gameObject.transform.position = playingPosition;
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
