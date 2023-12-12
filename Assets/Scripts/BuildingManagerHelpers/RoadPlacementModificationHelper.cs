using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonePlacementHelper : StructureModificationHelper
{
    public ZonePlacementHelper(
        StructureRepository structureRepository,
        GridStructure grid,
        IPlacementManager placementManager,
        ResourceManager resourceManager
    )
        : base(structureRepository, grid, placementManager, resourceManager) { }
}
