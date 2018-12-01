using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public enum eBuildingTypes { House, Farm, Mine, WoodCutter }
public class BuildingController : MonoBehaviour
{
    List<GameObject> houses         = new List<GameObject>();
    List<GameObject> farms          = new List<GameObject>();
    List<GameObject> mines          = new List<GameObject>();
    List<GameObject> woodCutters    = new List<GameObject>();

    public void AddBuilding(GameObject building, eBuildingTypes buildingType)
    {
        switch (buildingType)
        {
            case eBuildingTypes.House:
                houses.Add(building);
                break;
            case eBuildingTypes.Farm:
                farms.Add(building);
                break;
            case eBuildingTypes.Mine:
                mines.Add(building);
                break;
            case eBuildingTypes.WoodCutter:
                woodCutters.Add(building);
                break;
            default:
                break;
        }
    }

    public GameObject GetRandomBuilding(eBuildingTypes buildingType)
    {
        switch (buildingType)
        {
            case eBuildingTypes.House:
                if(houses.Count > 0)
                {
                    return houses[UnityEngine.Random.Range(0, houses.Count)];
                }
                break;
            case eBuildingTypes.Farm:
                if (farms.Count > 0)
                {
                    return farms[UnityEngine.Random.Range(0, farms.Count)];
                }
                break;
            case eBuildingTypes.Mine:
                if (mines.Count > 0)
                {
                    return mines[UnityEngine.Random.Range(0, mines.Count)];
                }
                break;
            case eBuildingTypes.WoodCutter:
                if (woodCutters.Count > 0)
                {
                    return woodCutters[UnityEngine.Random.Range(0, woodCutters.Count)];
                }
                break;
        }

        return null;
    }
}
