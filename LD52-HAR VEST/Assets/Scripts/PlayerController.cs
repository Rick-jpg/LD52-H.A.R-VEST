using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    InputHandler inputHandler;
    CharacterController characterController;

    [Header("Movement")]
    [SerializeField]
    private float movementSpeed = 5f;
    Vector3 movement = Vector3.zero;

    [Header("Jumping")]
    [SerializeField]
    private float jumpPower = 3f;
    [SerializeField]
    private bool isJumping;
    private float jumpPressedRemember = 0;
    [SerializeField]
    private float jumpPressedRememberTime = 0.5f;

    [Header("Gravity")]
    [SerializeField]
    private float gravityMultiplier = 3f;
    private const float GRAVITY = -9.81f;
    [SerializeField]
    private float velocity;

    void Start()
    {
        inputHandler = GetComponentInChildren<InputHandler>();
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        ApplyGravity();
        MovementHandling();
        JumpingHandling();
    }

    private void ApplyGravity()
    {
        if(isJumping && velocity > 0f && inputHandler.GetJump()== false)
        {
            gravityMultiplier *= 2;
        }
        if (isGrounded() && velocity < 0f)
        {
            velocity = -1f;
            gravityMultiplier = 3f;
        }
        else
        {
            velocity += GRAVITY * gravityMultiplier * Time.deltaTime;
        }
        
    }

    private void MovementHandling()
    {
        float xMovement = inputHandler.GetMovement() * movementSpeed;

        movement = new Vector3(xMovement, velocity, 0) * Time.deltaTime;
        characterController.Move(movement);
    }

    private void JumpingHandling()
    {
        jumpPressedRemember -= Time.deltaTime;
        isJumping = inputHandler.GetJump();
        if (!isJumping) return;
        jumpPressedRemember = jumpPressedRememberTime;
        if (jumpPressedRemember > 0f)
        {
            velocity += jumpPower;
        }
    }

    private bool isGrounded()
    {
        return characterController.isGrounded;
    }
}
