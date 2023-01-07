using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [SerializeField]
    Animator anim;

    public void SetBool(string parameter, bool value)
    {
        anim.SetBool(parameter, value);
    }

    public void SetFloat(string parameter, float value)
    {
        anim.SetFloat(parameter, value);
    }
}
