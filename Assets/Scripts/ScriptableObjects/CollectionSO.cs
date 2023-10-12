using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New collection", menuName = "CityBuilder/CollectionSO")]
public class CollectionSO : ScriptableObject
{
    public List<SingleStructureBaseSO> singleStructureList;
    public List<ZoneStructureSO> zonesList;
}