using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {
    public float sensitivity;
    private Vector3 lastMousePosition;
    private bool hasClicked;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0))
        {
            if (hasClicked)
            {
                this.transform.position += (lastMousePosition - Camera.main.ScreenToWorldPoint(Input.mousePosition)) * sensitivity;
                lastMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
            else
            {
                lastMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                hasClicked = true;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            lastMousePosition = Vector3.zero;
            hasClicked = false;
        }
	}
}
