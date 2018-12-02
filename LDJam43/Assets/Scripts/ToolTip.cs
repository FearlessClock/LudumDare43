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
                UpdateTextFields("House", "Wood : 5\nGold : 5\nFood : 10", "Spanws people");
                break;

            case eBuildingTypes.Farm:
                UpdateTextFields("Farm", "Wood : 5\nGold : 5\nFood : 10", "10 food /s");
                break;

            case eBuildingTypes.Mine:
                UpdateTextFields("Mines", "Wood : 5\nGold : 5\nFood : 10", "10 gold /s");
                break;

            case eBuildingTypes.WoodCutter:
                UpdateTextFields("Wood Cutter Hut", "Wood : 5\nGold : 5\nFood : 10", "10 wood /s");
                break;

            case eBuildingTypes.Temple:
                UpdateTextFields("Temple", "Wood : 5\nGold : 5\nFood : 10", "10 favor /s");
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
