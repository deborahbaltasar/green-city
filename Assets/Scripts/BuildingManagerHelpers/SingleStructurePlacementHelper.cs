using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleStructurePlacementHelper : StructureModificationHelper
{
    public SingleStructurePlacementHelper(
        StructureRepository structureRepository,
        GridStructure grid,
        IPlacementManager placementManager,
        IResourceManager resourceManager
    )
        : base(structureRepository, grid, placementManager, resourceManager) { }

    public override void PrepareStructureForModification(Vector3 inputPosititon, string structureName, StructureType structureType)
    {
        base.PrepareStructureForModification(inputPosititon, structureName, structureType);

        GameObject buildingPrefab = structureData.prefab;

        Vector3 gridPosition = grid.CalculateGridPosition(inputPosititon);
        var gridPositionInt = Vector3.floorToInt(gridPosition);

        if (grid.IsCellTaken(gridPositionInt) == false)
        {
            if (structuresToBeModified.ContainsKey(gridPositionInt))
            {
                resourceManager.AddMoney(structureData.placementCost)
                RevokeStructurePlacementAt(gridPositionInt);
            }
            else if (resourceManager.CanIBuyIt(structureData.placementCost))
            {
                PlaceNewStructureAt(buildingPrefab, gridPosition, gridPositionInt);
                resourceManager.SpendMoney(structureData.placementCost);
            }
        }
    }
}
