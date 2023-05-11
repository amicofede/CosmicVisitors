using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    private int score;
    public GameObject playerPrefab;

    public enum State
    {
        None = -1,
        Playing,
        Pause,
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
                case State.None: { EventController.RaiseOnGameStart(); break; }
                case State.Playing: { ResumeGame(); break; }
                case State.Pause: { PauseGame(); break; }
                case State.GameOver: { GameOver(); break; }
            }
        }
    }

    private int Score
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
    }

    private void OnDisable()
    {
        EventController.VisitorKilled -= OnScoreAdded;
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
    public void StartGame()
    {
        Time.timeScale = 1;
        gameState = State.Playing;
        EventController.RaiseOnResumeGame();
        EventController.RaiseOnSpaceshipAnimationStarted();
        StartCoroutine(SpawnPlayer());
    }

    private void GameOver()
    {
        gameState = State.GameOver;
        EventController.RaiseOnGameOver();
    }

    private void PauseGame()
    {
        gameState = State.Pause;
        EventController.RaiseOnPauseGame();
    }

    private void ResumeGame()
    {
        gameState = State.Playing;
        EventController.RaiseOnResumeGame();
    }

    #endregion

    #region Coroutine
    private IEnumerator SpawnPlayer()
        {
            GameObject player = Instantiate(playerPrefab, new Vector3(0f,-1,0f), Quaternion.Euler(0f,0f,90f));
            while (player.transform.position.y <= 1.5f)
                {
                    player.transform.position = Vector2.MoveTowards(player.transform.position, new Vector3(0f, 1.5f, 0f), 3 * Time.deltaTime);
                    yield return null;
                }
            yield return new WaitForSeconds(1);
            EventController.RaiseOnGenerateLevel();
        }
    #endregion


}
