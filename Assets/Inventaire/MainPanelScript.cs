using UnityEngine;
using System.Collections;

public class MainPanelScript : MonoBehaviour {

    public GameObject mainPanel;
    public bool activationMenu;
    public GameObject quickBar;
    public bool activationQuick;

	// Use this for initialization
	void Start () {
        mainPanel.SetActive(false);
        activationMenu = false;
        activationQuick = true;
        quickBar.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.M))
        {
            activationMenu = !activationMenu;
            mainPanel.SetActive(activationMenu);
            quickBar.SetActive(activationQuick);
        }/*
        
        if(Input.GetKeyDown(KeyCode.I))
        {
            activationQuick = !activationQuick;
            quickBar.SetActive(activationQuick);
        }*/
    }
}
