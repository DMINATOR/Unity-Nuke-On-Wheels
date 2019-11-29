using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(VehicleControllerLocator))]
public class VehicleController : MonoBehaviour
{
    //Not exposed

    [Header("Constants")]
    [ReadOnly]
    [Tooltip("Logging source")]
    public static string LOG_SOURCE = "VehicleController";

    //Exposed

    [Header("Locator")]

    [Tooltip("Locator")]
    public VehicleControllerLocator Locator;



    [Header("Variables")]

    [Tooltip("Maximum Torque")]
    public float MaxMotorTorque;

    [Tooltip("Maximum Steering Angle")]
    public float MaxSteeringAngle;

    [Tooltip("Increases / Decreases Throttle")]
    [SerializeField]
    public InputButton ButtonThrottle;

    [Tooltip("Turns wheels Left / Right")]
    [SerializeField]
    public InputButton ButtonTurn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var throttle = MaxMotorTorque * Input.GetAxis(ButtonThrottle.KeyName);
        var angle = MaxSteeringAngle * Input.GetAxis(ButtonTurn.KeyName);

        foreach (var axle in Locator.Axles)
        {
            axle.ManualUpdate(angle, throttle);
        }
    }
}
