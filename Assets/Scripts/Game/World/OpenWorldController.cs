using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public static int BLOCKS_PER_X = 4;

    [ReadOnly]
    [Tooltip("Blocks per vertical axis")]
    public static int BLOCKS_PER_Z = 4;


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
    [Tooltip("Center block in")]
    public OpenWorldBlock CenterBlock;

    [ReadOnly]
    [Tooltip("Block the player is currently in")]
    public OpenWorldBlock PlayerBlock;


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

        BlockSize = SettingsController.Instance.GetValue<int>(BLOCK_SIZE_SETTING_NAME);
        HalfBlockSize = BlockSize / 2;

        BlockOutRescale = SettingsController.Instance.GetValue<int>(BLOCK_OUT_RESCALE);

        CreateInitialBlocks();
    }

    // Start is called before the first frame update
    void Start()
    {
        // TODO - should take player position
        //LoadBlocks(5, 5);
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
                block.OnStatusChange( OpenWorldBlockStatus.CREATED );
                block.QualityLevel = OpenWorldBlockQualityLevel.NONE;
                block.SetDeltaPosition(x, z);

                Blocks[counter] = block;

                // Center
                if ( x == 0 && z == 0 )
                {
                    CenterBlock = block;
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
    public void LoadBlocks(long centerX, long centerZ)
    {
        var counter = 0;

        for (var x = -BLOCKS_PER_X; x <= BLOCKS_PER_X; x++)
        {
            for (var z = -BLOCKS_PER_Z; z <= BLOCKS_PER_Z; z++)
            {
                var block = Blocks[counter];

                // Load block with the game data
                block.OnStatusChange( OpenWorldBlockStatus.LOADED );
                block.LoadWithData( centerX + x, centerZ + z);

                counter++;
            }
        }
    }

    /// <summary>
    /// Translate blocks to the new position, keeping old blocks in place and create new instead
    /// </summary>
    /// <param name="newCenterX"></param>
    /// <param name="newCenterZ"></param>
    public void TranslateBlocks(long newCenterX, long newCenterZ)
    {
        var deltaBlockX = newCenterX - CenterBlock.BlockX;
        var deltaBlockZ = newCenterZ - CenterBlock.BlockZ;

        var counter = 0;

        // Find new center block first
        for (var x = -BLOCKS_PER_X; x <= BLOCKS_PER_X; x++)
        {
            for (var z = -BLOCKS_PER_Z; z <= BLOCKS_PER_Z; z++)
            {
                var block = Blocks[counter];

                // Pick new cnter
                if (block.BlockX == newCenterX && block.BlockZ == newCenterZ)
                {
                    // Shift center first relative to the old center
                    block.Shift(deltaBlockX, deltaBlockZ);
                    //block.SetDeltaPosition(0, 0);

                    // Set it as new center
                    CenterBlock = block;
                }

                counter++;
            }
        }

        // Correct blocks and shift them to the direction of world movement
        Stack<OpenWorldBlock> expiredBlocks = new Stack<OpenWorldBlock>();
        Dictionary<Tuple<long, long>, OpenWorldBlock> blocksCache = new Dictionary<Tuple<long, long>, OpenWorldBlock>();
        counter = 0;
        for (var x = -BLOCKS_PER_X; x <= BLOCKS_PER_X; x++)
        {
            for (var z = -BLOCKS_PER_Z; z <= BLOCKS_PER_Z; z++)
            {
                var block = Blocks[counter];

                if (block.BlockX == newCenterX && block.BlockZ == newCenterZ)
                {
                    // Don't do anything with center block
                }
                else
                {
                    block.Shift(deltaBlockX, deltaBlockZ);
                    //block.SetDeltaPosition(x, z);
                }

                // Remember expired blocks
                if( block.Status == OpenWorldBlockStatus.EXPIRED )
                {
                    expiredBlocks.Push(block);
                }
                else
                {
                    blocksCache[new Tuple<long,long>(block.BlockDeltaX, block.BlockDeltaZ)] = block;
                }

                counter++;
            }
        }

        // Re-calculate blocks array
        counter = 0;

        for (var x = -BLOCKS_PER_X; x <= BLOCKS_PER_X; x++)
        {
            for (var z = -BLOCKS_PER_Z; z <= BLOCKS_PER_Z; z++)
            {
                OpenWorldBlock block;
                if( !blocksCache.TryGetValue(new Tuple<long, long>(x, z), out block))
                {
                    // No block available here - get one that is expired
                    block = expiredBlocks.Pop();

                    block.SetDeltaPosition(x, z);
                    block.LoadWithData(newCenterX + x, newCenterZ + z);
                    block.OnStatusChange(OpenWorldBlockStatus.LOADED);
                }

                Blocks[counter] = block;

                counter++;
            }
        }

    }

    internal OpenWorldBlock FindBlock(float unityX, float unityZ)
    {
        return Blocks.SingleOrDefault(x => x.IsWithin(unityX, unityZ));
    }

    public void ResetWorldToCenter(OpenWorldBlock block)
    {
        Log.Instance.Info(LOG_SOURCE, "ResetWorldToCenter");

        TranslateBlocks(block.BlockX, block.BlockZ);
    }
}
