using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour, IActivatable
{
    public delegate void PlayerHit();
    public static event PlayerHit OnPlayerHit;
    LineRenderer laserRenderer;
    [SerializeField]
    BoxCollider laserTriggerBox;

    private void Start()
    {
        laserRenderer = GetComponent<LineRenderer>();
        laserTriggerBox = GetComponent<BoxCollider>();
    }
    public void Activate()
    {
        laserRenderer.enabled = false;
        laserTriggerBox.enabled = false;
    }

    public void Deactivate()
    {
        laserRenderer.enabled=true;
        laserTriggerBox.enabled=true;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        if(other.GetComponent<PlayerController>() != null)
        {     
            OnPlayerHit.Invoke();
        }
    }

}
