using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempleController : Building {

    void Start () {
        buildingType = eBuildingTypes.Temple;
        godController.instance.AddConstantFavor(0.2f);
        godController.instance.OnCreateTemple();
    }
}
