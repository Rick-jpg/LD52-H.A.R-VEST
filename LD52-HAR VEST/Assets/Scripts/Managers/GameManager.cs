using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    [Header("ScoreUI")]
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI timerText;

    private void OnEnable()
    {
        ScoringManager.OnUpdateScoreUI += UpdateScoreText;
    }

    private void OnDisable()
    {
        ScoringManager.OnUpdateScoreUI -= UpdateScoreText;
    }

    private void Start()
    {
        ScoringManager.Instance.StartTimer();
    }

    private void Update()
    {
        UpdateTimerText();
    }

    public void UpdateScoreText(int value)
    {
        scoreText.text = value.ToString();
    }

    public void UpdateTimerText()
    {
        TimeSpan time = ScoringManager.Instance.GetTimer();
        timerText.text = string.Format("{0:D2}:{1:D2}", time.Minutes, time.Seconds);
    }
}
