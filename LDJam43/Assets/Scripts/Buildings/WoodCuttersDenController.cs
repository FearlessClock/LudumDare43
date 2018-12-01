using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodCuttersDenController : Building {
    public int woodGiveAmount;
    public float timeTillGive;
    private float timer;
	// Use this for initialization
	void Start () {
        timer = timeTillGive;

    }
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            resourceController.AddResources(woodGiveAmount, 0, 0);
            timer = timeTillGive;
        }
	}
}
