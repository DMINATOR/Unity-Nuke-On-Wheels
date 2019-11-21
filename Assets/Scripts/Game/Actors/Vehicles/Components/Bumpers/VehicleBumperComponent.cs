using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(VehicleBumperComponentLocator))]
public class VehicleBumperComponent : VehicleComponentBase
{
    [Header("Locator")]

    [Tooltip("Locator")]
    public VehicleBumperComponentLocator Locator;
}
