using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FixedWeaponComponentLocator))]
public class FixedWeaponComponent : MonoBehaviour
{
    [Header("Locator")]

    [Tooltip("Locator")]
    public FixedWeaponComponentLocator Locator;
}
