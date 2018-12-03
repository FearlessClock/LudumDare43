using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public Vector2 mousePos = Vector2.zero;
    public float mouseClickRange;

    public GameObject TemplePanel;
    public GameObject TownHallPanel;
    public GameObject fadePanel;

    public float favorGainForSacrifice;

    public bool isBuilding;
    private AudioSource source;
    public AudioClip sacrificeClip;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    void Update () {
        if (Input.GetMouseButtonDown(0) && !isBuilding)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D[] collisions = Physics2D.OverlapCircleAll(mousePos, mouseClickRange);

            Collider2D building = null;

            foreach(Collider2D obj in collisions)
            {
                switch (obj.tag)
                {
                    case "Temple":
                        building = obj;
                        break;

                    case "TownHall":
                        building = obj;
                        break;

                    case "Wood":
                        ResourceController.instance.AddResources(15, 0, 0);
                        Destroy(obj.gameObject);
                        break;

                    case "Gold":
                        ResourceController.instance.AddResources(0, 15, 0);
                        Destroy(obj.gameObject);
                        break;

                    default:
                        break;
                }
            }

            if(building != null)
            {
                if(building.transform.GetComponent<Building>().buildingType == eBuildingTypes.Temple && building.transform.GetComponent<Building>().buildingPlaced)
                {
                    TemplePanel.SetActive(true);
                }
                if (building.transform.GetComponent<Building>().buildingType == eBuildingTypes.TownHall && building.transform.GetComponent<Building>().buildingPlaced)
                {
                    TownHallPanel.SetActive(true);
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
            source.PlayOneShot(sacrificeClip);
            PopulationController.instance.KillPercentOfPopulation(30);
            godController.instance.AddFavor(10); // Maybe % of max favor -> 35%
            godController.instance.AddConstantFavor(favorGainForSacrifice);
            Invoke("RemoveSacrificeBonus", 15); //Remove the constant bonus from the sacrifice
        }
    }

    public void RemoveSacrificeBonus()
    {
        godController.instance.AddConstantFavor(-favorGainForSacrifice);
    }

    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine("GoToNextScene", sceneName);
    }

    IEnumerator GoToNextScene(string sceneName)
    {
        fadePanel.GetComponent<Animator>().SetTrigger("FadeIn");
        yield return new WaitForSeconds(10f / 60f);
        SceneManager.LoadScene(sceneName);
    }
}
