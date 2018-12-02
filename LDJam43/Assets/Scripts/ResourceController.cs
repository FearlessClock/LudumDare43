using UnityEngine;
using System.Collections;
using TMPro;

public class ResourceController : MonoBehaviour
{

    public static ResourceController instance;

    public int woodStoredAmount;
    public int goldStoredAmount;
    public int foodStoredAmount;

    public TextMeshProUGUI woodUI;
    public TextMeshProUGUI goldUI;
    public TextMeshProUGUI foodUI;

    public Animator woodAnim;
    public Animator goldAnim;
    public Animator foodAnim;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        UpdateResourceUI();
    }

    void Update()
    {
    }

    public bool HasEnoughResources(int woodNeeded, int goldNeeded, int foodNeeded)
    {
        if (woodNeeded <= woodStoredAmount && goldNeeded <= goldStoredAmount && foodNeeded <= foodStoredAmount)
        {
            UpdateResourceUI();
            return true;
        }
        return false;
    }

    public void UseResources(int woodNeeded, int goldNeeded, int foodNeeded)
    {
        if (woodNeeded <= woodStoredAmount && goldNeeded <= goldStoredAmount && foodNeeded <= foodStoredAmount)
        {
            woodStoredAmount -= woodNeeded;
            goldStoredAmount -= goldNeeded;
            foodStoredAmount -= foodNeeded;
            UpdateResourceUI();
        }
        else
        {
            throw new System.Exception("Not enough resources but we were too late to stop it...");
        }
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

        if(wood != 0)
        {
            woodAnim.SetTrigger("Bop");
        }
        if (gold != 0)
        {
            goldAnim.SetTrigger("Bop");
        }
        if (food != 0)
        {
            foodAnim.SetTrigger("Bop");
        }

        UpdateResourceUI();
    }
}
