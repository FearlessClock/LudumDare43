using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class TownHall : Building
{
    public static TownHall instance;

    public int townHallLevel;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI infoText;

    public float townHallLevelFavorLoseAmount;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        buildingPlaced = true;
    }

    void Start()
    {
        townHallLevel = 1;
        UpdateText();
    }

    public void UpgradeTownHall()
    {
        if(ResourceController.instance.HasEnoughResources((townHallLevel * 50), (townHallLevel * 50), (townHallLevel * 50)))
        {
            ResourceController.instance.UseResources((townHallLevel * 50), (townHallLevel * 50), (townHallLevel * 50));
            townHallLevel += 1;
            godController.instance.AddConstantFavor(townHallLevel * townHallLevelFavorLoseAmount);
            UpdateText();

            if (townHallLevel == 10)
            {
                GameController.instance.LoadScene("EndGame");
            }
        }
    }

    public void UpdateText()
    {
        levelText.text = "" + townHallLevel + "/10";
        infoText.text = "Please The Gods by upgrading your town hall ! Get to level 10 and . . . finish the game?\n" + (townHallLevel * 50) + " WOOD; " + (townHallLevel * 50) + " GOLD; " + (townHallLevel * 50) + " FOOD";
    }
}
