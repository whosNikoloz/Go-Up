using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstPersonController : MonoBehaviour
{
    private Rigidbody rb;
    public Camera playerCamera;

    public bool lockCursor = true;
    public bool crosshair = true;
    public Sprite crosshairImage;
    public Color crosshairColor = Color.white;
    private Image crosshairObject;

    public bool enableZoom = true;
    public bool holdToZoom = false;
    public KeyCode zoomKey = KeyCode.Mouse1;
    public float zoomFOV = 30f;
    public float zoomStepTime = 5f;

    public bool playerCanMove = true;
    public float walkSpeed = 5f;
    public float maxVelocityChange = 10f;

    public bool enableSprint = true;
    public bool unlimitedSprint = false;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public float sprintSpeed = 7f;
    public float sprintDuration = 5f;
    public float sprintCooldown = .5f;
    public float sprintFOV = 80f;
    public float sprintFOVStepTime = 10f;
    public bool useSprintBar = true;
    public bool hideBarWhenFull = true;
    public float sprintBarWidthPercent = .3f;
    public float sprintBarHeightPercent = .015f;

    public bool enableJump = true;
    public KeyCode jumpKey = KeyCode.Space;
    public float jumpPower = 5f;

    public bool enableCrouch = true;
    public bool holdToCrouch = true;
    public KeyCode crouchKey = KeyCode.LeftControl;
    public float crouchHeight = .75f;
    public float speedReduction = .5f;

    public bool enableHeadBob = true;
    public Transform joint;
    public float bobSpeed = 10f;
    public Vector3 bobAmount = new Vector3(.15f, .05f, 0f);

    public float mouseSensitivity = 100f;
    public float maxLookAngle = 45f;
    public float defaultFOV = 60f;
    public bool cameraCanMove = true;

    private CameraController cameraController;
    private PlayerMovement playerMovement;
    private HeadBob headBob;

    public bool CameraCanMove => cameraCanMove;
    public PlayerMovement PlayerMovement => playerMovement;

    // Expose playerHeight
    public float playerHeight = 1.8f;
    public float PlayerHeight => playerHeight;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        crosshairObject = GetComponentInChildren<Image>();

        // Ensure that crosshairObject is assigned
        if (crosshairObject == null)
        {
            Debug.LogError("Crosshair Object is not assigned.");
        }

        // Initialize CameraController
        cameraController = new CameraController(this, playerCamera, zoomKey, zoomFOV, zoomStepTime, crosshair, crosshairImage, crosshairColor);


        // Initialize PlayerMovement
        playerMovement = new PlayerMovement(
            this, rb, walkSpeed, maxVelocityChange, sprintKey, sprintSpeed, sprintDuration, sprintCooldown,
            useSprintBar, hideBarWhenFull, sprintBarWidthPercent, sprintBarHeightPercent,
            jumpKey, jumpPower, crouchKey, crouchHeight, speedReduction
        );

        // Initialize HeadBob
        headBob = new HeadBob(this, joint, bobSpeed, bobAmount, rb);

        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }


    private void Update()
    {
        cameraController.Update();
        playerMovement.Update();
        headBob.Update();

        if (enableJump && Input.GetKeyDown(jumpKey) && playerMovement.IsGrounded)
        {
            playerMovement.Jump();
        }

        if (enableCrouch)
        {
            if (Input.GetKeyDown(crouchKey) && !holdToCrouch)
            {
                playerMovement.Crouch();
            }

            if (Input.GetKeyDown(crouchKey) && holdToCrouch)
            {
                playerMovement.IsCrouched = false;
                playerMovement.Crouch();
            }
            else if (Input.GetKeyUp(crouchKey) && holdToCrouch)
            {
                playerMovement.IsCrouched = true;
                playerMovement.Crouch();
            }
        }
    }

    private void FixedUpdate()
    {
        playerMovement.FixedUpdate();
    }
}
