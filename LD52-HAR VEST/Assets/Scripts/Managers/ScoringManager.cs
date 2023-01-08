using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoringManager : Singleton<ScoringManager>
{
    [Header("Time")]
    [SerializeField] float gameTimer;
    TimeSpan timePlaying;
    bool isTiming;

    [Header("Score")]
    [SerializeField] int currentScore;


    public delegate void UpdateScoreUI(int value);
    public static event UpdateScoreUI OnUpdateScoreUI;

    private void Awake()
    {
        if (ScoringManager.Instance != this)
            Destroy(gameObject);


        timePlaying = TimeSpan.FromSeconds(gameTimer);
    }

    // Update is called once per frame
    void Update()
    {
        // For the timer functions
        if (isTiming)
        {
            gameTimer += Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(gameTimer);
        }
    }

    public void StartTimer()
    {
        isTiming = true;
    }

    public void PauseTimer()
    {
        isTiming = false;
    }

    public TimeSpan GetTimer()
    {
        return timePlaying;
    }

    public void AddScore(int value)
    {
        currentScore += value;
        OnUpdateScoreUI?.Invoke(currentScore);
    }

    public int GetScore()
    {
        return currentScore;
    }

    public void ResetValues()
    {
        gameTimer = 0;
        currentScore = 0;
    }
}
