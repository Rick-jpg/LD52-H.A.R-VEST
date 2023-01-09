using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroCamera : MonoBehaviour
{
    Camera mainCamera;
    [SerializeField]
    private float desiredFOV;
    private float currentFov;
    [SerializeField]
    private PlayerController player;
    [SerializeField]
    private SideScrollingCamera offsetCamera;

    private Vector2 endOffset = new Vector2(0, 0.2f);
    // Start is called before the first frame update
    void Start()
    {
        offsetCamera = GetComponent<SideScrollingCamera>();
        mainCamera = Camera.main;
        currentFov = mainCamera.fieldOfView;
        StartCoroutine(ZoomOut());
    }

    IEnumerator ZoomOut()
    {
        yield return new WaitForSeconds(2f);
        offsetCamera.ChangeOffset(endOffset);
        while (currentFov < desiredFOV)
        {
            currentFov++;
            mainCamera.fieldOfView = currentFov;
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.5f);
        player.SetCanMove(true);


    }
}
