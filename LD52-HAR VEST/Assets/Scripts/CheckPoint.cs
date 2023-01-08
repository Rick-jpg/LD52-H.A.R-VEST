using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{

    private Respawn respawn;

    void Awake()
    {
        respawn = GameObject.FindGameObjectWithTag("Respawn").GetComponent<Respawn>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            respawn.respawnPoint = this.gameObject;
        }
    }



}
