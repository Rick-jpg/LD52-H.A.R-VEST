using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Respawn : MonoBehaviour
{

    public GameObject player;
    public GameObject respawnPoint;
    public PlayerController playerController;

    [SerializeField]
    private Animator fade;

    private void ResetLevel()
    {
        StartCoroutine(RespawnSequence());
    }

    IEnumerator RespawnSequence()
    {
        fade.Play("FadeBlackIn");
        playerController.SetCanMove(false);
        yield return new WaitForSeconds(.5f);
        player.transform.position = respawnPoint.transform.position;
        fade.Play("FadeBlackOut");
        yield return new WaitForSeconds(.5f);
        playerController.SetCanMove(true);
        playerController.EnableGravity(true);
        playerController.ToggleInput(true);
        yield return new WaitForSeconds(.1f);
    }

    public void SetRespawnPoint(GameObject newSpawnpoint)
    {
        respawnPoint = newSpawnpoint;
    }

    private void OnEnable()
    {
        RespawnManager.onResetLevel += ResetLevel;
    }

    private void OnDisable()
    {
        RespawnManager.onResetLevel -= ResetLevel;
    }


}    


