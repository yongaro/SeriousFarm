using UnityEngine;
using System.Collections;
using System;

public class toolController : MonoBehaviour {

    public String currentTool;
    Animator anim;
	// Use this for initialization
	void Start () {
        currentTool = "pelle";
	}
	
	// Update is called once per frame
	void Update () {
        
            Debug.Log(currentTool);
            foreach (Transform child in transform)
            {
                if (child.gameObject.tag == currentTool)
                {
                    child.gameObject.SetActive(true);

                    
                }
                else
                    child.gameObject.SetActive(false);
            }
        


    }
}
