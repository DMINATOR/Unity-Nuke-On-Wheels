using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(OpenWorldControllerLocator))]
public class OpenWorldController : MonoBehaviour
{
    [Header("Constants")]
    [ReadOnly]
    [Tooltip("Logging source")]
    public static string LOG_SOURCE = "OpenWorld";

    [ReadOnly]
    [Tooltip("Blocks per horizontal axis")]
    public static int BLOCKS_PER_X = 2;

    [ReadOnly]
    [Tooltip("Blocks per vertical axis")]
    public static int BLOCKS_PER_Z = 2;


    //Public instance to game controller
    public static OpenWorldController Instance = null;

    //Exposed

    [Header("Locator")]

    [Tooltip("Locator")]
    public OpenWorldControllerLocator Locator;

    [Header("Settings")]

    [ReadOnly]
    [Tooltip("Settings to load BLOCK_SIZE value")]
    public SettingsConstants.Name BLOCK_SIZE_SETTING_NAME = SettingsConstants.Name.BLOCK_SIZE;


    [ReadOnly]
    [Tooltip("Settings to load BLOCK_OUT_RESCALE value")]
    public SettingsConstants.Name BLOCK_OUT_RESCALE = SettingsConstants.Name.BLOCK_OUT_RESCALE;

    [Header("Loaded Settings")]

    [ReadOnly]
    [Tooltip("Half Block Size")]
    public int HalfBlockSize;

    [ReadOnly]
    [Tooltip("Current size of the block in Unity units (loaded from Settings)")]
    public int BlockSize;

    [ReadOnly]
    [Tooltip("Resets position back to center after leaving block ranges (loaded from Settings)")]
    public int BlockOutRescale;


    [Header("Status")]


    [ReadOnly]
    [Tooltip("Currently loaded and active blocks")]
    //These are loaded initially and re-used
    public OpenWorldBlock[] Blocks = null;

    [ReadOnly]
    [Tooltip("Current block the player is in")]
    public OpenWorldBlock CurrentBlock;


    private void Awake()
    {
        //Create instance
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        BlockSize = SettingsController.Instance.GetValue<int>(BLOCK_SIZE_SETTING_NAME);
        HalfBlockSize = BlockSize / 2;

        BlockOutRescale = SettingsController.Instance.GetValue<int>(BLOCK_OUT_RESCALE);

        CreateInitialBlocks();

        // TODO - should take player position
        LoadBlocks(5, 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Creates initial set of blocks that will be cached and re-used
    /// </summary>
    public void CreateInitialBlocks()
    {
        Blocks = new OpenWorldBlock[(BLOCKS_PER_X * 2 + 1) * (BLOCKS_PER_Z * 2 + 1)];

        var counter = 0;

        for( var x = -BLOCKS_PER_X; x <= BLOCKS_PER_X; x++)
        {
            for(var z = -BLOCKS_PER_Z; z <= BLOCKS_PER_Z; z++)
            {
                var gameObject = Instantiate(Locator.OpenWorldPrefab, new Vector3(x * BlockSize, 0, z * BlockSize), Quaternion.identity, this.Locator.OpenWorldActiveBlocks.gameObject.transform);
                var block = gameObject.GetComponent<OpenWorldBlock>();

                // Set initial values
                block.Status = OpenWorldBlockStatus.CREATED;
                block.QualityLevel = OpenWorldBlockQualityLevel.NONE;
                block.SetDeltaPosition(x, z);

                Blocks[counter] = block;

                // Center
                if ( x == 0 && z == 0 )
                {
                    CurrentBlock = block;
                }

                counter++;
            }
        }
    }

    /// <summary>
    /// Loads blocks with the following center position
    /// </summary>
    /// <param name="centerX"></param>
    /// <param name="centerZ"></param>
    public void LoadBlocks(int centerX, int centerZ)
    {
        var counter = 0;

        for (var x = -BLOCKS_PER_X; x <= BLOCKS_PER_X; x++)
        {
            for (var z = -BLOCKS_PER_Z; z <= BLOCKS_PER_Z; z++)
            {
                var block = Blocks[counter];

                // Load block with the game data
                block.Status = OpenWorldBlockStatus.LOADED;
                block.LoadWithData( centerX + x, centerZ + z);

                counter++;
            }
        }
    }

    public void ResetWorldToCenter()
    {
        Log.Instance.Info(LOG_SOURCE, "ResetWorldToCenter");

        var transform = Locator.PlayerOrigin.transform;

        Locator.PlayerOrigin.transform.localPosition = new Vector3(0, transform.localPosition.y, 0); // Keep Y unchanged
    }
}
