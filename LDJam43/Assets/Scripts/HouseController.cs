using UnityEngine;
using System.Collections;

public class HouseController : Building
{
    public int maxPeopleInTheHouse;
    private PopulationController popCounter;
    // Use this for initialization
    void Start()
    {
        popCounter = FindObjectOfType<PopulationController>();
        popCounter.IncreasePopulationSize(maxPeopleInTheHouse);
    }
}
