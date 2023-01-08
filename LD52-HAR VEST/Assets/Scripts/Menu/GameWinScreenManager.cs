using System.Collections;
using System.Collections.Generic;
using UnityEditor.XR;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class GameWinScreenManager : MonoBehaviour
{
    [Header("Score texts")]
    [SerializeField]
    TextMeshProUGUI scoreText;
    [SerializeField]
    TextMeshProUGUI timerText;
    [SerializeField]
    TextMeshProUGUI rankText;

    [Header("Variables")]
    [SerializeField]
    float duration = 1.5f;

    [SerializeField]
    bool canContinue;

    [SerializeField]
    Animator fade;

    [Header("rank")]
    [SerializeField]
    Rank[] ranks;
    [SerializeField]
    float minTimeThreshold = 30;
    [SerializeField]
    float maxTimeThreshold = 180;
    [SerializeField]
    float maxTimeBonus = 2000;
    int finalScore;

    string rank;

    private void Start()
    {
        // pause timer just in case
        ScoringManager.Instance.PauseTimer();

        // get time
        TimeSpan time = ScoringManager.Instance.GetTimer();
        timerText.text = string.Format("{0:D2}:{1:D2}", time.Minutes, time.Seconds);

        // get score
        scoreText.text = ScoringManager.Instance.GetScore().ToString();

        // Calc Rank
        rankText.text = CalculateRank();
    }

    private void Update()
    {
        if (canContinue)
        {
            if (Input.GetButtonDown("Submit") || Input.GetButtonDown("TeleportAttack") || Input.GetButtonDown("Jump"))
            {
                StartCoroutine(NextScene());
            }
        }
    }

    public void FadeCanvasIn(CanvasGroup canvas)
    {
        StartCoroutine(FadeInSequence(canvas, duration));
    }

    IEnumerator FadeInSequence(CanvasGroup canvas, float duration)
    {
        float currentTime = 0;
        float start = 0;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            canvas.alpha = Mathf.Lerp(start, 1, currentTime / duration);
            yield return null;
        }
        canvas.alpha = 1f;

        yield break;
    }

    public void SetCanContinue(bool value)
    {
        canContinue = value;
    }

    IEnumerator NextScene()
    {
        yield return new WaitForSeconds(.5f);
        fade.Play("FadeBlackIn");
        ScoringManager.Instance.ResetValues();
        AudioManager.Instance.StopMusic(2.5f);
        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadScene("MainMenu");
    }

    public void StartMusic()
    {
        switch (rank)
        {
            case "S":
                AudioManager.Instance.PlayMusic(AudioManager.Instance.GetSound(0, 3));
                break;

            case "A":
                AudioManager.Instance.PlayMusic(AudioManager.Instance.GetSound(0, 4));
                break;

            case "B":
                AudioManager.Instance.PlayMusic(AudioManager.Instance.GetSound(0, 5));
                break;

            case "C":
                AudioManager.Instance.PlayMusic(AudioManager.Instance.GetSound(0, 6));
                break;

            case "D":
                AudioManager.Instance.PlayMusic(AudioManager.Instance.GetSound(0, 7));
                break;

            case "F":
                AudioManager.Instance.PlayMusic(AudioManager.Instance.GetSound(0, 8));
                break;

            default:
                break;
        }
    }

    public string CalculateRank()
    {
        float timeValue = (float)ScoringManager.Instance.GetTimer().TotalSeconds;
        int timeBonus = 0;

        // Calculate time bonus if above theshold
        if (timeValue < maxTimeThreshold)
        {
            timeBonus = Convert.ToInt32(Remap(timeValue, minTimeThreshold, maxTimeThreshold, maxTimeBonus, 0));
            Debug.Log(timeBonus);
        }

        finalScore = timeBonus + ScoringManager.Instance.GetScore();

        rank = "B";

        for (int i = 0; i < ranks.Length; i++)
        {
            if (finalScore >= ranks[i].GetThreshold())
            {
                rank = ranks[i].GetLetter();
            }
        }

        return rank;
    }

    public float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}
