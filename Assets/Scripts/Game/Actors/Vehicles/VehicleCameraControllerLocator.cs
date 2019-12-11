using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleCameraControllerLocator : MonoBehaviour
{
    [Tooltip("Camera to rotate")]
    public Camera Camera;

    [Tooltip("Target object to rotate around")]
    public Transform TargetObject;
}
