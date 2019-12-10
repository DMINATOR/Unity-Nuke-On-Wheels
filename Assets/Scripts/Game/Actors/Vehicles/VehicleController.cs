using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(VehicleControllerLocator))]
public class VehicleController : MonoBehaviour
{
    // Not exposed

    // Back camera that can be rotate, if located
    private Camera BackCamera = null;

    [Tooltip("Current Time Scale Instance assigned for this object")]
    [SerializeField]
    private TimeControlTimeScale _instance;

    [Header("Constants")]
    [ReadOnly]
    [Tooltip("Logging source")]
    public static string LOG_SOURCE = "VehicleController";

    //Exposed

    [Header("Settings")]

    [ReadOnly]
    [Tooltip("Settings to control speed of a vehicle camera X direction")]
    public SettingsConstants.Name VEHICLE_CAMERA_SPEED_X_NAME = SettingsConstants.Name.VEHICLE_CAMERA_SPEED_X;

    [ReadOnly]
    [Tooltip("Settings to control speed of a vehicle camera Y direction")]
    public SettingsConstants.Name VEHICLE_CAMERA_SPEED_Y_NAME = SettingsConstants.Name.VEHICLE_CAMERA_SPEED_Y;

    [Header("Loaded Settings")]

    [ReadOnly]
    [Tooltip("Speed of vehicle camera movement X direction")]
    public float VehicleCameraSpeedX;

    [ReadOnly]
    [Tooltip("Speed of vehicle camera movement Y direction")]
    public float VehicleCameraSpeedY;

    [Header("Locator")]

    [Tooltip("Locator")]
    public VehicleControllerLocator Locator;


    [Header("Variables")]

    [Tooltip("Maximum Torque")]
    public float MaxMotorTorque;

    [Tooltip("Maximum Steering Angle")]
    public float MaxSteeringAngle;

    // Start is called before the first frame update
    void Start()
    {
        _instance = TimeControlController.Instance.CreateTimeScaleInstance(this);
        BackCamera = Locator.Cameras.SingleOrDefault(c => c.gameObject.layer == LayerMask.NameToLayer("Camera_Back"));

        // Load settings
        VehicleCameraSpeedX = SettingsController.Instance.GetValue<float>(VEHICLE_CAMERA_SPEED_X_NAME);
        VehicleCameraSpeedY = SettingsController.Instance.GetValue<float>(VEHICLE_CAMERA_SPEED_Y_NAME);
    }

    internal void UpdateTime()
    {
        _instance.Update();
    }

    internal void ManualUpdate(float throttleInput, float angleInput)
    {
        var throttle = MaxMotorTorque * throttleInput;
        var angle = MaxSteeringAngle * angleInput;

        foreach (var axle in Locator.Axles)
        {
            axle.ManualUpdate(angle, throttle);
        }
    }

    public void OrbitCamera(Vector3 target, float y_rotate, float x_rotate)
    {
        Vector3 angles = BackCamera.transform.eulerAngles;
        angles.z = 0;
        BackCamera.transform.eulerAngles = angles;
        BackCamera.transform.RotateAround(target, Vector3.up, y_rotate);
        BackCamera.transform.RotateAround(target, Vector3.left, x_rotate);

        BackCamera.transform.LookAt(target);
    }

    public void OrbitCamera2(Vector3 target, float y_rotate, float x_rotate)
    {
        float yMinLimit = -20f;
        float yMaxLimit = 80f;

        float x = x_rotate;
        float y = y_rotate;

        y = ClampAngle(y, yMinLimit, yMaxLimit);

        Quaternion rotation = Quaternion.Euler(y, x, 0);

        Vector3 negDistance = new Vector3(0.0f, 0.0f, 5.0f);
        Vector3 position = rotation * negDistance + gameObject.transform.position;

        BackCamera.transform.rotation = rotation;
        BackCamera.transform.position = position;
    }

    internal void UpdateCameraRotation(float rotationX, float rotationY)
    {
        /*
        if(BackCamera != null && BackCamera.isActiveAndEnabled)
        {
            var rotationCalculatedX = rotationX * VehicleCameraSpeedX * _instance.UnityDeltaTime;
            var rotationCalculatedY = rotationY * VehicleCameraSpeedY * _instance.UnityDeltaTime;

            OrbitCamera2(gameObject.transform.position, rotationCalculatedX, rotationCalculatedY);
        }*/
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }

    internal void ToggleCamera()
    {
        var index = 0;

        // Find active and disable current camera
        foreach(var camera in Locator.Cameras)
        {
            if( camera.gameObject.activeSelf)
            {
                camera.gameObject.SetActive(false);
                break;
            }

            index++;
        }

        // Find next camera index to enable
        index = (index + 1) % Locator.Cameras.Length;

        Locator.Cameras[index].gameObject.SetActive(true);
    }
}
