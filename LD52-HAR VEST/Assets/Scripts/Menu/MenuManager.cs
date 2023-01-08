using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    AudioManager audioManager;

    [SerializeField]
    bool canClick;

    [SerializeField]
    string sceneToLoad;

    [SerializeField]
    Animator fade;

    private void Start()
    {
        audioManager = AudioManager.Instance;
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
            StartCoroutine(CreditsSequence());
        }
    }

    IEnumerator CreditsSequence()
    {
        yield return new WaitForSeconds(.2f);
    }

    public void ExitGame()
    {
        if (canClick)
        {
            canClick = false;
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

    public void PlayMusic()
    {
        audioManager.StopPlayMusic(audioManager.GetSound(0, 0), 2.5f, 2f);
    }
}
