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
            new Vector3( BoundsMax.x, 0, BoundsMax.z)
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
        Locator.DebugLineRendererCurrent.gameObject.SetActive(this == OpenWorldController.Instance.PlayerBlock);
    }

    public bool IsWithin(float unityX, float unityZ)
    {
        return unityX <= BoundsMax.x &&
              unityZ <= BoundsMax.z &&
              unityX >= BoundsMin.x &&
              unityZ >= BoundsMin.z;
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
