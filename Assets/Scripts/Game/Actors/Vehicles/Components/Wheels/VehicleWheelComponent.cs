using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(VehicleWheelComponentLocator))]
public class VehicleWheelComponent : VehicleComponentBase
{
    [Header("Locator")]

    [Tooltip("Locator")]
    public VehicleWheelComponentLocator Locator;
}
