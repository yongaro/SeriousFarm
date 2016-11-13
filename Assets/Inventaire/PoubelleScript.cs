using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class PoubelleScript : MonoBehaviour, IPointerDownHandler {

    // Use this for initialization
    Inventaire inventaire;
    public GameObject droppedItem;


	void Start () {
        inventaire = GameObject.FindGameObjectWithTag("Inventaire").GetComponent<Inventaire>();
    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

 

    public void OnPointerDown(PointerEventData eventData)
    {
        if (inventaire.draggingItem)
        {
            inventaire.closeDraggedItem();
        }
    }
}
