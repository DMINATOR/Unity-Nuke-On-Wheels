using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// OpenWorld block declares a 2d space in the universe where objects will be created
/// </summary>
public class OpenWorldBlock : MonoBehaviour
{
    [Header("Locator")]

    [Tooltip("Locator")]
    public OpenWorldBlockLocator Locator;

    [Header("Game world position")]

    [ReadOnly]
    [Tooltip("Current block position in the universe")]
    public long BlockX;

    [ReadOnly]
    [Tooltip("Current block position in the universe")]
    public long BlockZ;

    [Header("Unity position")]

    [ReadOnly]
    [Tooltip("Current block position relative to the center")]
    public long BlockDeltaX;

    [ReadOnly]
    [Tooltip("Current block position relative to the center")]
    public long BlockDeltaZ;

    [ReadOnly]
    [Tooltip("Current block bound range min")]
    public Vector3 BoundsMin;

    [ReadOnly]
    [Tooltip("Current block bound range max")]
    public Vector3 BoundsMax;


    [ReadOnly]
    [Tooltip("Current status of the block")]
    public OpenWorldBlockStatus Status;

    [ReadOnly]
    [Tooltip("Quality level of the block")]
    public OpenWorldBlockQualityLevel QualityLevel;

    public void SetDeltaPosition(long blockDeltaX, long blockDeltaZ)
    {
        // Set current block unique position
        BlockDeltaX = blockDeltaX;
        BlockDeltaZ = blockDeltaZ;

        // Calculate block bounds
        BoundsMin.x = BlockDeltaX * OpenWorldController.Instance.BlockSize;
        BoundsMax.x = BlockDeltaX * OpenWorldController.Instance.BlockSize + OpenWorldController.Instance.BlockSize;

        BoundsMin.z = BlockDeltaZ * OpenWorldController.Instance.BlockSize;
        BoundsMax.z = BlockDeltaZ * OpenWorldController.Instance.BlockSize + OpenWorldController.Instance.BlockSize;

        this.gameObject.transform.position = new Vector3(BlockDeltaX * OpenWorldController.Instance.BlockSize, 0, BlockDeltaZ * OpenWorldController.Instance.BlockSize);

        UpdateDebugInformation();

        Log.Instance.Info(OpenWorldController.LOG_SOURCE, $"Set Delta [{BlockDeltaX}, {BlockDeltaZ}]");
    }

    public void LoadWithData(long blockX, long blockZ)
    {
        BlockX = blockX;
        BlockZ = blockZ;

        UpdateDebugInformation();

        Log.Instance.Info(OpenWorldController.LOG_SOURCE, $"Set Delta [{BlockDeltaX}, {BlockDeltaZ}]");
    }

    public void UpdateDebugInformation()
    {
        //make lines to match the size of the block
        Locator.DebugLineRendererEdge.SetPositions(new Vector3[]
        {
            new Vector3( BoundsMin.x, 0, BoundsMin.z),
            new Vector3( BoundsMax.x, 0, BoundsMin.z),
            new Vector3( BoundsMax.x, 0, BoundsMax.z),
            new Vector3( BoundsMin.x, 0, BoundsMax.z),
            new Vector3( BoundsMin.x, 0, BoundsMin.z)
        });

        Locator.DebugLineRendererCurrent.SetPositions(new Vector3[]
        {
            new Vector3( BoundsMin.x, 0, BoundsMin.z),
            new Vector3( BoundsMax.x, 0, BoundsMax.z),
            new Vector3( BoundsMax.x, 0, BoundsMin.z),
            new Vector3( BoundsMin.x, 0, BoundsMax.z)
        });

        switch (Status)
        {
            case OpenWorldBlockStatus.CREATED:
                Locator.DebugBlockDeltaText.color = UnityEngine.Color.yellow;
                Locator.DebugBlockGameText.color = UnityEngine.Color.yellow;
                break;

            case OpenWorldBlockStatus.LOADED:
                Locator.DebugBlockDeltaText.color = UnityEngine.Color.green;
                Locator.DebugBlockGameText.color = UnityEngine.Color.green;
                break;

            case OpenWorldBlockStatus.EXPIRED:
                Locator.DebugBlockDeltaText.color = UnityEngine.Color.red;
                Locator.DebugBlockGameText.color = UnityEngine.Color.red;
                break;

            default:
                Locator.DebugBlockDeltaText.color = UnityEngine.Color.white;
                Locator.DebugBlockGameText.color = UnityEngine.Color.white;
                break;
        }

        Locator.DebugBlockDeltaText.text = $"{BlockDeltaX},{BlockDeltaZ}";
        Locator.DebugBlockGameText.text = $"{BlockX},{BlockZ}";

        if (OpenWorldController.Instance.CenterBlock != null)
        {
            Color color;
            if (GetMinDistanceFromCenter() > OpenWorldController.Instance.BlockOutRescale)
            {
                // Outside of rescale and should re-center to the new position
                color = Color.red;
            }
            else if (this == OpenWorldController.Instance.PlayerBlock)
            {
                // Position of the player
                color = Color.green;
            }
            else
            {
                // Any other block
                color = Color.white;
            }

            Gradient gradient = new Gradient();
            gradient.SetKeys(
                new GradientColorKey[] { new GradientColorKey(color, 0.0f), new GradientColorKey(color, 1.0f) },
                new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(1.0f, 1.0f) }
            );
            Locator.DebugLineRendererCurrent.colorGradient = gradient;
        }

