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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
