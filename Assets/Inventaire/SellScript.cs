﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class SellScript : MonoBehaviour, IPointerDownHandler
{

    GameObject itemPanel;
    GameObject quantitePanel;
    Text prixPanel;
    Button vendreButton;
    Button moinsButton;
    Button plusButton;
    Item item;
    QuickBar quickBar;
    public GameObject droppedItem;
    int nbProductSell;
    ShopGlobalScrip shopGlobal;

   

    // Use this for initialization
    void Start () {
        quickBar = GameObject.FindGameObjectWithTag("QuickBar").GetComponent<QuickBar>();
        itemPanel = transform.GetChild(1).gameObject;
        quantitePanel = transform.GetChild(2).gameObject;
        prixPanel = transform.GetChild(3).transform.GetChild(0).gameObject.GetComponent<Text>();
        vendreButton = transform.GetChild(4).GetComponent<Button>();
        vendreButton.onClick.AddListener(vendreProduct);
        plusButton = quantitePanel.transform.GetChild(0).GetComponent<Button>();
        plusButton.onClick.AddListener(plusQuantite);
        moinsButton = quantitePanel.transform.GetChild(1).GetComponent<Button>();
        moinsButton.onClick.AddListener(moinsQuantite);
        shopGlobal = GameObject.FindGameObjectWithTag("ShopGlobal").GetComponent<ShopGlobalScrip>();
        item = new Item();
        nbProductSell = 0;
    }

    private void plusQuantite()
    {
        if (nbProductSell < item.itemValue)
        {
            nbProductSell++;
        }
    }

    private void moinsQuantite()
    {
        if (nbProductSell > 1)
        {
            nbProductSell--;
        }
    }

    private void vendreProduct()
    {
        if (item.itemValue != 0)
        {
           shopGlobal.monnaie += nbProductSell * item.itemPrice;
            if (nbProductSell == item.itemValue)
            {
                quickBar.deleteItem(item.itemID);
                item = new Item();
                itemPanel.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("point");
                nbProductSell = 0;
            }
            else
            {
                for (int i = 0; i < quickBar.Items.Count; i++)
                {
                    if (quickBar.Items[i].itemID == item.itemID)
                    {
                        quickBar.Items[i].itemValue -= nbProductSell;
                        nbProductSell = item.itemValue;
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update () {
        quantitePanel.transform.GetChild(2).GetComponent<Text>().text = ""+nbProductSell;
        prixPanel.text = " "+item.itemPrice * nbProductSell;
        if (nbProductSell <= 1)
            moinsButton.GetComponent<Image>().color = Color.gray;
        if (nbProductSell >= item.itemValue)
            plusButton.GetComponent<Image>().color = Color.gray;
        if (nbProductSell > 1)
            moinsButton.GetComponent<Image>().color = Color.white;
        if (nbProductSell < item.itemValue)
            plusButton.GetComponent<Image>().color = Color.white;
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (quickBar.draggingItem)
        {
            item = quickBar.draggedItem;
            itemPanel.transform.GetChild(0).GetComponent<Image>().sprite = item.itemIcon;
            quickBar.closeDraggedItem();
            quickBar.addItem(item.itemID);
            nbProductSell = item.itemValue;
            Debug.Log(item.itemValue);
        }
    }



}
