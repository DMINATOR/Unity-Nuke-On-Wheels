using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(OpenWorldBlockTerrainLocator))]
public class OpenWorldBlockTerrain : MonoBehaviour
{
    [Tooltip("Locator")]
    public OpenWorldBlockTerrainLocator Locator;




    [Header("Status")]

    [Tooltip("Indicates that geometry was generated")]
    public bool GeometryGenerated;

    [Tooltip("Currently active mesh that is rendered")]
    public MeshRenderer ActiveMesh;


    public void OnQualityLevelChange(OpenWorldBlockQualityLevel newLevel)
    {
        // Disable currently active mesh
        if (ActiveMesh != null)
        {
            ActiveMesh.enabled = false;
        }

        switch (newLevel)
        {
            case OpenWorldBlockQualityLevel.HIGH:
                // TODO - set to high
                ActiveMesh = Locator.Mesh;
                break;

            case OpenWorldBlockQualityLevel.MEDIUM:
                // TODO - set to medium
                ActiveMesh = Locator.Mesh;
                break;

            case OpenWorldBlockQualityLevel.LOW:
                // TODO - set to low
                ActiveMesh = Locator.Mesh;
                break;

            case OpenWorldBlockQualityLevel.NONE:
                ActiveMesh = null;
                break;

            default:
                throw new Exception($"Unknown quality level {newLevel}");
        }

        // Enable new mesh
        if (ActiveMesh != null)
        {
            ActiveMesh.enabled = true;
        }
    }

    public void GenerateGeometry(long BlockX, long BlockZ)
    {
        var seed = GameController.Instance.CurrentSaveInstance.Seed;

        if (GeometryGenerated)
        {
            UpdateExistingGeometry(seed, BlockX, BlockZ);
        }
        else
        {
            CreateInitialGeometry(seed, BlockX, BlockZ);
        }
    }


    private void CreateInitialGeometry(long seed, long BlockX, long BlockZ)
    {
        GeometryGenerated = true;
        Log.Instance.Info(OpenWorldController.LOG_SOURCE, $"Generate Terrain (initial, {seed}) [{BlockX}, {BlockZ}]");
    }

    private void UpdateExistingGeometry(long seed, long BlockX, long BlockZ)
    {
        Log.Instance.Info(OpenWorldController.LOG_SOURCE, $"Update Terrain (existing, {seed}) [{BlockX}, {BlockZ}]");
    }
}
