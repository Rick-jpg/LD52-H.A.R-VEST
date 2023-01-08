using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyEndMachine : MonoBehaviour
{
    [SerializeField]
    private Door door;
    PlayerController playerController;
    private bool isActivated;
    public delegate void CompleteLevel();
    public static CompleteLevel OnCompleteLevel;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerController>() != null && isActivated == false)
        {
            playerController = other.GetComponent<PlayerController>();
            StartCoroutine(CollectedEnergySequence());
            isActivated = true;
        }
    }

    public IEnumerator CollectedEnergySequence()
    {
        playerController.ToggleInput(false);
        playerController.SetCanMove(false);
        OnCompleteLevel?.Invoke();
        yield return new WaitForSeconds(1f);
        door.PlayDoorAnimation(true);
        yield return new WaitForSeconds(.5f);
        playerController.SetCanMove(true);
        playerController.ToggleInput(true);
    }
}
