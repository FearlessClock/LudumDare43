using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum ECutscene { ZoomIn, IdleLightning, ZoomOut}

public class CutSceneController : MonoBehaviour {

    public ECutscene currentCutScene;
    private bool doCutsceneOnce = true;
    public GameObject fadePanel;

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
	
	void Update () {
        switch (currentCutScene)
        {
            case ECutscene.ZoomIn:
                if (doCutsceneOnce)
                {
                    doCutsceneOnce = false;
                }
                break;

            case ECutscene.IdleLightning:
                CheckEndOfDialog();
                SpawnLightning();
                break;

            case ECutscene.ZoomOut:
                if(doCutsceneOnce)
                {
                    LoadScene("Main scene");
                    doCutsceneOnce = false;
                }
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
        doCutsceneOnce = true;
    }

    public void CheckEndOfDialog()
    {
        if(TextBubbleController.instance.ReachedEndOfDialog())
        {
            GoToNextCutScene();
        }
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
