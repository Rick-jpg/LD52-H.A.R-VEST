using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyEndMachine : MonoBehaviour
{
    private bool isActivated;
    public delegate void CompleteLevel();
    public static CompleteLevel OnCompleteLevel;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerController>() != null && isActivated == false)
        {
            StartCoroutine(CollectedEnergySequence());
            isActivated = true;
        }
    }

    public IEnumerator CollectedEnergySequence()
    {
        OnCompleteLevel?.Invoke();
        yield return new WaitForSeconds(2f);
    }
}
