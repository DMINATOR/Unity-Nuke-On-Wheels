using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// World tracking, considered to be a center point of the world.
/// 
/// Can move world when traveled enough distance
/// </summary>
public class OpenWorldOrigin : MonoBehaviour
{
    [ReadOnly]
    [Tooltip("Current block position in the universe")]
    public long BlockX;

    [ReadOnly]
    [Tooltip("Current block position in the universe")]
    public long BlockZ;

    [ReadOnly]
    [Tooltip("Current position in Unity coordinates")]
    public float UnityX;

    [ReadOnly]
    [Tooltip("Current position in Unity coordinates")]
    public float UnityZ;


    [Tooltip("Event to call when OpenWorldPosition is translated to center")]
    public UnityEvent TranslateToCenterCallback;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
