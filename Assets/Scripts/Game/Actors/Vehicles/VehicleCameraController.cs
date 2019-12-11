using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(VehicleCameraControllerLocator))]
public class VehicleCameraController : MonoBehaviour
{
    // Not exposed

    [Header("Locator")]

    [Tooltip("Locator")]
    public VehicleCameraControllerLocator Locator;

    //Exposed

    [Header("Settings")]

    [ReadOnly]
    [Tooltip("Settings to control speed of a vehicle camera X direction")]
    public SettingsConstants.Name VEHICLE_CAMERA_SPEED_X_NAME = SettingsConstants.Name.VEHICLE_CAMERA_SPEED_X;

    [ReadOnly]
    [Tooltip("Settings to control speed of a vehicle camera Y direction")]
    public SettingsConstants.Name VEHICLE_CAMERA_SPEED_Y_NAME = SettingsConstants.Name.VEHICLE_CAMERA_SPEED_Y;

    [ReadOnly]
    [Tooltip("Settings to control Minimum limit for vehicle Y axis")]
    public SettingsConstants.Name VEHICLE_CAMERA_MIN_LIMIT_Y_NAME = SettingsConstants.Name.VEHICLE_CAMERA_MIN_LIMIT_Y;

    [ReadOnly]
    [Tooltip("Settings to control Maximum limit for vehicle Y axis")]
    public SettingsConstants.Name VEHICLE_CAMERA_MAX_LIMIT_Y_NAME = SettingsConstants.Name.VEHICLE_CAMERA_MAX_LIMIT_Y;

    [ReadOnly]
    [Tooltip("Settings to control Minimum limit for camera to zoom into the object")]
    public SettingsConstants.Name VEHICLE_CAMERA_MIN_DISTANCE_NAME = SettingsConstants.Name.VEHICLE_CAMERA_MIN_DISTANCE;

    [ReadOnly]
    [Tooltip("Settings to control Minimum limit for camera to zoom into the object")]
    public SettingsConstants.Name VEHICLE_CAMERA_MAX_DISTANCE_NAME = SettingsConstants.Name.VEHICLE_CAMERA_MAX_DISTANCE;

    [Header("Loaded Settings")]

    [Header("Camera settings")]

    [ReadOnly]
    [Tooltip("Speed of vehicle camera movement X direction")]
    public float VehicleCameraSpeedX;

    [ReadOnly]
    [Tooltip("Speed of vehicle camera movement Y direction")]
    public float VehicleCameraSpeedY;

    [ReadOnly]
    [Tooltip("Min limit for vehicle Y axis")]
    private float VehicleCameraMinLimitY;

    [ReadOnly]
    [Tooltip("Max limit for vehicle Y axis")]
    private float VehicleCameraMaxLimitY;

    [ReadOnly]
    [Tooltip("Minimum limit for camera to zoom into the object")]
    private float VehicleCameraMinDistance;

    [ReadOnly]
    [Tooltip("Maximum limit for camera to zoom into the object")]
    private float VehicleCameraMaxDistance;

    [Header("Input")]

    [Tooltip("Vehicle Camera rotation vertical")]
    [SerializeField]
    public InputButton VehicleRotateCameraX;

    [Tooltip("Vehicle Camera rotation horizontal")]
    [SerializeField]
    public InputButton VehicleRotateCameraY;

    [Tooltip("Vehicle Camera zoom in-out")]
    [SerializeField]
    public InputButton VehicleZoomCamera;

    [Header("Status")]

    [ReadOnly]
    [Tooltip("Currently distance")]
    public float Distance = 5.0f;

    [ReadOnly]
    [Tooltip("Currently rotation X")]
    public float X = 0.0f;

    [ReadOnly]
    [Tooltip("Currently rotation Y")]
    public float Y = 0.0f;

    // Use this for initialization
    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        X = angles.y;
        Y = angles.x;

        var rigidbody = GetComponent<Rigidbody>();

        // Make the rigid body not change rotation
        if (rigidbody != null)
        {
            rigidbody.freezeRotation = true;
        }

        // Load settings
        VehicleCameraSpeedX = SettingsController.Instance.GetValue<float>(VEHICLE_CAMERA_SPEED_X_NAME);
        VehicleCameraSpeedY = SettingsController.Instance.GetValue<float>(VEHICLE_CAMERA_SPEED_Y_NAME);
        VehicleCameraMinLimitY = SettingsController.Instance.GetValue<float>(VEHICLE_CAMERA_MIN_LIMIT_Y_NAME);
        VehicleCameraMaxLimitY = SettingsController.Instance.GetValue<float>(VEHICLE_CAMERA_MAX_LIMIT_Y_NAME);
        VehicleCameraMinDistance = SettingsController.Instance.GetValue<float>(VEHICLE_CAMERA_MIN_DISTANCE_NAME);
        VehicleCameraMaxDistance = SettingsController.Instance.GetValue<float>(VEHICLE_CAMERA_MAX_DISTANCE_NAME);
    }

    void LateUpdate()
    {
        UpdateCameraRotation(
            Input.GetAxis(VehicleRotateCameraX.KeyName),
            Input.GetAxis(VehicleRotateCameraY.KeyName),
            Input.GetAxis(VehicleZoomCamera.KeyName));
    }

    internal void UpdateCameraRotation(float rotationX, float rotationY, float zoom)
    {
        if (Locator.TargetObject)
        {
            X += Input.GetAxis("Mouse X") * VehicleCameraSpeedX * Distance;
            Y -= Input.GetAxis("Mouse Y") * VehicleCameraSpeedY;

            Y = ClampAngle(Y, VehicleCameraMinLimitY, VehicleCameraMaxLimitY);

            Quaternion rotation = Quaternion.Euler(Y, X, 0);

            Distance = Mathf.Clamp(Distance - Input.GetAxis("Mouse ScrollWheel") * 5, VehicleCameraMinDistance, VehicleCameraMaxDistance);

            Vector3 negDistance = new Vector3(0.0f, 0.0f, -Distance);
            Vector3 position = rotation * negDistance + Locator.TargetObject.position;

            transform.rotation = rotation;
            transform.position = position;
        }
    }
    
    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}
