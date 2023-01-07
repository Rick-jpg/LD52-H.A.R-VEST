using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScrollingCamera : MonoBehaviour
{
    [SerializeField]
    private PlayerController player;
    [SerializeField]
    private CameraArea area;
    [SerializeField]
    private Vector2 offsetPos;
    [SerializeField]
    float movementTime;
    private void OnEnable()
    {
        CameraArea.OnChangeCameraArea += ChangeArea;
    }

    private void LateUpdate()
    {
        if(player == null) return;
        Vector3 startPos = transform.position;
        Vector3 targetPos = player.transform.position;

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

    private void OnDisable()
    {
        CameraArea.OnChangeCameraArea -= ChangeArea;
    }
}
