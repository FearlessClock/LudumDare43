using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMove : MonoBehaviour {
    public float sensitivity;
    private Vector3 lastMousePosition;
    private bool hasClicked;
    [HideInInspector]
    public bool CanMove;
	// Use this for initialization
	void Start () {
        CanMove = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (hasClicked || (!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButton(0) && CanMove))
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

        if (CanMove && Input.GetMouseButtonUp(0) || hasClicked && Input.GetMouseButtonUp(0))
        {
            lastMousePosition = Vector3.zero;
            hasClicked = false;
        }
	}
}
