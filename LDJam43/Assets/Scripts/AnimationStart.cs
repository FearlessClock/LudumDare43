using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStart : MonoBehaviour {

    private Animator anim;
    public int animIdToPlay; 

	void Start () {
        anim = transform.GetComponent<Animator>();
        anim.Play(animIdToPlay, 0, Random.Range(0, 1f));
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Villager")
        {
            anim.SetTrigger("Move");
        }
    }
}
