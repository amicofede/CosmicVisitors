using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EventController : Utility.MonoSingleton<EventController>
{
    public static event Action VisitorKilled;
    public static event Action<bool> VisitorHitBounds;
    public static event Action BuildVisitorArmy;

    public static event Action BossSpawn;
    public static event Action BossDeath;
    public static event Action BossLaserPhaseTwo;
    public static event Action BossSolarBeamCharge;
    public static event Action BossSolarBeamShoot;

    public static event Action SpaceshipSpawn;
    public static event Action SpaceshipDisableInput;
    public static event Action SpaceshipEnableInput;
    public static event Action SpaceshipShooted;
    public static event Action<InputAction.CallbackContext> SpaceshipMoveStarted;
    public static event Action<InputAction.CallbackContext> SpaceshipMoveFinished;
    public static event Action<InputAction.CallbackContext> SpaceshipShoot;
    public static event Action<InputAction.CallbackContext> SpaceshipShield;


    public static event Action<int> ScoreChanged;
    public static event Action<int> LivesChanged;


    public static event Action GameStartUI;
    public static event Action ResumeGameUI;
    public static event Action GameOverUI;
    public static event Action PauseGameUI;
    public static event Action RestartGameUI;
    public static event Action StageCompleteUI;
    public static event Action PlayingUI;

    public static event Action GenerateStage;
    public static event Action StageComplete;
    public static event Action ClearStage;
    public static event Action SetStage;


    #region Visitor
    public static void RaiseOnVisitorKilled()
    {
        VisitorKilled?.Invoke();
    }

    public static void RaiseOnBuildVisitorArmy()
    {
        BuildVisitorArmy?.Invoke();
    }
    public static void RaiseOnVisitorHitBounds(bool _hitBounds)
    {
        VisitorHitBounds?.Invoke(_hitBounds);
    }
    #endregion

    #region Boss
    public static void RaiseOnBossSpawn()
    {
        BossSpawn?.Invoke();
    }

    public static void RaiseOnBossDeath()
    {
        BossDeath?.Invoke();
    }

    public static void RaiseOnBossLaserPhaseTwo()
    {
        BossLaserPhaseTwo?.Invoke();
    }
    public static void RaiseOnBossSolarBeamCharge()
    {
        BossSolarBeamCharge?.Invoke();
    }
    public static void RaiseOnBossSolarBeamShoot()
    {
        BossSolarBeamShoot?.Invoke();
    }

    #endregion

    #region SpaceShip
    public static void RaiseOnSpaceshipEnableInput()
    {
        SpaceshipEnableInput?.Invoke();
    }

    public static void RaiseOnSpaceshipDisableInput()
    {
        SpaceshipDisableInput?.Invoke();
    }
    public static void RaiseOnSpaceshipSpawn()
    {
        SpaceshipSpawn?.Invoke();
    }
    public static void RaiseOnSpaceshipShooted()
    {
        SpaceshipShooted?.Invoke();
    }


    public static void RaiseOnSpaceshipMoveStarted(InputAction.CallbackContext _context)
    {
        SpaceshipMoveStarted?.Invoke(_context);
    }
    public static void RaiseOnSpaceshipMoveFinished(InputAction.CallbackContext _context)
    {
        SpaceshipMoveFinished?.Invoke(_context);
    }
    public static void RaiseOnSpaceshipShoot(InputAction.CallbackContext _context)
    {
        SpaceshipShoot?.Invoke(_context);
    }
    public static void RaiseOnSpaceshipShield(InputAction.CallbackContext _context)
    {
        SpaceshipShield?.Invoke(_context);
    }
    #endregion

    #region UI
    public static void RaiseOnScoreChanged(int _score)
    {
        ScoreChanged?.Invoke(_score);
    }
    public static void RaiseOnLivesChanged(int _lives)
    {
        LivesChanged?.Invoke(_lives);
    }
    #endregion

    #region Game Flow
    public static void RaiseOnGameStartUI()
    {
        GameStartUI?.Invoke();
    }
    public static void RaiseOnResumeGameUI()
    {
        ResumeGameUI?.Invoke();
    }
    public static void RaiseOnGameOverUI()
    {
        GameOverUI?.Invoke();
    }
    public static void RaiseOnPauseGameUI()
    {
        PauseGameUI?.Invoke();
    }
    public static void RaiseOnRestartGameUI()
    {
        RestartGameUI?.Invoke();
    }
    public static void RaiseOnStageCompleteUI()
    {
        StageCompleteUI?.Invoke();
    }
    public static void RaiseOnPlayingUI()
    {
        PlayingUI?.Invoke();
    }
    #endregion

    #region StageGeneration
    public static void RaiseOnGenerateStage()
    {
        GenerateStage?.Invoke();
    }
    public static void RaiseOnClearStage()
    {
        ClearStage?.Invoke();
    }

    public static void RaiseOnStageComplete()
    {
        StageComplete?.Invoke();
    }
    public static void RaiseOnSetStage()
    {
        SetStage?.Invoke();
    }
    #endregion
}
