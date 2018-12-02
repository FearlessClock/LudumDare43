using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextBubbleController : MonoBehaviour {

    public static TextBubbleController instance;

    public string[] texts;
    public int currentText;
    private Coroutine typingCoroutine;
    public TextMeshProUGUI bubbleUI;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    void Start () {
        StartCoroutine("TypeText");
	}

    public void NextTextBubble()
    {
        currentText++;
        if (currentText < texts.Length)
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
        int lastText = currentText;
        bubbleUI.text = "";
        for (int i = 0; currentText < texts.Length && i < texts[currentText].Length; i++)
        {
            if (currentText == lastText)
            {
                bubbleUI.text += texts[currentText][i].ToString();
                yield return 1;
            }
            else
            {
                break;
            }
        }
    }

    public bool ReachedEndOfDialog()
    {
        return currentText == texts.Length;
    }
}
