using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FavorBar : MonoBehaviour {

    private float initScaleX;
    private float currentScaleX;

    private float maxFavor;
    private float currentFavor;

    private void Awake()
    {
        initScaleX = transform.localScale.x;
        currentScaleX = initScaleX;

        maxFavor = 100;
        currentFavor = maxFavor;
    }
    
    void Update () {
        UpdateBar();
	}
    
    public void UpdateBar()
    {
        currentFavor = godController.instance.favorLevel;
        switch (godController.instance.currentGodAngerLevel)
        {
            case eGodAngerLevel.Happy:
                gameObject.GetComponent<Image>().color = Color.cyan;// new Color(139f, 250f, 255f);
                break;

            case eGodAngerLevel.notImpressed:
                gameObject.GetComponent<Image>().color = Color.yellow;// new Color(250f, 150f, 30f);
                break;

            case eGodAngerLevel.angry:
                gameObject.GetComponent<Image>().color = new Color(255f, 0, 0);
                break;

            case eGodAngerLevel.furious:
                gameObject.GetComponent<Image>().color = new Color(0f, 0, 0);
                break;

            default:
                break;
        }

        currentScaleX = (currentFavor / maxFavor) * initScaleX;

        Vector3 temp = transform.localScale;
        temp.x = currentScaleX;

        transform.localScale = temp;
    }
}
