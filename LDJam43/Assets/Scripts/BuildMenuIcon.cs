using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildMenuIcon : MonoBehaviour {
    public int iconID;
    public BuildMenuController buildMenuController;
    public ResourceController resourceController;
    public PopulationController populationController;
    public GameController gameController;
    private bool holdingBuilding;
    private GameObject heldBuilding;

    [Header("Cost of the building")]
    public int woodCost;
    public int goldCost;
    public int foodCost;
    public int villagerCost;

    public Camera currentCamera;
    public CameraMove cameraMove;

    public LayerMask buildingLayerMask;

    private Collider2D boxCollider;

    private AudioSource audioSource;
    public AudioClip buildSound;
    public AudioClip cantBuildSound;
    public AudioClip buildOnGroundSound;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        EventTrigger trigger = GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((data) => { OnPointerDown((PointerEventData)data); });
        trigger.triggers.Add(entry);

        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerUp;
        entry.callback.AddListener((data) => { OnPointerUp((PointerEventData)data); });
        trigger.triggers.Add(entry);

        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.Drag;
        entry.callback.AddListener((data) => { OnDrag((PointerEventData)data); });
        trigger.triggers.Add(entry);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (heldBuilding != null && holdingBuilding)
        {
            Vector3 pos = currentCamera.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;
            pos.x = Mathf.RoundToInt(pos.x);
            pos.y = Mathf.RoundToInt(pos.y);
            heldBuilding.transform.position = pos;
            if (boxCollider.IsTouchingLayers(buildingLayerMask))
            {
                heldBuilding.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.red;
            }
            else
            {
                heldBuilding.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;
            }

        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (resourceController.HasEnoughResources(woodCost, goldCost, foodCost) && populationController.HasSufficientWorkers(villagerCost))
        {
            audioSource.PlayOneShot(buildSound);
            resourceController.UseResources(woodCost, goldCost, foodCost);
            populationController.ConscriptPopulation(villagerCost);
            heldBuilding = buildMenuController.SpawnSelectedBuilding(iconID);
            boxCollider = heldBuilding.GetComponent<BoxCollider2D>();
            holdingBuilding = true;
            cameraMove.CanMove = false;
            gameController.isBuilding = true;
        }
        else
        {
            audioSource.PlayOneShot(cantBuildSound);
            cameraMove.CanMove = false;
            gameController.isBuilding = true;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (holdingBuilding)
        {
            Vector3 pos = currentCamera.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;
            pos.x = Mathf.RoundToInt(pos.x);
            pos.y = Mathf.RoundToInt(pos.y);
            if (boxCollider.IsTouchingLayers(buildingLayerMask))
            {
                audioSource.PlayOneShot(cantBuildSound);
                resourceController.AddResources(woodCost, goldCost, foodCost);
                populationController.freeVillager(villagerCost);
                Destroy(heldBuilding);
            }
            else
            {
                audioSource.PlayOneShot(buildOnGroundSound);
            }
            heldBuilding.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 5;
            heldBuilding.GetComponent<Building>().buildingPlaced = true;
            heldBuilding = null;
            holdingBuilding = false;
        }
        cameraMove.CanMove = true;
        gameController.isBuilding = false;
    }
}
