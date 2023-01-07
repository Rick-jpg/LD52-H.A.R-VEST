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
        return Input.GetButtonDown("Dash");
    }

    public bool GetSingleAttack()
    {
        return Input.GetButtonDown("SingleShot");
    }

    public bool GetLightningAttack()
    {
        return Input.GetButtonDown("LightningShot");
    }

    public bool GetTeleportAttack()
    {
        return true;
    }

}
