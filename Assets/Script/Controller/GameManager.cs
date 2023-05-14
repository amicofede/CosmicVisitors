using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    private int score;
    public GameObject SpaceshipPrefab;

    public enum State
    {
        None = -1,
        Playing,
        Pause,
        StageComplete,
        GameOver,
    }
    private State gameState;
    public State GameState
    {
        get { return gameState; }
        set
        {
            switch (value)
            {
                case State.None: { EventController.RaiseOnGameStartUI(); break; }
                case State.Playing: { OnResumeGame(); break; }
                case State.Pause: { OnPauseGame(); break; }
                case State.StageComplete: { StageComplete(); break; }
                case State.GameOver: { OnGameOver(); break; }
            }
        }
    }
    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
            EventController.RaiseOnScoreChanged(score);
        }
    }


    #region UnityMessages
    private new void Awake()
    {
        base.Awake();
        Score = 0;
        GameState = State.None;
    }

    private void OnEnable()
    {
        EventController.VisitorKilled += OnScoreAdded;
        EventController.StageCompleteUI += OnStageComplete;
        EventController.PauseGameUI += OnPauseGame;
        EventController.ResumeGameUI += OnResumeGame;
        EventController.GameOverUI += OnGameOver;
    }

    private void OnDisable()
    {
        EventController.VisitorKilled -= OnScoreAdded;
        EventController.StageCompleteUI -= OnStageComplete;
        EventController.PauseGameUI -= OnPauseGame;
        EventController.RestartGameUI -= OnRestartGame;
        EventController.GameOverUI -= OnGameOver;
    }
    #endregion

    #region Delegates
    private void OnScoreAdded()
    {
        Score++;
        Debug.Log("Score: " + score);
    }
    #endregion

    #region Game Flow

    public void SetGameState(State _gameState)
    {
        GameState = _gameState;
    }
    public void StartStage()
    {
        StopAllCoroutines();
        Time.timeScale = 1;
        gameState = State.Playing;
        Debug.Log(gameState);
        GameObject player = Instantiate(SpaceshipPrefab);
        EventController.RaiseOnSpaceshipSpawn();
        EventController.RaiseOnPlayingUI();
    }
    private void OnGameOver()
    {
        Time.timeScale = 0;
        gameState = State.GameOver;
    }
    private void OnPauseGame()
    {
        Time.timeScale = 0;
        gameState = State.Pause;
    }
    private void OnResumeGame()
    {
        Time.timeScale = 1;
        gameState = State.Playing;
    }
    public void OnRestartGame()
    {
        Score = 0;
        StopAllCoroutines();
        EventController.RaiseOnRestartGameUI();
    }
    private void OnStageComplete()
    {
        gameState = State.StageComplete;
    }
    private void StageComplete()
    {
        Time.timeScale = 0;
    }
    #endregion
}
