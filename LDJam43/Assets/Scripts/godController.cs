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
    public float maxFavorDrain;
    //TODO: Make the sacrifice favor gain be temporary
    public float favorTimeStep;
    private float favorTimer;
    public ResourceController resourceController;
    public PopulationController populationController;

    public float happyLevel;
    public float notImpressedLevel;
    public float angryLevel;
    public float furiousLevel;
    // Max favor = 100
    public GameObject blackDeathPrefab;
    public GameObject BigStormPrefab;
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

        CalculateFavorGain();
        AddFavor(favorGain);

        if (favorLevel <= furiousLevel)
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
            Vector2 cameraPos = Camera.main.transform.position;
            Vector3 throughCenter = Vector3.zero;
            switch (currentGodAngerLevel)
            {
                case eGodAngerLevel.Happy:
                    //TODO: Sing a nice song
                    break;
                case eGodAngerLevel.notImpressed:
                    //TODO: Set a building on fire
                    break;
                case eGodAngerLevel.angry:

                    GameObject storm = Instantiate<GameObject>(BigStormPrefab, this.transform);

                    BigStormController bsController = storm.GetComponent<BigStormController>();
                    //Start the black plague offscreen and get the point across from the screen
                    bsController.startingPosition = cameraPos + (UnityEngine.Random.insideUnitCircle.normalized * spawnRange);
                    throughCenter = (Camera.main.transform.position - bsController.startingPosition).normalized * spawnRange;
                    bsController.endPosition = throughCenter;
                    break;
                case eGodAngerLevel.furious:
                    
                    //Create the black plague
                    GameObject bpObj = Instantiate<GameObject>(blackDeathPrefab, this.transform);
                    
                    BlackPlagueController bpController = bpObj.GetComponent<BlackPlagueController>();
                    //Start the black plague offscreen and get the point across from the screen
                    bpController.startingPosition = cameraPos + (UnityEngine.Random.insideUnitCircle.normalized * spawnRange);
                    throughCenter = (Camera.main.transform.position - bpController.startingPosition).normalized * spawnRange; 
                    bpController.endPosition = throughCenter;
                    break;
                default:
                    break;
            }
        }
    }

    private void CalculateFavorGain()
    {
        float prosperity = (resourceController.foodStoredAmount + resourceController.woodStoredAmount + resourceController.goldStoredAmount + populationController.amountOfVillagers) /4;
        
        favorGain = -(Mathf.Log(prosperity/15f + 1)/4f) * maxFavorDrain + constantFavor;    //TODO: Change the equation to a more constant raise
    }

    public void AddConstantFavor(float amount)
    {
        constantFavor += amount;
    }

    public void AddFavor(float amount)
    {
        favorLevel += amount*Time.deltaTime;
        if(favorLevel > 100)
        {
            favorLevel = 100;
        }
        else if(favorLevel < 0)
        {
            favorLevel = 0;
        }
    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(Camera.main.transform.position, spawnRange);
    }
}
