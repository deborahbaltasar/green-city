using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ZonePlacementHelper : StructureModificationHelper
{
    Vector3 mapBottomLeftCorner;
    Vector3 startPoint;
    Vector3? previousEndPosition = null;
    bool startPositionAcquired = false;
    Queue<GameObject> gameObjectsToReuse = new Queue<GameObject>();
    private int structuresOldQuantity = 0;

    public ZonePlacementHelper(
        StructureRepository structureRepository,
        GridStructure grid,
        IPlacementManager placementManager,
        Vector3 mapBottomLeftCorner,
        IResourceManager resourceManager
    )
        : base(structureRepository, grid, placementManager, resourceManager)
    {
        this.mapBottomLeftCorner = mapBottomLeftCorner;
    }

    public override void PrepareStructureForModification(
        Vector3 inputPosititon,
        string structureName,
        StructureType structureType
    )
    {
        base.PrepareStructureForModification(inputPosititon, structureName, structureType);
        Vector3 gridPosition = grid.CalculateGridPosition(inputPosititon);
        if (startPositionAcquired == false && grid.IsCellTaken(gridPosition) == false)
        {
            startPoint = gridPosition;
            startPositionAcquired = true;
        }
        else if (startPositionAcquired && (previousEndPosition == null || ZoneCalculator.CheckIfPositionHasChanged(gridPosition, previousEndPosition.Value, grid))
        {
            PlaceNewZoneUpToPosition(gridPosition);
        }
    }

    private void PlaceNewZoneUpToPosition(Vector3 endPoint)
    {
        Vector3Int minPoint = Vector3Int.FloorToInt(startPoint);
        Vector3Int maxPoint = Vector3Int.FloorToInt(endPoint);

        ZoneCalculator.PrepareStartAndEndPoints(startPoint, endPoint, ref minPoint, ref maxPoint, mapBottomLeftCorner);
        HashSet<Vector3Int> newPositionsSet = grid.GetAllPositionsFromTo(minPoint, maxPoint);

        newPositionsSet = CalculateZoneCost(newPositionsSet);


        previousEndPosition = endPoint;
        ZoneCalculator.CalculateZone(newPositionsSet, structuresToBeModified, gameObjectsToReuse);

        foreach (var positionToPlaceStructure in newPositionsSet) {
            if (grid.IsCellTaken(positionToPlaceStructure))
            {
                continue;
            }
            GameObject structureToAdd = null;
            if (gameObjectsToReuse.Count > 0)
            {
                var gameObjectToReuse = gameObjectsToReuse.Dequeue();
                gameObjectToReuse.SetActive(true);
                structureToAdd = placementManager.MoveStructureOnTheMap(positionToPlaceStructure, gameObjectToReuse, structureData.prefab);
            }
            else
            {
                structureToAdd = GameObject.Instantiate(structureData.prefab);
            }
        }

    }

    private HashSet<Vector3Int> CalculateZoneCost(HashSet<Vector3Int> newPositionsSet)
    {
        resourceManager.AddMoney(structureData.placementCost * structuresOldQuantity);
        int numberOfZonesToPlace = resourceManager.HowManyStructuresCanIPlace(structureData.placementCost, newPositionsSet.Count);
        if (numberOfZonesToPlace < newPositionsSet.Count)
        {
            newPositionsSet = new HashSet<Vector3Int>(newPositionsSet.Take(numberOfZonesToPlace));
        }
        structuresOldQuantity = newPositionsSet.Count;
        resourceManager.SpendMoney(structureData.placementCost * structuresOldQuantity);

        return newPositionsSet;
    }

    public override void CancelModifications()
    {
        resourceManager.AddMoney(structureData.placementCost * structuresOldQuantity);
        base.CancelModifications();
        ResetZonePlacementHelper();
    }

    public override void ConfirmModifications()
    {
        base.ConfirmModifications();
        ResetZonePlacementHelper();
    }

    private void ResetZonePlacementHelper()
    {
        structuresOldQuantity = 0;
        placementManager.DestroyStructures(gameObjectsToReuse);
        gameObjectsToReuse.Clear();
        previousEndPosition = null;
        startPositionAcquired = false;
    }
}