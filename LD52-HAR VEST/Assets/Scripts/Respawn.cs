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


    void Update()
    {
        
    }

    private void ResetLevel()
    {
        playerController.SetCanMove(false);
        player.transform.position = respawnPoint.transform.position;
        playerController.SetCanMove(true);
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


