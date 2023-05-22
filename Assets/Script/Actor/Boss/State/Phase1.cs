//using UnityEngine;

//public class Phase1 : IState
//{
//    private int speed;
//    private int fireRate;

//    private Transform cannonDX;
//    private Transform cannonSX;

//    private float ShootCD;

//    public EnemyAISM Boss { get; set; }

//    public Phase1 (int _speed, int _fireRate, Transform _cannonDx, Transform _cannoSx)
//    {
//        this.speed = _speed;
//        this.fireRate = _fireRate;
//    }

//    public void EnterState(EnemyAISM _boss)
//    {
//        Boss = _boss;
//        Boss.MoveHorizontal();
//        Boss.SimpleShoot();
//    }

//    public void ExitState()
//    {
//    }

//    public void UpdateState()
//    {
//    }

//    //private IEnumerator MoveAnimation()
//    //{
//    //    yield return new WaitForSeconds(1);
//    //    while (!ChangePhase())
//    //    {
//    //        if (boundsHitted)
//    //        {
//    //            rigidBody.MovePosition(Boss.gameObject.transform.position + (Vector3)Vector2.left * speed * Time.fixedDeltaTime);
//    //        }
//    //        else
//    //        {
//    //            rigidBody.MovePosition(Boss.gameObject.transform.position + (Vector3)Vector2.right * speed * Time.fixedDeltaTime);
//    //        }
//    //        yield return new WaitForFixedUpdate();
//    //    }
//    //}

//    //private IEnumerator ShootPhase1()
//    //{
//    //    while (!ChangePhase())
//    //    {
//    //        yield return new WaitForSeconds(1f);
//    //        GameObject BossLaserShooted1;
//    //        GameObject BossLaserShooted2;
//    //        BossLaserShooted1 = Factory.Instance.activateBossLaser();
//    //        BossLaserShooted2 = Factory.Instance.activateBossLaser();
//    //        BossLaserShooted1.SetActive(true);
//    //        BossLaserShooted2.SetActive(true);
//    //        BossLaserShooted1.transform.position = cannonSx.transform.position;
//    //        BossLaserShooted2.transform.position = cannonDx.transform.position;
//    //    }
//    //    yield return null;
//    //}
//}
