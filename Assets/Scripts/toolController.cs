using UnityEngine;
using System.Collections;
using System;

public class toolController : MonoBehaviour {
    
    public FarmTools currentTool;
    Animator anim;

    //public enum FarmerTools {Pelle, Axe, Hoe, Pickaxe, Scythe, WateringCan };

	// Use this for initialization
	void Start () {
        currentTool = FarmTools.Axe;
	}
	
	// Update is called once per frame
	void Update () {

        if (currentTool == FarmTools.Axe)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
        else
            transform.GetChild(0).gameObject.SetActive(false);

        if (currentTool == FarmTools.Hoe)
        {
            transform.GetChild(1).gameObject.SetActive(true);
        }
        else
            transform.GetChild(1).gameObject.SetActive(false);
        if (currentTool == FarmTools.Pickaxe)
        {
            transform.GetChild(2).gameObject.SetActive(true);
        }
        else
            transform.GetChild(2).gameObject.SetActive(false);
        if (currentTool == FarmTools.Scythe)
        {
            transform.GetChild(3).gameObject.SetActive(true);
        }
        else
            transform.GetChild(3).gameObject.SetActive(false);
        if (currentTool == FarmTools.WateringCan)
        {
            transform.GetChild(4).gameObject.SetActive(true);
        }
        else
            transform.GetChild(4).gameObject.SetActive(false);

    }
}
