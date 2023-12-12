using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager
{
    GridStructure grid;
    PlacementManager placementManager;
    StructureRepository structureRepository;
    StructureModificationHelper helper;

    //Dictionary<Vector3Int, GameObject> structuresToBeModified = new Dictionary<Vector3Int, GameObject>();

    public BuildingManager(
        int cellSize,
        int width,
        int length,
        PlacementManager placementManager,
        ResourceManager resourceManager
    )
    {
        this.grid = new GridStructure(cellSize, width, length);
        this.placementManager = placementManager;
        StructureModificationFactory.PrepareFactory(
            structureRepository,
            grid,
            placementManager,
            resourceManager
        );
    }

    public void PlaceStructureAt(Vector3 inputPosition)
    {
        Vector3 gridPosition = grid.CalculateGridPosition(inputPosition);
        if (grid.IsCellTaken(gridPosition) == false)
        {
            placementManager.CreateBuilding(gridPosition, grid);
        }
    }

    public void RemoveBuildingAt(Vector3 inputPosition)
    {
        Vector3 gridPosition = grid.CalculateGridPosition(inputPosition);
        if (grid.IsCellTaken(gridPosition))
        {
            placementManager.RemoveBuilding(gridPosition, grid);
        }
    }

    public IEnumerable<StructureBaseSO> GetAllStructures()
    {
        return grid.GetAllStructures();
    }
}
