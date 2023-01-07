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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.R))
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
}
