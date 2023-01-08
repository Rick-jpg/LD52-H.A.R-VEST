using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioDebugger : MonoBehaviour
{
    AudioManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = AudioManager.Instance;
    }

    // Debug
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            manager.PlayMusic(manager.GetSound(0, 0), 2f);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            manager.StopPlayMusic(manager.GetSound(0, 1), 3f, 3f);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            SceneManager.LoadScene("TestScene");
        }
    }
}
