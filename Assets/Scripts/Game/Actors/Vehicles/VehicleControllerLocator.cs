using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleControllerLocator : MonoBehaviour
{
    [Tooltip("Axles associated with this vehicle")]
    public VehicleComponentBase[] Axles;

    [Tooltip("Body parts with this vehicle")]
    public VehicleComponentBase[] Body;
}
