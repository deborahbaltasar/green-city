using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureModificationHelper
{
    protected Dictionary<Vector3Int, GameObject> structuresToBeModified =
        new Dictionary<Vector3Int, GameObject>();
    protected readonly StructureRepository structureRepository;
    protected readonly GridStructure grid;
    protected readonly IPlacementManager placementManager;
    protected StructureBaseSO structureData;
    protected ResourceManager resourceManager;

    public StructureModificationHelper(
        StructureRepository structureRepository,
        GridStructure grid,
        IPlacementManager placementManager,
        ResourceManager resourceManager
    )
    {
        this.structureRepository = structureRepository;
        this.grid = grid;
        this.placementManager = placementManager;
        this.resourceManager = resourceManager;
    }
}
