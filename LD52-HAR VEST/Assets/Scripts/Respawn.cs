using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Respawn : MonoBehaviour
{

    public GameObject player;
    public GameObject respawnPoint;


    void Update()
    {
        
    }

    private void ResetLevel()
    {
        player.transform.position = respawnPoint.transform.position;
        Debug.Log("uh resetting lol");
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


