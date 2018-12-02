using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOverBuildMenu : MonoBehaviour {

    public eBuildingTypes type;
    public GameObject toolTip;
    
    public void ShowToolTip()
    {
        toolTip.SetActive(true);
        toolTip.GetComponent<ToolTip>().ShowToolTip(type);
    }
}
