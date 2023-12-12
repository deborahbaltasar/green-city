using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NullStructureSO : StructureBaseSO
{
    private void OnEnable()
    {
        buildingName = "Null Structure";
        prefab = null;
        placementCost = 0;
        upkeepCost = 0;
        income = 0;
    }
}
