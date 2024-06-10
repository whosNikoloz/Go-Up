using UnityEngine;
using UnityEngine.UI;

public class CameraController
{
    private FirstPersonController fpc;
    private Camera playerCamera;
    private KeyCode zoomKey;
    private float zoomFOV;
    private float zoomStepTime;
    private bool crosshair;
    private Sprite crosshairImage;
    private Color crosshairColor;
    private Image crosshairObject;
    private bool isZoomed = false;
    private float yaw = 0.0f;
    private float pitch = 0.0f;

    public CameraController(FirstPersonController fpc, Camera playerCamera, KeyCode zoomKey, float zoomFOV, float zoomStepTime, bool crosshair, Sprite crosshairImage, Color crosshairColor)
    {
        this.fpc = fpc;
        this.playerCamera = playerCamera;
        this.zoomKey = zoomKey;
        this.zoomFOV = zoomFOV;
        this.zoomStepTime = zoomStepTime;
        this.crosshair = crosshair;
        this.crosshairImage = crosshairImage;
        this.crosshairColor = crosshairColor;

        crosshairObject = playerCamera.GetComponentInChildren<Image>();
        if (crosshair)
        {
            crosshairObject.sprite = crosshairImage;
            crosshairObject.color = crosshairColor;
        }
        else
        {
            crosshairObject.gameObject.SetActive(false);
        }
    }

    public void Update()
    {
        if (fpc.CameraCanMove)
        {
            yaw = fpc.transform.localEulerAngles.y + Input.GetAxis("Mouse X") * fpc.mouseSensitivity;
            pitch -= fpc.mouseSensitivity * Input.GetAxis("Mouse Y");
            pitch = Mathf.Clamp(pitch, -fpc.maxLookAngle, fpc.maxLookAngle);

            fpc.transform.localEulerAngles = new Vector3(0, yaw, 0);
            playerCamera.transform.localEulerAngles = new Vector3(pitch, 0, 0);
        }

        if (fpc.enableZoom)
        {
            if (Input.GetKeyDown(zoomKey) && !fpc.holdToZoom && !fpc.PlayerMovement.IsSprinting)
            {
                isZoomed = !isZoomed;
            }

            if (fpc.holdToZoom && !fpc.PlayerMovement.IsSprinting)
            {
                isZoomed = Input.GetKey(zoomKey);
            }

            if (isZoomed)
            {
                playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, zoomFOV, zoomStepTime * Time.deltaTime);
            }
            else if (!isZoomed && !fpc.PlayerMovement.IsSprinting)
            {
                playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, fpc.defaultFOV, zoomStepTime * Time.deltaTime);
            }
        }
    }
}
