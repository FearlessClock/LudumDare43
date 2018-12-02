using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eGodAngerLevel { Happy, notImpressed, angry, furious}

public class godController : MonoBehaviour {

    public static godController instance;

    public float favorLevel;
    public float favorGain;         //Favor gained/Lost from resources
    public float constantFavor;     //Favor gained from temples and sacrifices;
    //TODO: Make the sacrifice favor gain be temporary
    public float favorTimeStep;
    private float favorTimer;
    public ResourceController resourceController;
    public PopulationController populationController;

    public float happyLevel;
    public float notImpressedLevel;
    public float angryLevel;
    public float furiousLevel;
    public GameObject blackDeathPrefab;
    public float spawnRange;

    public eGodAngerLevel currentGodAngerLevel;

    public float randomChanceToDoSomething;
    public float timeTillNextEvent;
    private float timer;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    void Start () {
        timer = timeTillNextEvent;
        favorTimer = favorTimeStep;
	}
	
	// Update is called once per frame
	void Update () {
        favorTimer -= Time.deltaTime;
        if(favorTimer <= 0)
        {
            favorTimer = favorTimeStep;
            CalculateFavorGain();
            AddFavor(favorGain);
        }
		if(favorLevel <= furiousLevel)
        {
            currentGodAngerLevel = eGodAngerLevel.furious;
        }
        else if (favorLevel <= angryLevel)
        {
            currentGodAngerLevel = eGodAngerLevel.angry;
        }
        else if (favorLevel <= notImpressedLevel)
        {
            currentGodAngerLevel = eGodAngerLevel.notImpressed;
        }
        else if (favorLevel <= happyLevel)
        {
            currentGodAngerLevel = eGodAngerLevel.Happy;
        }
        timer -= Time.deltaTime;
        if(timer <= 0 && UnityEngine.Random.Range(0f, 1f) < randomChanceToDoSomething)
        {
            timer = timeTillNextEvent;
            switch (currentGodAngerLevel)
            {
                case eGodAngerLevel.Happy:
                    //TODO: Sing a nice song
                    break;
                case eGodAngerLevel.notImpressed:
                    //TODO: Set a building on fire
                    break;
                case eGodAngerLevel.angry:
                    //TODO: Send great wind storm
                    break;
                case eGodAngerLevel.furious:
                    //Create the black plague
                    GameObject bpObj = Instantiate<GameObject>(blackDeathPrefab, this.transform);
                    BlackPlagueController bpController = bpObj.GetComponent<BlackPlagueController>();
                    Vector2 cameraPos = Camera.main.transform.position;
                    //Start the black plague offscreen and get the point across from the screen
                    bpController.startingPosition = cameraPos + (UnityEngine.Random.insideUnitCircle.normalized * spawnRange);
                    Vector3 toCenter = (Camera.main.transform.position - bpController.startingPosition).normalized * spawnRange; 
                    bpController.endPosition = toCenter;
                    break;
                default:
                    break;
            }
        }
    }

    private void CalculateFavorGain()
    {
        float prosperity = (resourceController.foodStoredAmount + resourceController.woodStoredAmount + resourceController.goldStoredAmount + populationController.amountOfVillagers) /4;

        favorGain = -1/(1 + Mathf.Exp(-prosperity/3)) - 1/2 + constantFavor;
    }

    public void AddConstantFavor(float amount)
    {
        constantFavor += amount;
    }

    public void AddFavor(float amount)
    {
        favorLevel += amount;
    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(Camera.main.transform.position, spawnRange);
    }
}
