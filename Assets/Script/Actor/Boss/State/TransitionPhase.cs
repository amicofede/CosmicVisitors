//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class TransitionPhase : IState
//{
//    private Vector3 currentPosition;
//    private Vector3 playingPosition;
//    public EnemyAISM Boss { get; set; }

//    public TransitionPhase(Vector3 _currentPosition, Vector3 _playingPosition)
//    {
//        this.currentPosition = _currentPosition;
//        this.playingPosition = _playingPosition;
//    }

//    public void EnterState(EnemyAISM _boss)
//    {
//        Boss = _boss;
//        Boss.transform.position = currentPosition;
//        Boss.ActivateShield();
//    }

//    public void ExitState()
//    {
//    }

//    public void UpdateState()
//    {
//    }
//}
