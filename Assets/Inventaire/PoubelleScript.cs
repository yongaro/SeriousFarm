using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class PoubelleScript : MonoBehaviour, IPointerDownHandler
{

   // Use this for initialization
    Inventaire inventaire;
    QuickBar quickBar;
    public GameObject droppedItem;


	void Start()
    {
        inventaire = GameObject.FindGameObjectWithTag("Inventaire").GetComponent<Inventaire>();
        quickBar = GameObject.FindGameObjectWithTag("QuickBar").GetComponent<QuickBar>();

    }
	
	// Update is called once per frame
	void Update()
    {
    }



    public void OnPointerDown(PointerEventData eventData)
    {
        
        if (inventaire.draggingItem )
        {
            inventaire.closeDraggedItem();
            FM_SonScript.poubelle();
        }

        if (quickBar.draggingItem)
        {
            quickBar.closeDraggedItem();
            FM_SonScript.poubelle();
        }
    }
}
