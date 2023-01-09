using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider))]
public class LevelEndTrigger : MonoBehaviour
{
    PlayerController player;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            player = other.GetComponent<PlayerController>();
            StartCoroutine(goToWinScreen());
        }
    }

    IEnumerator goToWinScreen()
    {
        player.EnableGravity(false);
        player.ToggleInput(false);
        player.SetCanMove(false);
        ScoringManager.Instance.PauseTimer();
        AudioManager.Instance.StopPlayMusic(AudioManager.Instance.GetSound(0, 2), 2f, 2f);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("GameWinScreen");
    }
}
