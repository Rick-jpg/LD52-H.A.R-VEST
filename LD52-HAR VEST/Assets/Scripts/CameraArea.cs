using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class CameraArea: MonoBehaviour
{
    [Header("Bounds")]
    public Vector2 boundsMin;
    public Vector2 boundsMax;

    public delegate void ChangeCameraArea(CameraArea area);
    public static event ChangeCameraArea OnChangeCameraArea;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerController>() != null)
        {
            OnChangeCameraArea.Invoke(this);
        }
    }
}
