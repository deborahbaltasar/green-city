using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadPlacementModificationHelper : StructureModificationHelper
{
    public RoadPlacementModificationHelper(
        StructureRepository structureRepository,
        GridStructure grid,
        IPlacementManager placementManager,
        IResourceManager resourceManager
    )
        : base(structureRepository, grid, placementManager, resourceManager) { }
}
