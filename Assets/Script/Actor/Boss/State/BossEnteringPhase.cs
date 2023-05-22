//using Unity.VisualScripting;
//using UnityEngine;

//public class BossEnteringPhase : IState
//{
//    private Vector3 playingPosition;
//    private Vector3 currentPosition;

//    public EnemyAISM Boss { get; set; }

//    public BossEnteringPhase(Vector3 _currentPosition, Vector3 _playingPosition)
//    {
//        this.currentPosition = _currentPosition;
//        this.playingPosition = _playingPosition;
//    }

//    public void EnterState(EnemyAISM _boss)
//    {
//        Boss = _boss;
//        Boss.transform.position = currentPosition;
//        Boss.transform.rotation = Quaternion.Euler(0f, 0f, -90f);
//    }

//    public void ExitState()
//    {
//    }

//    public void UpdateState()
//    {
//        if (currentPosition.y >= playingPosition.y)
//        {
//            currentPosition = Boss.transform.position;
//            Boss.transform.position = Vector2.MoveTowards(currentPosition, playingPosition, 1 * Time.deltaTime);
//        }
//        else
//        {
//            //Boss.ChangeState(new Phase1(Boss.Speed, Boss.FireRate));
//            EventController.RaiseOnSpaceshipEnableInput();
//        }
//    }
//}
