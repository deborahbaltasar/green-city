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
    protected IResourceManager resourceManager;

    public StructureModificationHelper(
        StructureRepository structureRepository,
        GridStructure grid,
        IPlacementManager placementManager,
        IResourceManager resourceManager
    )
    {
        this.structureRepository = structureRepository;
        this.grid = grid;
        this.placementManager = placementManager;
        this.resourceManager = resourceManager;
        structureData = ScriptableObject.CreateInstance<NullStructureSO>();
    }

    public GameObject AccessStructureInDictionary(Vector3Int gridPosition)
    {
        var gridPositionInt = Vector3Int.FloorToInt(gridPosition);
        if (structuresToBeModified.ContainsKey(gridPositionInt))
        {
            return structuresToBeModified[gridPositionInt];
        }
        return null;
    }

    public virtual void CancelModifications()
    {
        placementManager.DestroyStructures(structuresToBeModified.Values);
        ResetHelpersData();
    }

    public virtual void PrepareStructureForModification(
        Vector3 inputPosititon,
        string structureName,
        StructureType structureType
    )
    {
        if (
            structureData.GetType() == typeof(NullStructureSO)
            && structureType != StructureType.None
        )
        {
            structureData = structureRepository.GetStructureByNameAndType(
                structureName,
                structureType
            );
        }
    }

    private void ResetHelpersData()
    {
        structuresToBeModified.Clear();
        structureData = ScriptableObject.CreateInstance<NullStructureSO>();
    }
}
