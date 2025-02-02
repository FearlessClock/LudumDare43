﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackPlagueController : MonoBehaviour {
    public Vector3 startingPosition;
    public Vector3 endPosition;
    private Vector3 direction;
    private ParticleSystem smokeParticleSys;
    private PopulationController popController;
    public float speed;
    private bool killed;
	// Use this for initialization
	void Start () {
        killed = false;
        direction = endPosition - startingPosition;
        direction.Normalize();
        this.transform.position = startingPosition;
        smokeParticleSys = transform.GetChild(0).GetComponent<ParticleSystem>();
        popController = FindObjectOfType<PopulationController>();

        if(direction.x < 0)
        {
            Vector3 temp = transform.localScale;
            temp.x = -temp.x;
            transform.localScale = temp;
        }
    }
	
	// Update is called once per frame
	void Update () {
        this.transform.position += direction * speed * Time.deltaTime;
        if(Vector3.Distance(this.transform.position, endPosition) < 1 && !killed)
        {
            speed = 0;
            smokeParticleSys.Play();
            Invoke("RemoveSpriteRenderer", 0.7f);
            Destroy(this.gameObject, 3);
            popController.KillPercentOfPopulation(50);
            killed = true;
        }
	}

    public void RemoveSpriteRenderer()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(startingPosition, 1f);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(endPosition, 1f);
    }
}
