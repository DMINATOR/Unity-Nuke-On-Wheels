using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// OpenWorld block declares a 2d space in the universe where objects will be created
/// </summary>
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

    public void UpdatePosition(long blockX, long blockZ)
    {
        // Set current block unique position
        BlockX = blockX;
        BlockZ = blockZ;

        // Calculate block bounds
        BoundsMin.x = BlockDeltaX * OpenWorldController.Instance.BlockSize;
        BoundsMax.x = BlockDeltaX * OpenWorldController.Instance.BlockSize + OpenWorldController.Instance.BlockSize;

        BoundsMin.z = BlockDeltaZ * OpenWorldController.Instance.BlockSize;
        BoundsMax.z = BlockDeltaZ * OpenWorldController.Instance.BlockSize + OpenWorldController.Instance.BlockSize;

        UpdateDebugInformation();

        Log.Instance.Info(OpenWorldController.LOG_SOURCE, $"Block [{BlockX}, {BlockZ}] -> [{BlockDeltaX}, {BlockDeltaZ}] Updated");
    }

    void UpdateDebugInformation()
    {
        //make lines to match the size of the block
        Locator.DebugLineRenderer.SetPositions(new Vector3[]
        {
            new Vector3( BoundsMin.x, 0, BoundsMin.z),
            new Vector3( BoundsMax.x, 0, BoundsMin.z),
            new Vector3( BoundsMax.x, 0, BoundsMax.z),
            new Vector3( BoundsMin.x, 0, BoundsMax.z),
            new Vector3( BoundsMin.x, 0, BoundsMin.z)
        });

        switch(Status)
        {
            case OpenWorldBlockStatus.CREATED:
                Locator.DebugBlockName.color = UnityEngine.Color.yellow;
                break;

            case OpenWorldBlockStatus.LOADED:
                Locator.DebugBlockName.color = UnityEngine.Color.green;
                break;

            case OpenWorldBlockStatus.EXPIRED:
                Locator.DebugBlockName.color = UnityEngine.Color.red;
                break;

            default:
                Locator.DebugBlockName.color = UnityEngine.Color.white;
                break;
        }

        Locator.DebugBlockName.text = $"{BlockX},{BlockZ}";
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
