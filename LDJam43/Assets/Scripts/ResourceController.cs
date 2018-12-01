using UnityEngine;
using System.Collections;
using TMPro;

public class ResourceController : MonoBehaviour
{
    public int woodStoredAmount;
    public int goldStoredAmount;
    public int foodStoredAmount;

    public TextMeshProUGUI woodUI;
    public TextMeshProUGUI goldUI;
    public TextMeshProUGUI foodUI;
    // Use this for initialization
    void Start()
    {
        UpdateResourceUI();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public bool UseResources(int woodNeeded, int goldNeeded, int foodNeeded)
    {
        if (woodNeeded <= woodStoredAmount && goldNeeded <= goldStoredAmount && foodNeeded <= foodStoredAmount)
        {
            woodStoredAmount -= woodNeeded;
            goldStoredAmount -= goldNeeded;
            foodStoredAmount -= foodNeeded;
            UpdateResourceUI();
            return true;
        }
        return false;
    }

    public void UpdateResourceUI()
    {
        woodUI.text = woodStoredAmount.ToString();
        goldUI.text = goldStoredAmount.ToString();
        foodUI.text = foodStoredAmount.ToString();
    }

    public void AddResources(int wood, int gold, int food)
    {
        woodStoredAmount += wood;
        goldStoredAmount += gold;
        foodStoredAmount += food;
        UpdateResourceUI();
    }
}
