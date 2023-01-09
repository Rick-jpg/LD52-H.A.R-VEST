using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObstaclesManager : MonoBehaviour
{
    [SerializeField]
    private List<IHittable> hittables = new List<IHittable>();

    private void OnEnable()
    {
        RespawnManager.onResetLevel += ResetAll;
    }

    private void OnDisable()
    {
        RespawnManager.onResetLevel -= ResetAll;
    }

    private void Start()
    {
        hittables = GetComponentsInChildren<IHittable>().ToList();
    }

    private void ResetAll()
    {
        foreach (IHittable hit in hittables)
        {
            hit.Reset();
        }
    }

}
