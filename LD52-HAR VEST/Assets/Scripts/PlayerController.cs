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
    AttackHandler attackHandler;
    AnimationManager anim;

    [Header("Movement")]
    bool canMove;
    [SerializeField]
    private float movementSpeed = 5f;
    Vector3 movement = Vector3.zero;
    [SerializeField]
    [Tooltip("Can only be 1 or -1")]
    private int direction = 1;
    bool isWalking;

    [Header("Jumping")]
    [SerializeField]
    private float jumpPower = 3f;
    [SerializeField]
    private bool jumpInput;
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
    private bool canDash = true;
    [SerializeField]
    private bool dashInput;
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
        attackHandler = GetComponent<AttackHandler>();
        anim = GetComponent<AnimationManager>();


        gravityMultiplier = holdJumpGravityMultiplier;

        // Set canMove to true
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
                if (!isDashing)
            {
               ApplyGravity();
               MovementHandling();
               JumpingHandling();
                if (canDash == false) canDash = true;
             }
        }

        DashHandling();

        UpdateAnim();
    }

    private void ApplyGravity()
    {
        groundedRemember -= Time.deltaTime;

        if (velocity > 0f && inputHandler.GetJump() == false)
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

        if (xMovement > 0.01f || xMovement < -0.01f)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }

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

        jumpInput = inputHandler.GetJumpDown();
        if (!jumpInput) return;
        if (!isGrounded()) return;
        isJumping = true;
        jumpPressedRemember = jumpPressedRememberTime;
        velocity += jumpPower;
        isJumping = false;
    }

    private bool isGrounded()
    {
        return characterController.isGrounded;
    }

    private void DashHandling()
    {
        dashInput = inputHandler.GetJumpDown();
        if(dashInput && canDash && !isGrounded())
        {
            StartCoroutine(Dash());
        }
    }

    void UpdateAnim()
    {
        anim.SetBool("Grounded", isGrounded());
        anim.SetBool("Running", isWalking);

        anim.SetBool("Airdashing", isDashing);
        anim.SetBool("Shooting", attackHandler.GetAttackisBeingUsed(0));
        //anim.SetBool("Teleporting", );

        anim.SetFloat("VerticalVelocity", characterController.velocity.y);
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float savedVelocity = velocity;
        velocity = 0;
        gravityMultiplier = 0;

        float startTime = Time.time;

        while (Time.time < startTime + dashTime)
        {
            characterController.Move(new Vector3(direction, 0, 0) * dashSpeed * Time.deltaTime);
            yield return null;
        }
        velocity = savedVelocity;
        gravityMultiplier = 3f;
        isDashing = false;
       
    }

    public int Direction { get { return direction; } }
}
