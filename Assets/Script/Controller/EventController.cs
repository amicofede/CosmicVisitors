using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoSingleton<EventController>
{
    public static event Action VisitorKilled;
    public static event Action VisitorAnimationStarted;
    public static event Action VisitorAnimationFinished;
    public static event Action<bool> VisitorHitBounds;
    public static event Action BuildVisitorArmy;

    public static event Action SpaceshipAnimationStarted;
    public static event Action SpaceshipAnimationFinished;

    public static event Action<int> ScoreChanged;
    public static event Action<int> LivesChanged;

    public static event Action GameStart;
    public static event Action ResumeGame;
    public static event Action GameOver;
    public static event Action PauseGame;
    public static event Action ResetGame;

    public static event Action GenerateLevel;

    #region Visitor
    public static void RaiseOnVisitorKilled()
    {
        VisitorKilled?.Invoke();
    }
    public static void RaiseOnVisitorAnimationStarted()
    {
        VisitorAnimationStarted?.Invoke();
    }
    public static void RaiseOnVisitorAnimationFinished()
    {
        VisitorAnimationFinished?.Invoke();
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

    #region Player
    public static void RaiseOnSpaceshipAnimationStarted()
    {
        SpaceshipAnimationStarted?.Invoke();
    }
    public static void RaiseOnSpaceshipAnimationFinished()
    {
        SpaceshipAnimationFinished?.Invoke();
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
    public static void RaiseOnGameStart()
    {
        GameStart?.Invoke();
    }
    public static void RaiseOnResumeGame()
    {
        ResumeGame?.Invoke();
    }
    public static void RaiseOnGameOver()
    {
        GameOver?.Invoke();
    }
    public static void RaiseOnPauseGame()
    {
        PauseGame?.Invoke();
    }
    public static void RaiseOnResetGame()
    {
        ResetGame?.Invoke();
    }
    #endregion

    #region StageGeneration
    public static void RaiseOnGenerateLevel()
    {
        GenerateLevel?.Invoke();
    }
    #endregion
}
