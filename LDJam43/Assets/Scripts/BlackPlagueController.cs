using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackPlagueController : MonoBehaviour {
    public Vector3 startingPosition;
    public Vector3 endPosition;
    private Vector3 direction;
    private ParticleSystem smokeParticleSys;
    private PopulationController popController;
    public float speed;
	// Use this for initialization
	void Start () {
        direction = endPosition - startingPosition;
        direction.Normalize();
        this.transform.position = startingPosition;
        smokeParticleSys = GetComponent<ParticleSystem>();
        popController = FindObjectOfType<PopulationController>();

    }
	
	// Update is called once per frame
	void Update () {
        this.transform.position += direction * speed * Time.deltaTime;
        if(Vector3.Distance(this.transform.position, endPosition) < 1)
        {
            speed = 0;
            smokeParticleSys.Play();
            Invoke("RemoveSpriteRenderer", 0.7f);
            Destroy(this.gameObject, 3);
            popController.KillPercentOfPopulation(10);
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
