using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopulationController : MonoBehaviour {
    public int amountOfVillagers;
    public int amountOfWorkingVillagers;
    public TextMeshProUGUI amountOfVillagersUI;
    private List<GameObject> villagers;
    public GameObject villager;
    public int maxAmountOfVillagers;

    public BuildingController buildingController;
	// Use this for initialization
	void Start () {
        villagers = new List<GameObject>();
        UpdateUI();
    }

    public void IncreasePopulation(int amount)
    {
        amountOfVillagers += amount;
        UpdateUI();
    }

    public void UpdateUI()
    {
        amountOfVillagersUI.text = amountOfVillagers.ToString();
    }
	
	// Update is called once per frame
	void Update () {
        if (amountOfVillagers > villagers.Count && villagers.Count < maxAmountOfVillagers)
        {
            GameObject building = buildingController.GetRandomBuilding(eBuildingTypes.House);
            if(building != null)
            {
                villagers.Add(Instantiate<GameObject>(villager, building.transform.position + Vector3.down, Quaternion.identity));
            }
        }
	}

    public bool HasSufficientWorkers(int amount)
    {
        if(amountOfVillagers - amountOfWorkingVillagers >= amount)
        {
            return true;
        }
        return false;
    }

    public void ConscriptPopulation(int amount)
    {
        if(amountOfWorkingVillagers + amount <= amountOfVillagers)
        {
            amountOfWorkingVillagers += amount;
        }
        else
        {
            throw new Exception("There weren't enough workers but we were too late...");
        }
    }
}
