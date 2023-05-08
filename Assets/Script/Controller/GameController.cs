using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoSingleton<GameController>
{
    private int score;
    public GameObject playerPrefab;

    private int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
            EventController.ScoreChanged(score);
        }
    }

    #region UnityMessages
    private void Awake()
    {
        Instantiate(playerPrefab, new Vector3(0f,1.5f,0f), Quaternion.Euler(0f,0f,90f));
        base.Awake();
        Score = 0;
    }

    private void OnEnable()
    {
        EventController.OnVisitorKilled += OnScoreAdded;
    }

    private void OnDisable()
    {
        EventController.OnVisitorKilled -= OnScoreAdded;
    }
    #endregion

    #region Delegates
    private void OnScoreAdded()
    {
        Score++;
        Debug.Log("Score: " + score);
    }
    #endregion


}
