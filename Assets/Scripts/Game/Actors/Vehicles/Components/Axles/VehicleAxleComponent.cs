using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(VehicleAxleComponentLocator))]
public class VehicleAxleComponent : VehicleComponentBase
{
    //Not exposed

    //Exposed

    [Header("Locator")]

    [Tooltip("Locator")]
    public VehicleAxleComponentLocator Locator;


    [Header("Variables")]

    [Tooltip("Indicates if this axle is used by motor")]
    public bool Motor;

    [Tooltip("Indicates if this Axle steers wheels")]
    public bool Steering;


    [Header("Status")]

    [ReadOnly]
    [Tooltip("Target throttle to apply to Axle")]
    public float TargetThrottle = 0;

    [ReadOnly]
    [Tooltip("Target Angle value applied to the wheels")]
    public float TargetAngle = 0;

    public void ManualUpdate(float angle, float throttle)
    {
        if( Motor || Steering )
        {
            if( Steering)
            {
                TargetAngle = angle;
            }

            if( Motor )
            {
                TargetThrottle = throttle;
            }
        }

        if (Locator.WheelLeftComponent != null)
        {
            Locator.WheelLeftComponent?.ManualUpdate(TargetAngle, TargetThrottle);
        }

        if (Locator.WheelRightComponent != null)
        {
            Locator.WheelRightComponent?.ManualUpdate(TargetAngle, TargetThrottle);
        }
    }
}
