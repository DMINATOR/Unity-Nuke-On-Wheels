using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleControllerLocator : MonoBehaviour
{
    [Tooltip("Axles associated with this vehicle")]
    public VehicleAxleComponent[] Axles;

    [Tooltip("Body part of this vehicle")]
    public VehicleComponentBase Body;

    [Tooltip("Cameras")]
    public Camera[] Cameras;
}
