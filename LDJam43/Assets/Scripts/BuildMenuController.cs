using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMenuController : MonoBehaviour {
    public GameObject[] buildings;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SpawnSelectedBuilding(int selectedBuildingId)
    {
        GameObject building = Instantiate<GameObject>(buildings[selectedBuildingId], Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
        building.transform.Translate(0, 0, 10);
    }
}
