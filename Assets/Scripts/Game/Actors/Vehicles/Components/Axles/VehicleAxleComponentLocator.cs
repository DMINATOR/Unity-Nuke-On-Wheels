using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleAxleComponentLocator : VehicleComponentLocatorBase
{
    [Header("Prefabs")]

    [Tooltip("Wheel Left Component")]
    public VehicleWheelComponent WheelLeftComponent;

    [Tooltip("Wheel Right Component")]
    public VehicleWheelComponent WheelRightComponent;
}
