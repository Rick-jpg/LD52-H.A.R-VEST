using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
    [SerializeField]
    [Tooltip("Can only be 1 or -1")]
    private int direction = 1;

    [Header("Jumping")]
    [SerializeField]
    private float jumpPower = 3f;
    [SerializeField]
    private bool isJumping;
    [SerializeField]
    private float jumpPressedRemember = 0;
    [SerializeField]
    private float jumpPressedRememberTime = 0.25f;
    [SerializeField]
    private float groundedRemember = 0;
    [SerializeField]
    private float groundRememberTime = 0.2f;

    [Header("Dashing")]
    [SerializeField]
    private float dashSpeed = 10f;
    [SerializeField]
    private float dashTime = 0.5f;
    [SerializeField]
    private float dashCooldown = 2f;
    [SerializeField]
    private bool canDash = true;
    [SerializeField]
    private bool isDashing;

    [Header("Gravity")]
    [SerializeField]
    float gravityMultiplier = 0f;
    [SerializeField]
    float holdJumpGravityMultiplier = 3f;
    [SerializeField]
    [Tooltip("Will automatically be set to 2 x holdJumpGravityMultiplier")]
    float tapJumpGravityMultiplier;
    private const float GRAVITY = -9.81f;
    [SerializeField]
    private float velocity;

    void Start()
    {
        tapJumpGravityMultiplier = holdJumpGravityMultiplier * 2;
        inputHandler = GetComponentInChildren<InputHandler>();
        characterController = GetComponent<CharacterController>();


        gravityMultiplier = holdJumpGravityMultiplier;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDashing)
        {
            ApplyGravity();
            MovementHandling();
            JumpingHandling();
        }
        DashHandling();
    }

    private void ApplyGravity()
    {
        groundedRemember -= Time.deltaTime;

        if(velocity > 0f && inputHandler.GetJump()== false)
        {
            gravityMultiplier = tapJumpGravityMultiplier;
        }
        if (isGrounded() && velocity < 0f)
        {
            velocity = -1f;
            gravityMultiplier = holdJumpGravityMultiplier;
            groundedRemember = groundRememberTime;
        }
        else
        {
            velocity += GRAVITY * gravityMultiplier * Time.deltaTime;
        }
        
    }

    private void MovementHandling()
    {
        float movementInput = inputHandler.GetMovement();
        if (movementInput > 0f) movementInput = 1f;
        if (movementInput < 0f) movementInput = -1f;
        SetDirection(movementInput);
        float xMovement = movementInput * movementSpeed;

        movement = new Vector3(xMovement, velocity, 0) * Time.deltaTime;
        characterController.Move(movement);
    }

    private void SetDirection(float movement)
    {
        if (movement == 0) return;
        direction = Convert.ToInt32(movement);
    }

    private void JumpingHandling()
    {
        jumpPressedRemember -= Time.deltaTime;
        if (jumpPressedRemember > 0f && groundedRemember > 0)
        {
            jumpPressedRemember = 0f;
            groundedRemember = 0f;
            return;
        }

        isJumping = inputHandler.GetJumpDown();
        if (!isJumping) return;
        if (!isGrounded()) return;

        jumpPressedRemember = jumpPressedRememberTime;
        velocity += jumpPower;
    }

    private bool isGrounded()
    {
        return characterController.isGrounded;
    }

    private void DashHandling()
    {
        isDashing = inputHandler.GetDash();
        if(isDashing && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float savedVelocity = velocity;
        velocity = 0;
        gravityMultiplier = 0;

        float startTIme = Time.time;

        while (Time.time < startTIme + dashTime)
        {
            characterController.Move(new Vector3(direction, 0, 0) * dashSpeed * Time.deltaTime);
            yield return null;
        }
        isDashing = false;
        velocity = savedVelocity;
        gravityMultiplier = 3f;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
       
    }

    public int Direction { get { return direction; } }
}
