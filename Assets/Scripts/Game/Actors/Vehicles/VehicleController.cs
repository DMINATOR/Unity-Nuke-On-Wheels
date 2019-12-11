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

    [Header("Locator")]

    [Tooltip("Locator")]
    public VehicleControllerLocator Locator;


    [Header("Variables")]

    [Tooltip("Maximum Torque")]
    public float MaxMotorTorque;

    [Tooltip("Maximum Steering Angle")]
    public float MaxSteeringAngle;

    [Tooltip("Current camera distance")]
    public float VehicleCameraDistance;

    // Start is called before the first frame update
    void Start()
    {
        _instance = TimeControlController.Instance.CreateTimeScaleInstance(this);
        BackCamera = Locator.Cameras.SingleOrDefault(c => c.gameObject.layer == LayerMask.NameToLayer("Camera_Back"));
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
