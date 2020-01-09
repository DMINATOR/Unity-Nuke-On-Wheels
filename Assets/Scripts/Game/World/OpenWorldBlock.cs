﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenWorldBlock : MonoBehaviour
{
    [Header("Locator")]

    [Tooltip("Locator")]
    public OpenWorldBlockLocator Locator;

    [ReadOnly]
    [Tooltip("Current block position in the universe")]
    public long BlockX;

    [ReadOnly]
    [Tooltip("Current block position in the universe")]
    public long BlockZ;

    public void UpdatePosition(long blockX, long blockZ)
    {
        BlockX = blockX;
        BlockZ = blockZ;

        //make lines to match the size of the block
        Locator.DebugLineRenderer.SetPositions(new Vector3[]
        {
            new Vector3( 0, 0, 0),
            new Vector3( OpenWorldController.Instance.BlockSize, 0, 0),
            new Vector3( OpenWorldController.Instance.BlockSize, 0, OpenWorldController.Instance.BlockSize),
            new Vector3( 0, 0, OpenWorldController.Instance.BlockSize),
            new Vector3( 0, 0, 0)
        });

        Locator.DebugBlockName.color = UnityEngine.Color.white;
        Locator.DebugBlockName.text = $"{BlockX},{BlockZ}";

        Log.Instance.Info(OpenWorldController.LOG_SOURCE, $"Block [{BlockX}, {BlockZ}] Updated");
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
