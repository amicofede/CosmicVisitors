//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Phase2 : IState
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
//        if (!Boss.OrbitShooting)
//        {
//            Boss.transform.position = Vector2.MoveTowards(Boss.transform.position, nextOrbitPosition, 5 * Time.deltaTime);
//            if (Boss.transform.position == nextOrbitPosition)
//            {
//                nextOrbitPosition = new Vector3(Random.Range(-3f, 3f), 13f, 0f);
//                Boss.OnOrbitShoot();
//            }
//        }
//    }
//}
