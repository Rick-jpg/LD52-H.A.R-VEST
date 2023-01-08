using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Door : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    private string boolString = "isOpen";
    private int isOpenHashId;

    private void Start()
    {
        animator = GetComponent<Animator>();
        isOpenHashId = Animator.StringToHash(boolString);
    }

    public void PlayDoorAnimation(bool value)
    {
        animator.SetBool(isOpenHashId, value);
    }
}
