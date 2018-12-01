using UnityEngine;
using System.Collections;

public class FarmController : Building
{
    public int foodGiveAmount;
    public float timeTillGive;
    private float timer;

    // Use this for initialization
    void Start()
    {
        timer = timeTillGive;

    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            resourceController.AddResources(0, 0, foodGiveAmount);
            timer = timeTillGive;
        }

    }
}
