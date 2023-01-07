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
    [SerializeField]
    GameObject rotatorObject;

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

    bool canHandleInput;

    [Header("Energy")]
    [SerializeField]
    private int maxEnergyCapacity = 5;
    [SerializeField]
    private int currentEnergy;
    int energyExtraAdded = 2;

    [SerializeField]
    Energybar energyBar;

    private void OnEnable()
    {
        EnergyEndMachine.OnCompleteLevel += SetMaximumEnergy;
        Attack.OnEnergyUsed += DecreaseEnergy;
    }

    private void OnDisable()
    {
        EnergyEndMachine.OnCompleteLevel -= SetMaximumEnergy;
        Attack.OnEnergyUsed -= DecreaseEnergy;
    }

    void Start()
    {
        tapJumpGravityMultiplier = holdJumpGravityMultiplier * 2;
        inputHandler = GetComponentInChildren<InputHandler>();
        characterController = GetComponent<CharacterController>();
        attackHandler = GetComponent<AttackHandler>();
        anim = GetComponent<AnimationManager>();


        gravityMultiplier = holdJumpGravityMultiplier;
        currentEnergy = maxEnergyCapacity;

        energyBar.ChangeText(currentEnergy, maxEnergyCapacity);

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

                if (isGrounded())
                    if (canDash == false) canDash = true;
            }
        }

        DashHandling();

        UpdateAnim();

        // Set scale the same as direction;
        Vector3 scale = rotatorObject.transform.localScale;
        rotatorObject.transform.localScale = new Vector3(direction, scale.y, scale.z);
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

    public void SetCanMove(bool value)
    {
        canMove = value;
    }

    void SetMaximumEnergy()
    {
        maxEnergyCapacity += energyExtraAdded;
        currentEnergy = maxEnergyCapacity;
        energyBar.ChangeText(currentEnergy, maxEnergyCapacity);
    }

    public int GetMaximumEnergy()
    {
        return maxEnergyCapacity;
    }

    public int GetCurrentEnergy()
    {
        return currentEnergy;
    }

    void DecreaseEnergy(int amount)
    {
        int difference = currentEnergy - amount;
        if (difference < 0) difference = 0;
        currentEnergy = difference;
        energyBar.ChangeText(currentEnergy, maxEnergyCapacity);
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
        jumpPressedRemember = jumpPressedRememberTime;
        velocity += jumpPower;
    }

    private bool isGrounded()
    {
        return characterController.isGrounded;
    }

    public void ToggleInput(bool inputStatus)
    {
        canHandleInput = inputStatus;
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

    private void DashHandling()
    {
        dashInput = inputHandler.GetJumpDown();
        if (dashInput && canDash && !isGrounded())
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
