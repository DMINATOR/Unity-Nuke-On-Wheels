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

        ManualUpdate();
    }

    internal void ManualUpdate()
    {
        // Update visual representation of the wheel
        Vector3 position;
        Quaternion rotation;
        Locator.Collider.GetWorldPose(out position, out rotation);

        Locator.GameObjectPrefab.transform.position = position;
        Locator.GameObjectPrefab.transform.rotation = rotation;
    }
}
