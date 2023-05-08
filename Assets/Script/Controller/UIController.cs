using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoSingleton<UIController>
{
    [SerializeField] private TextMeshProUGUI scoreVisitor;

    #region UnityMessages
    private void OnEnable()
    {
        EventController.OnScoreChanged += OnScoreChanged;
    }

    private void OnDisable()
    {
        EventController.OnScoreChanged -= OnScoreChanged;

    }
    #endregion

    #region Delegates
    private void OnScoreChanged(int _score)
    {
        scoreVisitor.text = "Score: " + _score.ToString();
    }
    #endregion
}
