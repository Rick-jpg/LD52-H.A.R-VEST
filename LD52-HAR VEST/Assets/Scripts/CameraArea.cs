using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class CameraArea: MonoBehaviour
{
    [SerializeField]
    private Door door;
    PlayerController playerController;
    [Header("Bounds")]
    public Vector2 boundsMin;
    public Vector2 boundsMax;
    private bool isActivated;

    public delegate void ChangeCameraArea(CameraArea area);
    public static event ChangeCameraArea OnChangeCameraArea;
    public delegate void ToggleReset(bool value);
    public static event ToggleReset OnToggleReset;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerController>() != null && !isActivated)
        {
            isActivated = true;
            playerController = other.GetComponent<PlayerController>();
            OnChangeCameraArea.Invoke(this);
            if (door == null) return;
            StartCoroutine(CloseDoor());
        }
    }

    IEnumerator CloseDoor()
    {
        playerController.ToggleInput(false);
        playerController.SetCanMove(false);
        door.PlayDoorAnimation(false);
        yield return new WaitForSeconds(1f);
        playerController.SetCanMove(true);
        playerController.ToggleInput(true);
        OnToggleReset?.Invoke(true);
    }
}
