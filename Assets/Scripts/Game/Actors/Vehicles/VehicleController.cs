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

    [Header("Loaded Settings")]

    [ReadOnly]
    [Tooltip("Speed of vehicle camera movement X direction")]
    public float VehicleCameraSpeedX;


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

        //Create initial objects
        VehicleCameraSpeedX = SettingsController.Instance.GetValue<float>(VEHICLE_CAMERA_SPEED_X_NAME);
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

    internal void UpdateCameraRotation(float rotationX, float rotationY)
    {
        if(BackCamera != null && BackCamera.isActiveAndEnabled)
        {
            var rotationCalculated = rotationX * VehicleCameraSpeedX * _instance.UnityDeltaTime;
            BackCamera.transform.RotateAround(gameObject.transform.position, Vector3.up, rotationCalculated);
        }
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
