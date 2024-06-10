using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement
{
    private FirstPersonController fpc;
    private Rigidbody rb;
    public Rigidbody PlayerRigidbody => rb;
    private float walkSpeed;
    private float maxVelocityChange;
    private KeyCode sprintKey;
    private float sprintSpeed;
    private float sprintDuration;
    private float sprintCooldown;
    private bool useSprintBar;
    private bool hideBarWhenFull;
    private float sprintBarWidthPercent;
    private float sprintBarHeightPercent;
    private KeyCode jumpKey;
    private float jumpPower;
    private KeyCode crouchKey;
    private float crouchHeight;
    private float speedReduction;

    private float sprintRemaining;
    private float sprintCooldownReset;
    private bool isSprinting = false;
    private bool isGrounded = false;
    private bool isCrouched = false;
    private Vector3 originalScale;
    private CanvasGroup sprintBarCG;
    private float sprintBarWidth;
    private float sprintBarHeight;
    static public bool dialogue = false; 

    public bool IsGrounded => isGrounded;
    public bool IsSprinting => isSprinting;
    public bool IsCrouched { get => isCrouched; set => isCrouched = value; }

    public PlayerMovement(FirstPersonController fpc, Rigidbody rb, float walkSpeed, float maxVelocityChange, KeyCode sprintKey, float sprintSpeed, float sprintDuration, float sprintCooldown, bool useSprintBar, bool hideBarWhenFull, float sprintBarWidthPercent, float sprintBarHeightPercent, KeyCode jumpKey, float jumpPower, KeyCode crouchKey, float crouchHeight, float speedReduction)
    {
        this.fpc = fpc;
        this.rb = rb;
        this.walkSpeed = walkSpeed;
        this.maxVelocityChange = maxVelocityChange;
        this.sprintKey = sprintKey;
        this.sprintSpeed = sprintSpeed;
        this.sprintDuration = sprintDuration;
        this.sprintCooldown = sprintCooldown;
        this.useSprintBar = useSprintBar;
        this.hideBarWhenFull = hideBarWhenFull;
        this.sprintBarWidthPercent = sprintBarWidthPercent;
        this.sprintBarHeightPercent = sprintBarHeightPercent;
        this.jumpKey = jumpKey;
        this.jumpPower = jumpPower;
        this.crouchKey = crouchKey;
        this.crouchHeight = crouchHeight;
        this.speedReduction = speedReduction;

        originalScale = rb.transform.localScale;
        sprintRemaining = sprintDuration;
        sprintCooldownReset = sprintCooldown;

        if (useSprintBar)
        {
            sprintBarWidth = sprintBarWidthPercent * Screen.width;
            sprintBarHeight = sprintBarHeightPercent * Screen.height;
        }
    }

    public void Update()
    {
        // Handle sprinting logic
        if (fpc.enableSprint && Input.GetKey(sprintKey) && sprintRemaining > 0)
        {
            isSprinting = true;
            sprintRemaining -= Time.deltaTime;
            if (sprintRemaining <= 0)
            {
                sprintRemaining = 0;
                sprintCooldownReset = sprintCooldown;
            }
        }
        else
        {
            isSprinting = false;
            if (sprintRemaining < sprintDuration)
            {
                sprintCooldownReset -= Time.deltaTime;
                if (sprintCooldownReset <= 0)
                {
                    sprintRemaining += Time.deltaTime;
                    if (sprintRemaining > sprintDuration)
                    {
                        sprintRemaining = sprintDuration;
                    }
                }
            }
        }
    }

    public void FixedUpdate()
    {
        // Handle movement logic
        if (fpc.playerCanMove)
        {
            Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            targetVelocity = fpc.transform.TransformDirection(targetVelocity);
            targetVelocity *= isSprinting ? sprintSpeed : walkSpeed;

            Vector3 velocity = rb.velocity;
            Vector3 velocityChange = (targetVelocity - velocity);
            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
            velocityChange.y = 0;

            rb.AddForce(velocityChange, ForceMode.VelocityChange);
        }

        // Check if grounded
        isGrounded = Physics.Raycast(fpc.transform.position, Vector3.down, fpc.playerHeight / 2 + 0.1f);
    }

    public void Jump()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpPower, rb.velocity.z);
        }
    }

    public void Crouch()
    {
        if (isCrouched)
        {
            rb.transform.localScale = originalScale;
        }
        else
        {
            rb.transform.localScale = new Vector3(originalScale.x, crouchHeight, originalScale.z);
        }
    }
}
