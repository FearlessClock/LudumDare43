using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

    public float speed;
    public float currentSpeed;
    public float initXPos;

	void Start () {
        currentSpeed = speed * Random.Range(-0.5f, 1.2f);
    }
	
	void Update () {
        Vector2 temp = transform.position;
        temp.x += speed * Time.deltaTime;

        transform.position = temp;
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Collector")
        {
            Vector2 temp = transform.position;
            temp.x = initXPos;

            transform.position = temp;

            currentSpeed = speed * Random.Range(-0.5f, 1.2f);
        }
    }

}
