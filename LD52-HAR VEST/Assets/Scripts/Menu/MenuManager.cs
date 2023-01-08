using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    AudioManager audioManager;

    [SerializeField]
    bool canClick;
    bool canExitPanel;

    [SerializeField]
    string sceneToLoad;

    [SerializeField]
    Animator fade;
    [SerializeField]
    CanvasGroup buttonGroup;
    [SerializeField]
    CanvasGroup creditsGroup;
    [SerializeField]
    CanvasGroup storyGroup;

    CanvasGroup currentGroup;

    private void Start()
    {
        audioManager = AudioManager.Instance;

        creditsGroup.alpha= 0f;
        storyGroup.alpha= 0f;
    }

    private void Update()
    {
        if (canExitPanel)
        {
            if (Input.anyKeyDown || Input.GetButtonDown("TeleportAttack") || Input.GetButtonDown("Jump"))
            {
                ClosePanel();
            }
        }
    }

    public void SetCanClick(bool value)
    {
        canClick = value;
    }

    public void FadeIn()
    {
        fade.Play("FadeBlackIn");
    }

    public void FadeOut()
    {
        fade.Play("FadeBlackOut");
    }

    public void LoadScene()
    {
        if (canClick)
        {
            canClick = false;
            AudioManager.Instance.PlaySound(3, 0);
            DisableButtons();
            StartCoroutine(LoadSceneSequence());
        }
    }

    IEnumerator LoadSceneSequence()
    {
        yield return new WaitForSeconds(.2f);
        audioManager.StopMusic(2.5f);
        FadeIn();
        yield return new WaitForSeconds(4f);

        SceneManager.LoadScene(sceneToLoad);
    }

    public void ShowCredits()
    {
        if (canClick)
        {
            canClick = false;
            AudioManager.Instance.PlaySound(3, 0);
            DisableButtons();
            currentGroup = creditsGroup;
            StartCoroutine(PanelSequence(0.8f));
        }
    }

    public void ShowStory()
    {
        if (canClick)
        {
            canClick = false;
            AudioManager.Instance.PlaySound(3, 0);
            DisableButtons();
            currentGroup = storyGroup;
            Debug.Log(currentGroup);
            StartCoroutine(PanelSequence(0.8f));
        }
    }

    IEnumerator PanelSequence(float duration)
    {
        currentGroup.blocksRaycasts = true;

        yield return new WaitForSeconds(.2f);

        float currentTime = 0;
        float start = 0;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            currentGroup.alpha = Mathf.Lerp(start, 1, currentTime / duration);
            yield return null;
        }
        currentGroup.alpha = 1f;

        Debug.Log("Panel Opened");
        canExitPanel = true;
        yield break;
    }

    public void ClosePanel()
    {
        Debug.Log("Panel Closed");
        AudioManager.Instance.PlaySound(3, 1);
        StartCoroutine(ClosePanelSequence(currentGroup, .5f));
    }

    IEnumerator ClosePanelSequence(CanvasGroup panel, float duration)
    {
        panel.blocksRaycasts = false;

        float currentTime = 0;
        float start = 1;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            currentGroup.alpha = Mathf.Lerp(start, 0, currentTime / duration);
            yield return null;
        }

        canExitPanel = false;
        canClick = true;
        EnableButtons();
        yield break;
    }

    public void ExitGame()
    {
        if (canClick)
        {
            canClick = false;
            AudioManager.Instance.PlaySound(3, 0);
            DisableButtons();
            StartCoroutine(ExitSequence());
        }
    }

    IEnumerator ExitSequence()
    {
        yield return new WaitForSeconds(1f);
        audioManager.StopMusic(1f);
        FadeIn();
        yield return new WaitForSeconds(1.5f);
        Application.Quit();
    }

    public void DisableButtons()
    {
        buttonGroup.alpha = 0;
        buttonGroup.enabled = false;
    }

    public void EnableButtons()
    {
        buttonGroup.alpha = 1;
        buttonGroup.enabled = true;
    }

    public void PlayMusic()
    {
        audioManager.StopPlayMusic(audioManager.GetSound(0, 0), 2.5f, 2f);
    }
}
