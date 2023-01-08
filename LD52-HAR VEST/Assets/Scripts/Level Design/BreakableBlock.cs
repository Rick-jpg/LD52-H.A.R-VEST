using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableBlock : MonoBehaviour, IHittable
{
    bool hasBeenHit;
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Bullet>() != null || !hasBeenHit)
        {
            Hit();
        }
    }
    public void Hit()
    {
        hasBeenHit = true;
        this.gameObject.SetActive(false);
    }

    public void Reset()
    {
        hasBeenHit = false;
        this.gameObject.SetActive(true);
    }

}