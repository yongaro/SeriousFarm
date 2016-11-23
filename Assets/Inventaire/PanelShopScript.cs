using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class PanelShopScript : MonoBehaviour {

    public List<Item> Items = new List<Item>();
    public GameObject slots;
    public List<GameObject> Slots = new List<GameObject>();
    ItemDatabase database;
    Item.ItemType categorie;
    public int slotamount;
    public GameObject description;
    public int indexSlotSelected;
    Text descriptionText;
    Button buyButton;
    QuickBar quickBar;
    int monnaie;
    Text monnaieText;
    public GameObject monnaiePanel;

    // Use this for initialization
    void Start()
    {
        monnaie = 50;
        database = GameObject.FindGameObjectWithTag("ItemDatabase").GetComponent<ItemDatabase>();
        description = GameObject.FindGameObjectWithTag("Description");
        descriptionText = description.transform.GetChild(1).GetComponent<Text>();
        buyButton = description.transform.GetChild(2).GetComponent<Button>();
        buyButton.onClick.AddListener(buyProduct);
        quickBar = GameObject.FindGameObjectWithTag("QuickBar").GetComponent<QuickBar>();
        monnaieText = monnaiePanel.transform.GetChild(1).GetComponent<Text>();
        monnaieText.text = " : " + monnaie;
        categorie = Item.ItemType.Graine;
        slotamount = 0;
        for (int i = 0; i < 4; i++)
        {

            for (int j = 0; j < 6; j++)
            {
                GameObject slot = (GameObject)Instantiate(slots);
                slot.GetComponent<SlotShopScript>().slotNumber = slotamount;
                Slots.Add(slot);
                Items.Add(new Item());
                slot.name = "Slot" + i + "." + j;
                slot.transform.SetParent(this.gameObject.transform);
                slotamount++;
            }
        }

        addItem(10);
        addItem(11);
        addItem(12);
        addItem(13);
        addItem(14);
        addItem(15);
        addItem(16);
        addItem(17);
        addItem(18);
        addItem(19);
        addItem(20);
        addItem(21);
        addItem(22);
        addItem(23);
        addItem(24);
        addItem(25);
    }

    private void buyProduct()
    {
        Debug.Log(Items[indexSlotSelected].itemName);
        if (!quickBar.estPlein() && monnaie - Items[indexSlotSelected].itemPrice >= 0 )
        {
            quickBar.addItem(Items[indexSlotSelected].itemID);
            monnaie -= Items[indexSlotSelected].itemPrice;
            monnaieText.text = " : " + monnaie;
        }
        else
            buyButton.GetComponent<Image>().color = Color.grey;
    }

    // Update is called once per frame
    void Update()
    {
        descriptionText.text = Items[indexSlotSelected].itemDesc;
        description.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = Items[indexSlotSelected].itemName;
        description.transform.GetChild(3).GetComponent<Text>().text = "Prix : "+Items[indexSlotSelected].itemPrice;
        if (Items[indexSlotSelected].itemSaison == 1)
            description.transform.GetChild(4).GetComponent<Text>().text = "Saison : Hiver";
        if (Items[indexSlotSelected].itemSaison == 2)
            description.transform.GetChild(4).GetComponent<Text>().text = "Saison : Printemps";
        if (Items[indexSlotSelected].itemSaison == 3)
            description.transform.GetChild(4).GetComponent<Text>().text = "Saison : Ete";
        if (Items[indexSlotSelected].itemSaison == 4)
            description.transform.GetChild(4).GetComponent<Text>().text = "Saison : Automne";
    }

    
    void addItem(int id)
    {

        for (int i = 0; i < database.items.Count; i++)
        {
            if (database.items[i].itemID == id)
            {
                Item item = database.items[i];
                addItemAtEmptySlot(item);
                
            }
        }
    }

    void addItemAtEmptySlot(Item item)
    {
        for (int i = 0; i < Items.Count; i++)
        {
            if (Items[i].itemName == null)
            {
                Items[i] = item;

                break;
            }
        }
    }


}
