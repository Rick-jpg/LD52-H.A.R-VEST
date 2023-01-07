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

    public bool GetJumpDown()
    {
        return Input.GetButtonDown("Jump");
    }

    public bool GetDash()
    {
        return Input.GetButtonDown("Fire1");
    }
}
