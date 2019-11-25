using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BodyComponentLocator))]
public class BodyComponent : VehicleComponentBase
{
    [Header("Locator")]

    [Tooltip("Locator")]
    public BodyComponentLocator Locator;
}
