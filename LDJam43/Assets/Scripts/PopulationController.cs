using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopulationController : MonoBehaviour {

    public static PopulationController instance;

    public int amountOfVillagers;
    public int amountOfWorkingVillagers;
    private int populationSize;
    public TextMeshProUGUI amountOfVillagersUI;
    private List<GameObject> villagers;
    public GameObject villager;
    public int maxAmountOfVillagers;

    [Header("Population creation")]
    public float timeTillNextBaby;
    private float babyTimer;

    public BuildingController buildingController;

    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    void Start () {
        villagers = new List<GameObject>();
        populationSize = 1;
        babyTimer = timeTillNextBaby;
        UpdateUI();
    }

    public void IncreasePopulationSize(int amount)
    {
        populationSize += amount;
        UpdateUI();
    }

    public void AddVillagerToCounter(int amount)
    {
        amountOfVillagers += amount;
        UpdateUI();
    }

    public void KillPercentOfPopulation(int percentage)
    {
        float amountToKill = Mathf.RoundToInt(villagers.Count * (percentage / 100));
        for (int i = 0; i < amountToKill; i++)
        {
            DestroyImmediate(villagers[UnityEngine.Random.Range(0, villagers.Count)]);
        }
    }

    public void UpdateUI()
    {
        amountOfVillagersUI.text = villagers.Count + "/" + amountOfVillagers;
    }
	
	// Update is called once per frame
	void Update () {
        babyTimer -= Time.deltaTime;
        if(babyTimer <= 0)
        {
            babyTimer = timeTillNextBaby;
            if(amountOfVillagers < populationSize)
            {
                AddVillagerToCounter(1);
            }
        }
        if (amountOfVillagers > villagers.Count && villagers.Count < maxAmountOfVillagers)
        {
            GameObject building = buildingController.GetRandomBuilding(eBuildingTypes.House);
            if(building != null)
            {
                villagers.Add(Instantiate<GameObject>(villager, building.transform.position + Vector3.down, Quaternion.identity));
                UpdateUI();
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
