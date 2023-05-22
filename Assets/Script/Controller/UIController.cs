using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : Utility.MonoSingleton<UIController>
{
    public GameObject startMenuUI;
    public GameObject playingUI;
    public GameObject gameOverUI;
    public GameObject pauseMenuUI;
    public GameObject stageCompleteUI;

    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private TextMeshProUGUI stageComplete;
    [SerializeField] private TextMeshProUGUI gameOverStatistics;

    [SerializeField] private List<GameObject> LifeIcons = new List<GameObject>();

    #region UnityMessages
    private new void Awake()
    {
        base.Awake();
        StartMenuUI();
    }
    private void OnEnable()
    {
        EventController.ScoreChanged += OnScoreChanged;
        EventController.LivesChanged += OnLivesChanged;
        EventController.RestartGameUI += StartMenuUI;
        EventController.ResumeGameUI += PlayingUI;
        EventController.GameOverUI += GameOverUI;
        EventController.StageCompleteUI += StageCompleteUI;
        EventController.PauseGameUI += PauseMenuUI;
        EventController.RestartGameUI += StartMenuUI;
        EventController.PlayingUI += PlayingUI;
    }

    private void OnDisable()
    {
        EventController.ScoreChanged -= OnScoreChanged;
        EventController.LivesChanged -= OnLivesChanged;
        EventController.RestartGameUI -= StartMenuUI;
        EventController.ResumeGameUI -= PlayingUI;
        EventController.GameOverUI -= GameOverUI;
        EventController.StageCompleteUI -= StageCompleteUI;
        EventController.PauseGameUI -= PauseMenuUI;
        EventController.RestartGameUI -= StartMenuUI;
        EventController.PlayingUI -= PlayingUI;

    }
    #endregion

    #region Delegates
    private void OnScoreChanged(int _score)
    {
        score.text = "Score: " + _score.ToString();
    }
    private void OnLivesChanged(int _lives)
    {
        for (int i = LifeIcons.Count - 1; i >= 0; i--)
        {
            LifeIcons[i].SetActive(_lives >= i);
        }
    }
    #endregion

    #region UI Menù
    private void StartMenuUI()
    {
        startMenuUI.SetActive(true);
        playingUI.SetActive(false);
        gameOverUI.SetActive(false);
        pauseMenuUI.SetActive(false);
        stageCompleteUI.SetActive(false);
    }
    private void PlayingUI()
    {
        startMenuUI.SetActive(false);
        playingUI.SetActive(true);
        gameOverUI.SetActive(false);
        pauseMenuUI.SetActive(false);
        stageCompleteUI.SetActive(false);
    }
    private void GameOverUI()
    {
        startMenuUI.SetActive(false);
        playingUI.SetActive(false);
        gameOverUI.SetActive(true);
        pauseMenuUI.SetActive(false);
        stageCompleteUI.SetActive(false);
        gameOverStatistics.text = "Your reach the Stage n. " + StageController.Instance.Level + "."+
                                  "\nYour total Score is " + GameManager.Instance.Score + ".";

    }
    private void PauseMenuUI()
    {
        startMenuUI.SetActive(false);
        playingUI.SetActive(true);
        gameOverUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        stageCompleteUI.SetActive(false);
    }
    private void StageCompleteUI()
    {
        startMenuUI.SetActive(false);
        playingUI.SetActive(false);
        gameOverUI.SetActive(false);
        pauseMenuUI.SetActive(false);
        stageCompleteUI.SetActive(true);
        stageComplete.text = "Stage " + StageController.Instance.Level + "\nComplete!";
    }
    #endregion
}
