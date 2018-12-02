using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopulationController : MonoBehaviour {

    public static PopulationController instance;

    public int amountOfVillagers;               //Amount of villagers currently active
    public int amountOfWorkingVillagers;        //Amount of villagers currently working
    private int populationSize;                 //Currently available population.
    public TextMeshProUGUI amountOfVillagersUI;
    private List<GameObject> SpawnedVillagers;         //List of villagers instantiated
    public GameObject villager;
    public int maxAmountOfVisibleVillagers;     //The max amount of villagers that can be spawned

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
        SpawnedVillagers = new List<GameObject>();
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

    /// <summary>
    /// Kills off a certain percentage of villagers
    /// </summary>
    /// <param name="percentage"></param>
    public void KillPercentOfPopulation(int percentage)
    {
        int amountToKill = Mathf.CeilToInt(amountOfVillagers * (percentage / 100f));
        for (int i = 0; i < amountToKill; i++)
        {
            if(SpawnedVillagers.Count > 0)
            {
                int index = UnityEngine.Random.Range(0, SpawnedVillagers.Count);
                Destroy(SpawnedVillagers[index]);
                SpawnedVillagers.RemoveAt(index);
            }
        }
        //TODO: Check if buildings stop working when removing villagers
        amountOfVillagers -= amountToKill;
        UpdateUI();
    }

    public void UpdateUI()
    {
        amountOfVillagersUI.text = SpawnedVillagers.Count + "/" + amountOfVillagers;
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
        if (amountOfVillagers > SpawnedVillagers.Count && SpawnedVillagers.Count < maxAmountOfVisibleVillagers)
        {
            GameObject building = buildingController.GetRandomBuilding(eBuildingTypes.House);
            if(building != null)
            {
                SpawnedVillagers.Add(Instantiate<GameObject>(villager, building.transform.position + Vector3.down, Quaternion.identity));
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
