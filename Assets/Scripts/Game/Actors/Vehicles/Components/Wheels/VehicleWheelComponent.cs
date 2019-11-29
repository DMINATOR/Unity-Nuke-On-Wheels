using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(VehicleWheelComponentLocator))]
public class VehicleWheelComponent : VehicleComponentBase
{
    [Header("Locator")]

    [Tooltip("Locator")]
    public VehicleWheelComponentLocator Locator;

    internal void ManualUpdate(float angle, float throttle)
    {
        Locator.Collider.steerAngle = angle;
        Locator.Collider.motorTorque = throttle;

        if (Locator.Collider.transform.childCount == 0)
        {
            return;
        }

        Transform visualWheel = Locator.Collider.transform.GetChild(0);

        Vector3 position;
        Quaternion rotation;
        Locator.Collider.GetWorldPose(out position, out rotation);

        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;
    }
}
