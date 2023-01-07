using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static bool isGameOver;
    public static bool ResetLevel;


    public static Vector2 lastCheckPointPos = new Vector3(-2,1);

    private void Awake()
    {
        isGameOver = false;
        GameObject.FindGameObjectWithTag("Player").transform.position = lastCheckPointPos;
    }

    private void Update()
    {
        if (ResetLevel == true)
        {
            SceneManager.LoadScene("Checkpoint test");
        }
    }


}
