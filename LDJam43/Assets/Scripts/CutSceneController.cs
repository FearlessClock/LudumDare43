using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ECutscene { ZoomIn, IdleLightning, ZoomOut}

public class CutSceneController : MonoBehaviour {

    public ECutscene currentCutScene;

    // Lightning scene
    public GameObject lightning;

    public GameObject lightningSpawnPositionsHolder;
    private Vector2[] lightningSpawnPositions;

    public float timeBTWLightning;
    private float currentTimeBTWLightning;

    void Awake () {
        currentCutScene = ECutscene.ZoomIn;

        currentTimeBTWLightning = 0;
        lightningSpawnPositions = new Vector2[lightningSpawnPositionsHolder.transform.childCount];

        for (int i = 0; i < lightningSpawnPositions.Length; i++)
        {
            lightningSpawnPositions[i] = lightningSpawnPositionsHolder.transform.GetChild(i).position;
        }
    }
	
	// Update is called once per frame
	void Update () {
        switch (currentCutScene)
        {
            case ECutscene.ZoomIn:
                break;
            case ECutscene.IdleLightning:
                SpawnLightning();
                break;
            case ECutscene.ZoomOut:
                break;
            default:
                break;
        }
    }

    public void SpawnLightning()
    {
        currentTimeBTWLightning -= Time.deltaTime;
        if (currentTimeBTWLightning < 0)
        {
            Vector2 randPos = lightningSpawnPositions[Random.Range(0, lightningSpawnPositions.Length)];
            Instantiate(lightning, randPos, Quaternion.identity);

            currentTimeBTWLightning = timeBTWLightning * Random.Range(0.6f, 1.2f);
        }
    }

    public void GoToNextCutScene()
    {
        currentCutScene = (ECutscene)((int)currentCutScene + 1);
    }
}
