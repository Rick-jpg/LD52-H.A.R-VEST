using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyEndMachine : MonoBehaviour
{
    [SerializeField]
    private Door door;
    PlayerController playerController;
    private bool isActivated;
    private bool hasCollected;
    public delegate void CompleteLevel();
    public static CompleteLevel OnCompleteLevel;
    public delegate void ToggleReset(bool value);
    public static event ToggleReset OnToggleReset;

    [SerializeField]
    private BoxCollider invisibleCollider;

    [Header("Closin in")]
    [SerializeField]
    private float difference;
    public delegate void ShortenBorder(float difference);
    public static event ShortenBorder OnShortenBorder;

    private void Start()
    {
        ToggleInvisibleCollider(false);
    }

    private void ToggleInvisibleCollider(bool value)
    {
        invisibleCollider.enabled = value;
    }

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
        if (!hasCollected)
        {
            OnCompleteLevel?.Invoke();
            hasCollected = true;
        }
        yield return new WaitForSeconds(1f);
        door.PlayDoorAnimation(true);
        yield return new WaitForSeconds(.5f);
        playerController.SetCanMove(true);
        playerController.ToggleInput(true);
        ToggleInvisibleCollider(true);
        OnShortenBorder?.Invoke(difference);
        OnToggleReset?.Invoke(false);
    }

    public void ResetMachine()
    {
        isActivated = false;
        door.PlayDoorAnimation(false);
        ToggleInvisibleCollider(false);

    }

}
