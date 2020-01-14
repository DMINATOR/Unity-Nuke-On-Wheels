using System.Collections.Generic;


//SAVE GAME overloads
public partial class SaveGameData : DKAsset
{
    public List<SaveSlotInstance> SaveSlots;
}


[System.Serializable]
public class SaveSlotInstance
{
    // Indicates that this save slot is current
    public bool Current;

    // Position in the world map
    public long BlockX;
    public long BlockZ;

    // Info
    public DateTimeSerializer Created;
    public DateTimeSerializer Modified;
}

