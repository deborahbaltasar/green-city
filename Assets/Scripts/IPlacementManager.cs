using UnityEngine;

public interface IPlacementManager
{
    void CreateBuilding(Vector3 gridPosition, GridStructure grid);
    void RemoveBuilding(Vector3 gridPosition, GridStructure grid);
}
