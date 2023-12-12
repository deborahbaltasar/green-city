using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureDemolitionHelper : StructureModificationHelper
{
    Dictionary<Vector3Int, GameObject> roadToDemolish = new Dictionary<Vector3Int, GameObject>();

    public StructureDemolitionHelper(
        StructureRepository structureRepository,
        GridStructure grid,
        IPlacementManager placementManager,
        IResourceManager resourceManager
    )
        : base(structureRepository, grid, placementManager, resourceManager) { }

    public override void CancelModifications()
    {
        foreach (var structure in structuresToBeModified)
        {
            resourceManager.AddMoney(resourceManager.demolitionPrice);
        }

        this.placementManager.PlaceStructureOnTheMap(structuresToBeModified.Values);
        structuresToBeModified.Clear();
    }

    public override void PrepareStructureForModification(
        Vector3 inputPosititon,
        string structureName,
        StructureType structureType
    )
    {
        base.PrepareStructureForModification(inputPosititon, structureName, structureType);
        Vector3 gridPosition = grid.CalculateGridPosition(inputPosititon);
        if (grid.IsCellTaken(gridPosition))
        {
            var gridPositionInt = Vector3Int.FloorToInt(gridPosition);
            var structure = grid.GetStructureFromTheGrid(gridPositionInt);

            if (structuresToBeModified.ContainsKey(gridPositionInt))
            {
                resourceManager.AddMoney(resourceManager.demolitionPrice);
                RevokeStructurePlacementAt(gridPositionInt, structure);
            }
            else if (resourceManager.CanIBuyIt(resourceManager.demolitionPrice))
            {
                AddStructureForDemolition(gridPositionInt, structure);
                resourceManager.SpendMoney(resourceManager.demolitionPrice);
            }
        }
    }
}
