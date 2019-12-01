using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(VehicleControllerLocator))]
public class VehicleController : MonoBehaviour
{
    //Not exposed

    [Header("Constants")]
    [ReadOnly]
    [Tooltip("Logging source")]
    public static string LOG_SOURCE = "VehicleController";

    //Exposed

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
