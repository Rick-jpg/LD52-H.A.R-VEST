using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public float GetMovement()
    {
        return Input.GetAxisRaw("Horizontal");
    }

    public bool GetJump()
    {
        return Input.GetButton("Jump");
    }
}
