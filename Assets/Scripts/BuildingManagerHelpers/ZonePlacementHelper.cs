using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonePlacementHelper : StructureModificationHelper
{
    public ZonePlacementHelper(
        StructureRepository structureRepository,
        GridStructure grid,
        IPlacementManager placementManager,
        IResourceManager resourceManager
    )
        : base(structureRepository, grid, placementManager, resourceManager) { }

    public override void PrepareStructureForModification(
        Vector3 inputPosititon,
        string structureName,
        StructureType structureType
    )
    {
        base.PrepareStructureForModification(inputPosititon, structureName, structureType);
    }
}
