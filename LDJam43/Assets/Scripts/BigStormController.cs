using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigStormController : MonoBehaviour {

    public Vector3 startingPosition;
    public Vector3 endPosition;
    private Vector3 direction;

    private BuildingController buildingController;
    private PopulationController popController;
    public float speed;
    private bool killed;

    public GameObject explosionPrefab;

    // Use this for initialization
    void Start ()
    {
        killed = false;
        direction = endPosition - startingPosition;
        direction.Normalize();
        this.transform.position = startingPosition;
        popController = PopulationController.instance;
        buildingController = BuildingController.instance;

    }
	
	// Update is called once per frame
	void Update () {

        this.transform.position += direction * speed * Time.deltaTime;
        if (Vector3.Distance(this.transform.position, endPosition) < 1 && !killed)
        {
            speed = 0;
            Destroy(this.gameObject);
            popController.KillPercentOfPopulation(30);
            if(UnityEngine.Random.Range(0f, 1f) > 0.3f)
            {
                Array type = eBuildingTypes.GetValues(typeof(eBuildingTypes));
                eBuildingTypes buildingType = (eBuildingTypes)type.GetValue(UnityEngine.Random.Range(0, type.Length));
                GameObject building = buildingController.GetRandomBuilding(buildingType);
                if(building != null)
                {
                    Instantiate<GameObject>(explosionPrefab, building.transform.position, Quaternion.identity);
                    buildingController.RemoveBuilding(buildingType, building);
                }
                else
                {
                    Debug.Log("There were no buildings to destroy");
                }
            }
            killed = true;
        }
    }
}
