using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Threading;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public delegate void ResetLevel();
    public static ResetLevel onResetLevel;
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
    bool hasJumped;

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
    bool doGravity = true;
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
    [SerializeField]
    ParticleSystem energyParticle;
    int energyExtraAdded = 2;
    [SerializeField]
    Energybar energyBar;
    [Header("Death")]
    [SerializeField]
    private float deathTime = 4f;

    private void OnEnable()
    {
        EnergyEndMachine.OnCompleteLevel += SetMaximumEnergy;
        Attack.OnEnergyUsed += DecreaseEnergy;
        Laser.OnPlayerHit += Death;
        RespawnManager.onResetLevel += ResetEnergy;
    }

    private void OnDisable()
    {
        EnergyEndMachine.OnCompleteLevel -= SetMaximumEnergy;
        Attack.OnEnergyUsed -= DecreaseEnergy;
        Laser.OnPlayerHit -= Death;
        RespawnManager.onResetLevel -= ResetEnergy;
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
        energyBar.UpdateEnergyBarFill(CalculateEnergyFill());

        // Set some variables to true
        //canMove = true;
        doGravity = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            if (!isDashing)
            {
               if (doGravity)
                    ApplyGravity();
               MovementHandling();
               JumpingHandling();

                if (isGrounded())
                    if (canDash == false) canDash = true;
            }

            if (hasJumped && velocity < -0.01f)
            {
                if (isGrounded())
                {
                    hasJumped = false;
                    AudioManager.Instance.PlayRandomSound(1, 2, 4);
                }
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

    public void EnableGravity(bool enable)
    {
        doGravity = enable;

        if (!enable)
        {
            gravityMultiplier = 3f;
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
        canMove = value;        if (value) return;        DisableAnimation();
    }

    void DisableAnimation()
    {
        isWalking = false;
        isDashing = false;
        velocity = 0f;
    }

    void SetMaximumEnergy()
    {
        maxEnergyCapacity += energyExtraAdded;
        currentEnergy = maxEnergyCapacity;
        energyBar.ChangeText(currentEnergy, maxEnergyCapacity);
        energyBar.UpdateEnergyBarFill(CalculateEnergyFill());
    }

    public int GetMaximumEnergy()
    {
        return maxEnergyCapacity;
    }

    public int GetCurrentEnergy()
    {
        return currentEnergy;
    }

    void ResetEnergy()
    {
        currentEnergy = maxEnergyCapacity;
        energyBar.ChangeText(currentEnergy, maxEnergyCapacity);
        energyBar.UpdateEnergyBarFill(CalculateEnergyFill());
    }

    void DecreaseEnergy(int amount)
    {
        int difference = currentEnergy - amount;
        if (difference < 0) difference = 0;
        currentEnergy = difference;
        energyBar.ChangeText(currentEnergy, maxEnergyCapacity);
        energyBar.UpdateEnergyBarFill(CalculateEnergyFill());
        energyParticle.Play();
    }

    float CalculateEnergyFill()
    {
        return (float)currentEnergy / (float)maxEnergyCapacity;
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
        AudioManager.Instance.PlaySound(1, 1);
        hasJumped = true;
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

    private void DashHandling()
    {
        dashInput = inputHandler.GetJumpDown();
        if (dashInput && canMove && canDash && !isGrounded())
        {
            AudioManager.Instance.PlaySound(1, 0);
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

    private void Death()
    {
        StartCoroutine(DeathSequence());
    }

    private IEnumerator DeathSequence()
    {
        EnableGravity(false);
        ToggleInput(false);
        SetCanMove(false);
        //Play Death Animation
        yield return new WaitForSeconds(deathTime);
        onResetLevel?.Invoke();
        yield return new WaitForSeconds(1f);
    }

    void UpdateAnim()
    {
        anim.SetBool("Grounded", isGrounded());
        anim.SetBool("Running", isWalking);

        anim.SetBool("Airdashing", isDashing);
        anim.SetBool("Shooting", attackHandler.GetAttackisBeingUsed(0));
        anim.SetBool("LightningAttack", attackHandler.GetAttackisBeingUsed(1));
        anim.SetBool("Teleporting", attackHandler.GetAttackisBeingUsed(2));

        anim.SetFloat("VerticalVelocity", characterController.velocity.y);
    }

    public int Direction { get { return direction; } }
}
