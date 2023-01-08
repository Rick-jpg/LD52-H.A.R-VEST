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

    [SerializeField]
    ParticleSystem electricParticle;
    [SerializeField]
    GameObject orb;

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
        yield return new WaitForSeconds(0.5f);
        electricParticle.transform.position = playerController.transform.position;
        electricParticle.Play();
        AudioManager.Instance.PlaySound(1, 6);
        yield return new WaitForSeconds(1.5f);
        orb.SetActive(false);
        door.PlayDoorAnimation(true);
        yield return new WaitForSeconds(.5f);
        electricParticle.Stop();
        yield return new WaitForSeconds(.2f);
        playerController.SetCanMove(true);
        playerController.ToggleInput(true);
    }
}
