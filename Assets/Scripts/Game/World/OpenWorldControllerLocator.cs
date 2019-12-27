using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenWorldControllerLocator : MonoBehaviour
{
    [Header("Prefabs")]

    [Tooltip("Prefab to contain cache of OpenWorld blocks")]
    public GameObject OpenWorldBlocksCachePrefab;

    [Tooltip("Current origin of the world from player perspective")]
    public OpenWorldOrigin PlayerOrigin;

}
