using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(VehicleAxleComponentLocator))]
public class VehicleAxleComponent : VehicleComponentBase
{
    [Header("Locator")]

    [Tooltip("Locator")]
    public VehicleAxleComponentLocator Locator;
}
