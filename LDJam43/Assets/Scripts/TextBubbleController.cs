using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextBubbleController : MonoBehaviour {
    public string[] texts;
    public int currentText;
    private Coroutine typingCoroutine;
    public TextMeshProUGUI bubbleUI;
	// Use this for initialization
	void Start () {
        StartCoroutine("TypeText");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void NextTextBubble()
    {
        currentText++;
        if(currentText < texts.Length)
        {
            if(typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
                typingCoroutine = null;
            }
            bubbleUI.text = "";
            typingCoroutine = StartCoroutine("TypeText");
        }
    }

    public IEnumerator TypeText()
    {
        bubbleUI.text = "";
        for (int i = 0; i < texts[currentText].Length; i++)
        {
            bubbleUI.text += texts[currentText][i].ToString();
            yield return 1;
        }
    }
}
