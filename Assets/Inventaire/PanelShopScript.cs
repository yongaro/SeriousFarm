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
    public Item.ItemType categorie;
    public int slotamount;
    public GameObject description;
    public int indexSlotSelected;
    Text descriptionText;
    Button buyButton;
    public Button graineButton;
    public Button mobilierButton;
    QuickBar quickBar;
    ShopGlobalScrip shopGlobal;
    MarchandeScript marchande;
    public Boolean boolMarchande;
    // Use this for initialization
    void Start()
    {
        shopGlobal = GameObject.FindGameObjectWithTag("ShopGlobal").GetComponent<ShopGlobalScrip>();
        database = GameObject.FindGameObjectWithTag("ItemDatabase").GetComponent<ItemDatabase>();
        description = GameObject.FindGameObjectWithTag("Description");
        descriptionText = description.transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<Text>();
        buyButton = description.transform.GetChild(2).GetComponent<Button>();
        buyButton.onClick.AddListener(buyProduct);
        graineButton.onClick.AddListener(graineCategorie);
        mobilierButton.onClick.AddListener(mobilierCategorie);
        quickBar = GameObject.FindGameObjectWithTag("QuickBar").GetComponent<QuickBar>();
        marchande = GameObject.FindGameObjectWithTag("ShopGlobal").GetComponent<MarchandeScript>();
        boolMarchande = false;
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
        graineCategorie();  
    }

    private void mobilierCategorie()
    {
        FM_SonScript.marchandButton();
        for (int i = 0; i < 4 * 6; i++)
        {
            Items[i] = new Item();
        }
        addItem(23);
    }

    private void graineCategorie()
    {
        FM_SonScript.marchandButton();
        for (int i = 0; i < 4 * 6; i++)
        {
            Items[i] = new Item();
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
    }

    private void buyProduct()
    {
        if (Items[indexSlotSelected].itemValue == 0)
            return;
        if (boolMarchande)
        {
            boolMarchande = false;
            Debug.Log(Items[indexSlotSelected].itemName);
            if (!quickBar.estPlein() && shopGlobal.monnaie - Items[indexSlotSelected].itemPrice >= 0)
            {
                quickBar.addItem(Items[indexSlotSelected].itemID);
                shopGlobal.monnaie -= Items[indexSlotSelected].itemPrice;
                FM_SonScript.sonAchat();
            }
            else
                buyButton.GetComponent<Image>().color = Color.grey;
        }
        else if (marchande.LegumeDeSaison(Items[indexSlotSelected]))
        {
            
            Debug.Log(Items[indexSlotSelected].itemName);
            if (!quickBar.estPlein() && shopGlobal.monnaie - Items[indexSlotSelected].itemPrice >= 0)
            {
                quickBar.addItem(Items[indexSlotSelected].itemID);
                shopGlobal.monnaie -= Items[indexSlotSelected].itemPrice;
                FM_SonScript.sonAchat();
            }
            else
                buyButton.GetComponent<Image>().color = Color.grey;
        }
        else { boolMarchande = true; }
    }

    // Update is called once per frame
    void Update()
    {
        descriptionText.text = Items[indexSlotSelected].itemDesc;
        description.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = Items[indexSlotSelected].itemName;
        description.transform.GetChild(3).GetComponent<Text>().text = "Prix : "+Items[indexSlotSelected].itemPrice;
        description.transform.GetChild(4).GetComponent<Text>().text = "Plantation : "+ Items[indexSlotSelected].itemSaison;

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
