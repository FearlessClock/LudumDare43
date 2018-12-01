using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildMenuController : EventTrigger {
    public GameObject[] buildings;
    public eBuildingTypes[] buildingTypes;
    public BuildingController buildingController;
    
    public GameObject SpawnSelectedBuilding(int selectedBuildingId)
    {
        Debug.Log("" + selectedBuildingId);
        GameObject building = Instantiate<GameObject>(buildings[selectedBuildingId], Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
        buildingController.AddBuilding(building, buildingTypes[selectedBuildingId]);
        building.transform.Translate(0, 0, 10);
        building.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 10;
        return building;
    }
}
