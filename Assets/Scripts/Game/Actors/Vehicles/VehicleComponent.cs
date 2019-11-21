using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(VehicleComponentLocator))]
public class VehicleComponent : MonoBehaviour
{
    //Exposed

    [Header("Locator")]

    [Tooltip("Locator")]
    public VehicleComponentLocator Locator;

    [Header("Prefabs")]
    public GameObject GameObjectInstance;

}
