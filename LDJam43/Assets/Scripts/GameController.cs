using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public Vector2 mousePos = Vector2.zero;
    public float mouseClickRange;

    public GameObject TemplePanel;

    public bool isBuilding;

	void Update () {
        if (Input.GetMouseButtonDown(0) && !isBuilding)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D[] buildingS = Physics2D.OverlapCircleAll(mousePos, mouseClickRange);

            Collider2D building = null;

            foreach(Collider2D obj in buildingS)
            {
                if(obj.tag == "Temple")
                {
                    building = obj;
                }
            }

            if(building != null)
            {
                if(building.transform.GetComponent<Building>().buildingType == eBuildingTypes.Temple && building.transform.GetComponent<Building>().buildingPlaced)
                {
                    TemplePanel.SetActive(true);
                }
            }
        }
	}

    public void PleaseTheGods()
    {
        if(PopulationController.instance.amountOfVillagers >= 10)
        {
            // Kill of X% of the population
            // Play some sacrifice sound -> woula woula -> arggg
            PopulationController.instance.KillPercentOfPopulation(30);
            godController.instance.AddFavor(10); // Maybe % of max favor -> 35%
            godController.instance.AddConstantFavor(1);
            Invoke("RemoveSacrificeBonus", 30); //Remove the constant bonus from the sacrifice
        }
    }

    public void RemoveSacrificeBonus()
    {
        godController.instance.AddConstantFavor(-1);
    }

    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
    }
}
