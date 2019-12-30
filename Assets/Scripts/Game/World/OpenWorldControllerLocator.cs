using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenWorldControllerLocator : MonoBehaviour
{
    [Header("Prefabs")]

    [Tooltip("Contains currently active blocks")]
    public GameObject OpenWorldActiveBlocks;

    [Tooltip("Prefab to create OpenWorld blocks from")]
    public GameObject OpenWorldPrefab;

    [Tooltip("Current origin of the world from player perspective")]
    public OpenWorldOrigin PlayerOrigin;

}
