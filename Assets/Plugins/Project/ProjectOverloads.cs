﻿using System.Collections.Generic;


//SAVE GAME overloads
public partial class SaveGameData : DKAsset
{
    public List<SaveSlotInstance> SaveSlots;
}


[System.Serializable]
public class SaveSlotInstance
{
    //Indicates that this save slot is current
    public bool Current;

    //Info
    public DateTimeSerializer Created;
    public DateTimeSerializer Modified;
}

