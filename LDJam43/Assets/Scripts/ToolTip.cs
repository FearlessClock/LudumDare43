using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ToolTip : MonoBehaviour {

    public TextMeshProUGUI typeText;
    public TextMeshProUGUI costText;
    public TextMeshProUGUI gainText;

    public void TurnOffPanel()
    {
        gameObject.SetActive(false);
    }

    public void ShowToolTip(eBuildingTypes type)
    {
        switch (type)
        {
            case eBuildingTypes.House:
                UpdateTextFields("House", "WOOD : 5\nGOLD : 5\nFOOD : 10", "Spanws people");
                break;

            case eBuildingTypes.Farm:
                UpdateTextFields("Farm", "WOOD : 5\nGOLD : 5\nFOOD : 10", "10 FOOD /s");
                break;

            case eBuildingTypes.Mine:
                UpdateTextFields("Mines", "WOOD : 5\nGOLD : 5\nFOOD : 10", "10 GOLD /s");
                break;

            case eBuildingTypes.WoodCutter:
                UpdateTextFields("WOOD Cutter", "WOOD : 5\nGOLD : 5\nFOOD : 10", "10 WOOD /s");
                break;

            case eBuildingTypes.Temple:
                UpdateTextFields("Temple", "WOOD : 5\nGOLD : 5\nFOOD : 10", "10 FAVOR /s");
                break;

            default:
                break;
        }
    }

    public void UpdateTextFields(string type, string cost, string gain)
    {
        typeText.text = type;
        costText.text = cost;
        gainText.text = gain;
    }
}
