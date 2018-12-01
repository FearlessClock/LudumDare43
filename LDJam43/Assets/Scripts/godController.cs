using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum eGodAngerLevel { Happy, notImpressed, angry, furious}
public class godController : MonoBehaviour {
    public float favorLevel;
    public float favorGain;
    public ResourceController resourceController;

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
	// Use this for initialization
	void Start () {
        timer = timeTillNextEvent;
	}
	
	// Update is called once per frame
	void Update () {
        CalculateFavorGain();
        AddFavor(favorGain);
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
        float prosperity = (resourceController.foodStoredAmount + resourceController.woodStoredAmount + resourceController.goldStoredAmount)/3;

        favorGain = 4/(1 + Mathf.Exp(-prosperity/3)) - 4/2;
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
