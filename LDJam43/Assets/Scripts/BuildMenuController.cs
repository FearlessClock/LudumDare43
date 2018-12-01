using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildMenuController : EventTrigger {
    public GameObject[] buildings;
    
    public GameObject SpawnSelectedBuilding(int selectedBuildingId)
    {
        GameObject building = Instantiate<GameObject>(buildings[selectedBuildingId], Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
        building.transform.Translate(0, 0, 10);
        building.GetComponent<SpriteRenderer>().sortingOrder = 10;
        return building;
    }
}
