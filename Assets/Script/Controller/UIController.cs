using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoSingleton<UIController>
{
    public GameObject startMenuUI;
    public GameObject playingUI;
    public GameObject gameOverUI;
    public GameObject pauseMenuUI;
    public GameObject winUI;

    [SerializeField] private TextMeshProUGUI scoreVisitor;
    [SerializeField] private List<GameObject> LifeIcons = new List<GameObject>();

    #region UnityMessages
    private void OnEnable()
    {
        EventController.ScoreChanged += OnScoreChanged;
        EventController.LivesChanged += OnLivesChanged;
        EventController.ResetGame += StartMenu;
        EventController.ResumeGame += Playing;
        EventController.GameOver += GameOver;
    }

    private void OnDisable()
    {
        EventController.ScoreChanged -= OnScoreChanged;
        EventController.LivesChanged -= OnLivesChanged;
        EventController.ResetGame -= StartMenu;
        EventController.ResumeGame -= Playing;
        EventController.GameOver -= GameOver;
    }
    #endregion

    #region Delegates
    private void OnScoreChanged(int _score)
    {
        scoreVisitor.text = "Score: " + _score.ToString();
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
    private void StartMenu()
    {
        startMenuUI.SetActive(true);
        playingUI.SetActive(false);
        gameOverUI.SetActive(false);
        pauseMenuUI.SetActive(false);
        winUI.SetActive(false);
    }
    private void Playing()
    {
        startMenuUI.SetActive(false);
        playingUI.SetActive(true);
        gameOverUI.SetActive(false);
        pauseMenuUI.SetActive(false);
        winUI.SetActive(false);
    }
    private void GameOver()
    {
        startMenuUI.SetActive(false);
        playingUI.SetActive(false);
        gameOverUI.SetActive(true);
        pauseMenuUI.SetActive(false);
        winUI.SetActive(false);
    }
    private void PauseMenu()
    {
        startMenuUI.SetActive(false);
        playingUI.SetActive(true);
        gameOverUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        winUI.SetActive(false);
    }
    private void Win()
    {
        startMenuUI.SetActive(false);
        playingUI.SetActive(false);
        gameOverUI.SetActive(false);
        pauseMenuUI.SetActive(false);
        winUI.SetActive(true);
    }
    #endregion
}
