using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DynamicWeaponComponentLocator))]
public class DynamicWeaponComponent : VehicleComponentBase
{
    [Header("Locator")]

    [Tooltip("Locator")]
    public DynamicWeaponComponentLocator Locator;
}
