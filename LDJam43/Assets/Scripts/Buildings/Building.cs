using UnityEngine;
using System.Collections;

public enum EBuildingType { House, TownHall, Farm, Mine, WoodCutterHut, Temple }

public class Building : MonoBehaviour
{
    public ResourceController resourceController;
    public EBuildingType buildingType;

    //TODO: Click on the building to open a menu
    // Use this for initialization
    void Awake()
    {
        resourceController = FindObjectOfType<ResourceController>();
        if(resourceController == null)
        {
            Debug.Log("The resource controller doesn't exist");
        }
    }


}
