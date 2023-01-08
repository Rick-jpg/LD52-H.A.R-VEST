using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public delegate void ResetLevel();
    public static event ResetLevel onResetLevel;
    bool buttonPressed = false;
    float timer = 0;
    float duration = 0.5f;

    private void OnEnable()
    {
        PlayerController.onResetLevel += ResetPlayerPosition;
    }
    void Update()
    {
        if (Input.GetButton("Reset Level"))
        {
            buttonPressed = true;
            Debug.Log("button is pressed");
        }

        if (buttonPressed)
        {
            timer += Time.deltaTime;
            if (timer > duration)
            {
                timer = 0;
                buttonPressed = false;

                {
                    ResetPlayerPosition();
                    AudioManager.Instance.PlaySound(1, 5);
                    Debug.Log("button pressed");
                }
            }
        }
    }

    public void ResetPlayerPosition()
    {
        onResetLevel?.Invoke();
        Debug.Log("Resetting");
    }

    private void OnDisable()
    {
        PlayerController.onResetLevel -= ResetPlayerPosition;
    }
}
