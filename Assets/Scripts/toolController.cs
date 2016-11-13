using UnityEngine;
using System.Collections;
using System;

public class toolController : MonoBehaviour {
    
    public FarmerTools currentTool;
    Animator anim;

    public enum FarmerTools {Pelle, Axe, Hoe, Pickaxe, Scythe, WateringCan };

	// Use this for initialization
	void Start () {
        currentTool = FarmerTools.Axe;
	}
	
	// Update is called once per frame
	void Update () {
        
        Debug.Log(currentTool);

        if (currentTool == FarmerTools.Axe)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
        else
            transform.GetChild(0).gameObject.SetActive(false);

        if (currentTool == FarmerTools.Hoe)
        {
            transform.GetChild(1).gameObject.SetActive(true);
        }
        else
            transform.GetChild(1).gameObject.SetActive(false);
        if (currentTool == FarmerTools.Pickaxe)
        {
            transform.GetChild(2).gameObject.SetActive(true);
        }
        else
            transform.GetChild(2).gameObject.SetActive(false);
        if (currentTool == FarmerTools.Scythe)
        {
            transform.GetChild(3).gameObject.SetActive(true);
        }
        else
            transform.GetChild(3).gameObject.SetActive(false);
        if (currentTool == FarmerTools.WateringCan)
        {
            transform.GetChild(4).gameObject.SetActive(true);
        }
        else
            transform.GetChild(4).gameObject.SetActive(false);

    }
}
