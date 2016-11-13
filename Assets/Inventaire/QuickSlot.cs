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
    public GameObject slotPanel;

    toolController toolC;

    Text itemAmount;

    // Use this for initialization
    void Start () {
        itemAmount = gameObject.transform.GetChild(1).GetComponent<Text>();
        toolC = GameObject.FindGameObjectWithTag("Outils").GetComponent<toolController>();
        quickBar = GameObject.FindGameObjectWithTag("QuickBar").GetComponent<QuickBar>();
        inventaire = quickBar.inventairePanel.GetComponent<Inventaire>();
        itemImage = gameObject.transform.GetChild(0).GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update () {
     
        if (quickBar.Items[slotNumber].itemValue != 0)
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
                itemAmount.enabled = false;
        }
        else
        {
            itemImage.enabled = false;
            itemAmount.enabled = false;
        }

    }



    public void OnPointerDown(PointerEventData eventData)
    {

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

        if (item.itemType == Item.ItemType.Tool)
        {
            if (item.itemName == "pelle")
                toolC.currentTool = toolController.FarmerTools.Pelle;
            if (item.itemName == "Axe")
                toolC.currentTool = toolController.FarmerTools.Axe;
            if (item.itemName == "Hoe")
                toolC.currentTool = toolController.FarmerTools.Hoe;
            if (item.itemName == "Pickaxe")
                toolC.currentTool = toolController.FarmerTools.Pickaxe;
            if (item.itemName == "Scythe")
                toolC.currentTool = toolController.FarmerTools.Scythe;
            if (item.itemName == "WateringCan")
                toolC.currentTool = toolController.FarmerTools.WateringCan;
        }

    }

    public void OnDrag(PointerEventData eventData)
    {
        
        if (item.itemName != null && !quickBar.draggingItem)
        {
            quickBar.showDraggedItem(item, slotNumber);
            quickBar.Items[slotNumber] = new Item();
            item = new Item();
            itemAmount.enabled = false;
        }
    }

}
