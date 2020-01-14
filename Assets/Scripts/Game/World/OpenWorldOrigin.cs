using System;
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

    [Header("Status")]

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

    [ReadOnly]
    [Tooltip("Current block the vehicle is in")]
    public OpenWorldBlock CurrentBlock;

    [ReadOnly]
    [Tooltip("Previous block the vehicle was in")]
    public OpenWorldBlock PreviousBlock;

    [Header("Events")]

    [Tooltip("Event to call when OpenWorldPosition is translated to center")]
    public UnityEvent TranslateToCenterCallback;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UnityX = transform.position.x;
        UnityZ = transform.position.z;

        if( CurrentBlock != null )
        {
            if( !CurrentBlock.IsWithin(UnityX, UnityZ))
            {
                // Moved out of the block - find next block
                PreviousBlock = CurrentBlock;

                CurrentBlock = OpenWorldController.Instance.FindBlock(UnityX, UnityZ);
                OpenWorldController.Instance.PlayerBlock = CurrentBlock;

                PreviousBlock.UpdateDebugInformation();
                CurrentBlock.UpdateDebugInformation();
            }
            // Else - still within the block
        }
        // Else - no block is currently active, skip
    }

    public void TeleportTo(long blockX, long blockZ)
    {
        BlockX = blockX;
        BlockZ = blockZ;

        if( CurrentBlock == null )
        {
            // First time load - no block was assigned, use center
            CurrentBlock = OpenWorldController.Instance.CenterBlock;
            PreviousBlock = OpenWorldController.Instance.CenterBlock;
            OpenWorldController.Instance.LoadBlocks(BlockX, BlockZ);
        }
    }
}
