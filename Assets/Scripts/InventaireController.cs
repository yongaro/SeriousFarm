using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InventaireController : MonoBehaviour {

    bool activation = true;
    GameObject panel;
    public int[] slots;
	// Use this for initialization
	void Start () {
        GetComponent<Canvas>().enabled = false;
        panel = transform.GetChild(0).gameObject;
        slots = new int[panel.transform.childCount];
        GetComponent<Canvas>().enabled = activation;
    }
	
	// Update is called once per frame
	void Update () { 

        //affiche l'inventaire en appuyant sur i
	    if (Input.GetKeyDown(KeyCode.I))
        {
            activation = !activation;
            GetComponent<Canvas>().enabled = activation;
        }
	}

   
}
