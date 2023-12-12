using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StructureModificationFactory
{
    private StructureModificationHelper singleStructurePlacementHelper;
    private StructureModificationHelper zonePlacementHelper;
    private StructureModificationHelper structureDemolitionHelper;
    private StructureModificationHelper roadPlacementModificationHelper;

    public static void PrepareFactory(
        StructureRepository structureRepository,
        GridStructure grid,
        IPlacementManager placementManager,
        ResourceManager resourceManager
    )
    {
        singleStructurePlacementHelper = new SingleStructurePlacementHelper(
            structureRepository,
            grid,
            placementManager,
            resourceManager
        );
        zonePlacementHelper = new ZonePlacementHelper(
            structureRepository,
            grid,
            placementManager,
            resourceManager
        );
        structureDemolitionHelper = new StructureDemolitionHelper(
            structureRepository,
            grid,
            placementManager,
            resourceManager
        );
        roadPlacementModificationHelper = new RoadPlacementModificationHelper(
            structureRepository,
            grid,
            placementManager,
            resourceManager
        );
    }
}
