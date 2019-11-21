using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleControllerLocator : MonoBehaviour
{
    [Tooltip("Axles associated with this vehicle")]
    public VehicleComponent[] Axles;

    [Tooltip("Body parts with this vehicle")]
    public VehicleComponent[] Body;
}
