using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent (typeof(BoxCollider))]
public class SwitchDoor : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    private BoxCollider doorCollider;

    private string boolString = "isOpen";
    private int isOpenHashId;

    private void Start()
    {
        doorCollider = GetComponent<BoxCollider>();
        animator = GetComponent<Animator>();
        isOpenHashId = Animator.StringToHash(boolString);
    }

    public void PlayDoorAnimation(bool value)
    {
        animator.SetBool(isOpenHashId, value);
        if (value) doorCollider.enabled = false;
        else { doorCollider.enabled = true; }
    }
}
