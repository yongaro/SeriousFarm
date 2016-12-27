using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class SlotScript : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IDragHandler {

    public Item item;
    Image itemImage;
    public int slotNumber;
    Inventaire inventaire;
    QuickBar quickBar;

    Text itemAmount;
    
   
    // Use this for initialization
    void Start () {
        itemAmount = gameObject.transform.GetChild(1).GetComponent<Text>();
        inventaire = GameObject.FindGameObjectWithTag("Inventaire").GetComponent<Inventaire>();
        itemImage = gameObject.transform.GetChild(0).GetComponent<Image>();
        quickBar = GameObject.FindGameObjectWithTag("QuickBar").GetComponent<QuickBar>();
	}
	
	// Update is called once per frame
	void Update () {
       
	    if(inventaire.Items[slotNumber].itemName != null)
        {
            itemAmount.enabled = false;
            item = inventaire.Items[slotNumber];
            itemImage.enabled = true;
            itemImage.sprite = inventaire.Items[slotNumber].itemIcon;

            if (inventaire.Items[slotNumber].itemType != Item.ItemType.Tool)
            {
                itemAmount.enabled = true;
                itemAmount.text = "" + inventaire.Items[slotNumber].itemValue;
            }
        }
        else
        {
            itemImage.enabled = false;

        }
	}

    public void OnPointerDown(PointerEventData eventData)
    {
        FM_SonScript.quickBarSelection();
        if (inventaire.Items[slotNumber].itemType != Item.ItemType.Tool)
        {
            //inventaire.Items[slotNumber].itemValue--;
            if (inventaire.Items[slotNumber].itemValue  == 0)
            {
                inventaire.Items[slotNumber] = new Item();
                itemAmount.enabled = false;
                inventaire.hideTooltip();
            }
        }

        if (item.itemValue == 0 && quickBar.draggingItem)
        {
            inventaire.Items[slotNumber] = quickBar.draggedItem;
            item = quickBar.draggedItem;
            itemAmount.text = "" + quickBar.draggedItem.itemValue;
            quickBar.closeDraggedItem();
        }

        /*if (quickBar.Items[slotNumber].itemName == null && quickBar.draggingItem)
        {

            quickBar.Items[slotNumber] = quickBar.draggedItem;
            quickBar.closeDraggedItem();
        }*/

        if (inventaire.Items[slotNumber].itemName == null && inventaire.draggingItem)
        {
      
            inventaire.Items[slotNumber] = inventaire.draggedItem;
            inventaire.closeDraggedItem();
        }
        else if (inventaire.Items[slotNumber].itemName != null && inventaire.draggingItem)
        {
            inventaire.Items[inventaire.indexDraggedItem] = inventaire.Items[slotNumber];
            inventaire.Items[slotNumber] = inventaire.draggedItem;
            inventaire.closeDraggedItem();
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
        if (inventaire.Items[slotNumber].itemName != null  )
        {
            inventaire.showTooltip(inventaire.Slots[slotNumber].GetComponent<RectTransform>().localPosition, inventaire.Items[slotNumber]);
        }

     
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (inventaire.Items[slotNumber].itemName != null)
        {
            inventaire.hideTooltip();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
       /* if (inventaire.Items[slotNumber].itemType != Item.ItemType.Tool)
        {
            inventaire.Items[slotNumber].itemValue++;
        }*/
        
        if (inventaire.Items[slotNumber].itemName != null)
        {
            inventaire.showDraggedItem(inventaire.Items[slotNumber], slotNumber);
            inventaire.Items[slotNumber] = new Item();
            inventaire.hideTooltip();
            itemAmount.enabled = false;
        }
    }



}
