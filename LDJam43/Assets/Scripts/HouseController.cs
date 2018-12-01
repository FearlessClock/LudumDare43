using UnityEngine;
using System.Collections;

public class HouseController : MonoBehaviour
{
    public float timeTellNextBaby;
    private float timer;
    private PopulationController popCounter;
    // Use this for initialization
    void Start()
    {
        timer = timeTellNextBaby;
        popCounter = FindObjectOfType<PopulationController>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            timer = timeTellNextBaby * Random.Range(1f, 4f);
            popCounter.IncreasePopulation(1);
        }
    }
}
