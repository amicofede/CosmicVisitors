using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseOne : IState
{
    private BossAISM2 boss;

    private Transform cannonDx;
    private Transform cannonSx;


    private Vector3 startPosition;
    private Vector3 currentPosition;
    private Vector3 playingPosition;

    private float speed;
    private float timeBetweenShoot;
    private float shootCD;

    private Rigidbody2D rigidBody2D;

    public PhaseOne(BossAISM2 _boss, Rigidbody2D _rigidBody2D, Vector3 _startPosition, Vector3 _playingPosition, float _speed, float _timeBetweenShoot, Transform _cannonDx, Transform _cannonSx)
    {
        boss = _boss;
        rigidBody2D = _rigidBody2D;
        startPosition = _startPosition;
        playingPosition = _playingPosition;
        speed = _speed;
        timeBetweenShoot = _timeBetweenShoot;
        cannonDx = _cannonDx;
        cannonSx = _cannonSx;
    }
    public void OnEnter()
    {
        rigidBody2D.isKinematic = true;
        shootCD = 0;
        boss.gameObject.transform.position = startPosition;
    }

    public void OnExit()
    {
    }

    public void Tick()
    {
        if (boss.gameObject.transform.position.y > playingPosition.y)
        {
            currentPosition = boss.gameObject.transform.position;
            Vector2 movement = (playingPosition - currentPosition).normalized;
            rigidBody2D.MovePosition((Vector2)currentPosition + movement * 1 * Time.fixedDeltaTime);
        }
        else
        {
            boss.gameObject.transform.position = new Vector3(boss.gameObject.transform.position.x, playingPosition.y, 0f);
            rigidBody2D.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            rigidBody2D.isKinematic = false;
            EventController.RaiseOnSpaceshipEnableInput();
            MoveHorizontal();
            Shoot();
        }
    }


    private void MoveHorizontal()
    {
        if (boss.BoundsHitted)
        {
            rigidBody2D.MovePosition(boss.gameObject.transform.position + (Vector3)Vector2.left * speed * Time.fixedDeltaTime);
        }
        else
        {
            rigidBody2D.MovePosition(boss.gameObject.transform.position + (Vector3)Vector2.right * speed * Time.fixedDeltaTime);
        }
    }

    private void Shoot()
    {
        if (shootCD > timeBetweenShoot)
        {
            shootCD = 0;
            GameObject BossLaserShooted1;
            GameObject BossLaserShooted2;
            BossLaserShooted1 = Factory.Instance.activateBossLaser();
            BossLaserShooted2 = Factory.Instance.activateBossLaser();
            BossLaserShooted1.SetActive(true);
            BossLaserShooted2.SetActive(true);
            BossLaserShooted1.transform.position = cannonSx.transform.position;
            BossLaserShooted2.transform.position = cannonDx.transform.position;
        }
        else
        {
            shootCD += Time.deltaTime;
        }
    }
}
