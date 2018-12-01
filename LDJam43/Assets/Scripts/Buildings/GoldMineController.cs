using UnityEngine;
using System.Collections;

public class GoldMineController : Building
{
    public int goldGiveAmount;
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
            resourceController.AddResources(0, goldGiveAmount, 0);
            timer = timeTillGive;
        }

    }
}