        //Locator.DebugLineRendererCurrent.gameObject.SetActive(this == OpenWorldController.Instance.PlayerBlock);
    }

    // Shifts block position towards specific direction in Unity coordinates
    public void Shift(long deltaBlockX, long deltaBlockZ)
    {
        var newDeltaBlockX = BlockDeltaX - deltaBlockX;
        var newDeltaBlockZ = BlockDeltaZ - deltaBlockZ;

        if ((GetMinDistanceFromCenterX() > OpenWorldController.BLOCKS_PER_X) ||
            (GetMinDistanceFromCenterZ() > OpenWorldController.BLOCKS_PER_Z) )
        {
            // Unload if it's outside of the range
            OnStatusChange(OpenWorldBlockStatus.EXPIRED);
        }

        SetDeltaPosition(newDeltaBlockX, newDeltaBlockZ);
    }

    public bool IsWithin(float unityX, float unityZ)
    {
        return unityX <= BoundsMax.x &&
              unityZ <= BoundsMax.z &&
              unityX >= BoundsMin.x &&
              unityZ >= BoundsMin.z;
    }

    public long GetMinDistanceFromCenter()
    {
        return Math.Max(GetMinDistanceFromCenterX(), GetMinDistanceFromCenterZ());
    }

    public long GetMinDistanceFromCenterX()
    {
        return Math.Abs(OpenWorldController.Instance.CenterBlock.BlockX - BlockX);
    }

    public long GetMinDistanceFromCenterZ()
    {
        return Math.Abs(OpenWorldController.Instance.CenterBlock.BlockZ - BlockZ);
    }

    // Triggered when origin enters this block
    public void OnEnter(OpenWorldOrigin origin)
    {
        if( GetMinDistanceFromCenter() > OpenWorldController.Instance.BlockOutRescale)
        {
            var blockCenter = (origin.CurrentBlock.BoundsMin + origin.CurrentBlock.BoundsMax) / 2.0f;
            var newLocation = blockCenter - origin.transform.position;

            // Origin moved outside the block bounds and we need to reset the world back to the center
            OpenWorldController.Instance.ResetWorldToCenter(this);

            var newBlockCenter = (origin.CurrentBlock.BoundsMin + origin.CurrentBlock.BoundsMax) / 2.0f;
            var newLocationFromBlockCenter = newBlockCenter - newLocation;

            // Translate to the same position in the new block
            //origin.transform.localPosition = new Vector3(0, origin.transform.position.y, 0); // Keep Y unchanged
            origin.transform.localPosition = new Vector3(newLocationFromBlockCenter.x, origin.transform.position.y, newLocationFromBlockCenter.z); // Keep Y unchanged
        }
    }

    public void OnStatusChange(OpenWorldBlockStatus newStatus)
    {
        switch(newStatus)
        {
            case OpenWorldBlockStatus.CREATED:
                break;

            case OpenWorldBlockStatus.LOADED:
                break;

            case OpenWorldBlockStatus.EXPIRED:
                break;

            default:
                throw new Exception($"Unknown status {newStatus}");
        }

        Status = newStatus;
    }

    // Triggers when origin exits this block
    public void OnExit(OpenWorldOrigin origin)
    {
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
