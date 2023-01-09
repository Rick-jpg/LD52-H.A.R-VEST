using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class BreakableBlock : MonoBehaviour, IHittable
{
    [SerializeField]
    GameObject destroyedParticlePrefab;
    GameObject spawnedPrefab;

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
        spawnedPrefab = Instantiate(destroyedParticlePrefab, transform.position, Quaternion.identity);
        AudioManager.Instance.PlaySound(2, 0);
        this.gameObject.SetActive(false);
    }

    public void Reset()
    {
        hasBeenHit = false;
        Destroy(spawnedPrefab);
        this.gameObject.SetActive(true);
    }
}
