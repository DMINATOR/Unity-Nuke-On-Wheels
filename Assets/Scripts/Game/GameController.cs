using System.Linq;
using UnityEngine;

[RequireComponent(typeof(GameControllerLocator))]
public class GameController : MonoBehaviour
{
    SaveGameData _saveData;

    //Not exposed

    [Header("Constants")]
    [ReadOnly]
    [Tooltip("Logging source")]
    public static string LOG_SOURCE = "GameController";


    //Public instance to game controller
    public static GameController Instance = null;

    //Exposed

    [Header("Locator")]

    [Tooltip("Locator")]
    public GameControllerLocator Locator;

    /*
    [Header("Prefabs")]

    [Header("Settings")]

    [Header("Loaded Settings")]

    [Header("Variables")]

    [Header("Status")]
    */

    [Header("Input")]

    [Tooltip("Increases / Decreases Throttle")]
    [SerializeField]
    public InputButton VehicleButtonThrottle;

    [Tooltip("Turns wheels Left / Right")]
    [SerializeField]
    public InputButton VehicleButtonTurn;

    [Tooltip("Changes active camera")]
    [SerializeField]
    public InputButton VehicleToggleCamera;

    [Tooltip("Vehicle Camera rotation vertical")]
    [SerializeField]
    public InputButton VehicleRotateCameraX;

    [Tooltip("Vehicle Camera rotation horizontal")]
    [SerializeField]
    public InputButton VehicleRotateCameraY;

    [Header("Save Instance")]
    [Tooltip("Current Save Instance")]
    public SaveSlotInstance CurrentSaveInstance;

    private void LoadGameData()
    {
        Log.Instance.Info(GameController.LOG_SOURCE, $"Game Data Loading");

        _saveData = SaveGameController.Instance.Load();

        if( _saveData != null )
        {
            CurrentSaveInstance = _saveData.SaveSlots?.Where(x => x.Current == true).SingleOrDefault();
        }
        else
        {
            Log.Instance.Info(GameController.LOG_SOURCE, $"Game Data First time creation");
            _saveData = new SaveGameData();
        }

        if (CurrentSaveInstance == null)
        {
            CurrentSaveInstance = new SaveSlotInstance();
            CurrentSaveInstance.Current = true;

            if( _saveData.SaveSlots == null )
            {
                _saveData.SaveSlots = new System.Collections.Generic.List<SaveSlotInstance>();
            }

            _saveData.SaveSlots.Add(CurrentSaveInstance);
        }

        Log.Instance.Info(GameController.LOG_SOURCE, $"Game Data Loaded");
    }

    private void SaveGameData()
    {
        Log.Instance.Info(GameController.LOG_SOURCE, $"Game Data Saving");

        SaveGameController.Instance.Save(_saveData);

        Log.Instance.Info(GameController.LOG_SOURCE, $"Game Data Saved");
    }

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

    private void Start()
    {
        LoadGameData();

        StartGame();
    }

    void Update()
    {
        UpdateInputVehicle();
    }

    private void UpdateInputVehicle()
    {
        if( Locator.PlayerControllerVehicle != null )
        {
            Locator.PlayerControllerVehicle.UpdateTime();

            Locator.PlayerControllerVehicle.ManualUpdate(Input.GetAxis(VehicleButtonThrottle.KeyName), Input.GetAxis(VehicleButtonTurn.KeyName));

            if( Input.GetButtonUp(VehicleToggleCamera.KeyName))
            {
                Locator.PlayerControllerVehicle.ToggleCamera();
            }

            Locator.PlayerControllerVehicle.UpdateCameraRotation(Input.GetAxis(VehicleRotateCameraX.KeyName), Input.GetAxis(VehicleRotateCameraY.KeyName));
        }
    }

    private void StartGame()
    {
        // game starts here
    }
}
