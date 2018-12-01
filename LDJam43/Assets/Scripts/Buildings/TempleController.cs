using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempleController : Building {

    godController god;

	void Start () {
        god = FindObjectOfType<godController>();
	}


}
