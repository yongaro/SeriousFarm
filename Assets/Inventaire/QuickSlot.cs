using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class QuickSlot : MonoBehaviour, IPointerDownHandler, IDragHandler {

    public Item item;
    Image itemImage;
    public int slotNumber;
    QuickBar quickBar;
    Inventaire inventaire;

    ObjectController objectC;
    Text itemAmount;

    // Use this for initialization
    void Start () {
        itemAmount = gameObject.transform.GetChild(1).GetComponent<Text>();
        objectC = GameObject.FindGameObjectWithTag("Player").GetComponent<ObjectController>();
        quickBar = GameObject.FindGameObjectWithTag("QuickBar").GetComponent<QuickBar>();
        inventaire = quickBar.inventairePanel.GetComponent<Inventaire>();
        itemImage = gameObject.transform.GetChild(0).GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update () {
     
        if (quickBar.Items[slotNumber].itemValue > 0)
        {
            item = quickBar.Items[slotNumber];
            itemImage.enabled = true;
            itemImage.sprite = item.itemIcon;


            if (item.itemType != Item.ItemType.Tool)
            {
                itemAmount.text = "" + item.itemValue;
                itemAmount.enabled = true;
            }
            else
            {
                itemAmount.enabled = false;
                if (item.itemName == "WateringCan")
                {
                    itemAmount.text = " " + item.itemPower;
                    itemAmount.enabled = true;
                }
                

            }
        }
        else
        {
           // objectC.objectCurrent = quickBar.Items[slotNumber-1];
            itemImage.enabled = false;
            itemAmount.enabled = false;
            item = new Item();
           // objectC.objectCurrent = quickBar.Items[slotNumber ];
        }
        if (quickBar.indexSelectItem == slotNumber) 
        {
            GetComponent<Image>().color = Color.cyan;
        }
        else
            GetComponent<Image>().color = Color.white;

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //if (eventData != null) {
            if (item.itemValue == 0 && inventaire.draggingItem)
            {
                quickBar.Items[slotNumber] = inventaire.draggedItem;
                item = inventaire.draggedItem;
                itemAmount.text = "" + inventaire.draggedItem.itemValue;
                inventaire.closeDraggedItem();
            }
             

            if (quickBar.Items[slotNumber].itemName == null && quickBar.draggingItem)
            {
                quickBar.Items[slotNumber] = quickBar.draggedItem;
              
                item = quickBar.draggedItem;
                quickBar.closeDraggedItem();
            }

            if (!quickBar.draggingItem)
            {
                objectC.objectCurrent = item;
                quickBar.indexSelectItem = slotNumber;
            }
            
        //}
    }

    public void OnDrag(PointerEventData eventData)
    {
        
        if (item.itemName != null && !quickBar.draggingItem)
        {
            quickBar.showDraggedItem(item, slotNumber);
            quickBar.Items[slotNumber] = new Item();
            item = new Item();
            itemAmount.enabled = false;
            objectC.objectCurrent = null;
            
        }
    }

}
