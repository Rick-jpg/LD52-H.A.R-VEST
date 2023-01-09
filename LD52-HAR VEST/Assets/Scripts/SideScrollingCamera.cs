using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScrollingCamera : MonoBehaviour
{
    [SerializeField]
    private GameObject lookAhead;
    [SerializeField]
    private CameraArea area;
    [SerializeField]
    private Vector2 offsetPos;
    [SerializeField]
    float movementTime;
    private void OnEnable()
    {
        CameraArea.OnChangeCameraArea += ChangeArea;
        EnergyEndMachine.OnShortenBorder += ChangeBoundariesEndSection;
    }

    private void LateUpdate()
    {
        if(lookAhead == null) return;
        Vector3 startPos = transform.position;
        Vector3 targetPos = lookAhead.transform.position;

        targetPos.x += offsetPos.x;
        targetPos.y += offsetPos.y;
        targetPos.z = startPos.z;

        targetPos.x = Mathf.Clamp(targetPos.x, area.boundsMin.x, area.boundsMax.x);
        targetPos.y = Mathf.Clamp(targetPos.y, area.boundsMin.y, area.boundsMax.y);

        transform.position = Vector3.Lerp(startPos, targetPos, movementTime);
    }

    private void ChangeArea(CameraArea newArea)
    {
        area = newArea;
    }

    private void ChangeBoundariesEndSection(float difference)
    {
        float newMin = area.boundsMax.x - difference;
        area.boundsMin.x = newMin;
    }

    private void OnDisable()
    {
        CameraArea.OnChangeCameraArea -= ChangeArea;
        EnergyEndMachine.OnShortenBorder -= ChangeBoundariesEndSection;
    }
}
