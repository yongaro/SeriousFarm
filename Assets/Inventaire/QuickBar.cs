using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class QuickBar : MonoBehaviour
{
    // Use this for initialization
    Inventaire inventaire;
    public GameObject quickSlots;
    public List<GameObject> QuickSlots = new List<GameObject>();
    public List<Item> Items = new List<Item>();
    ItemDatabase database;
    public int slotamount;

    public GameObject inventairePanel;

    public GameObject draggedItemObject;
    public bool draggingItem;
    public Item draggedItem;
    public int indexDraggedItem;
    public int indexSelectItem;
   


    void Start()
    {
        database = GameObject.FindGameObjectWithTag("ItemDatabase").GetComponent<ItemDatabase>();
        inventaire = inventairePanel.GetComponent<Inventaire>();
        indexSelectItem = 0;
    
        slotamount = 0;
        draggingItem = false;
        for (int j = 0; j < 15; j++)
                {
                    GameObject slot = (GameObject)Instantiate(quickSlots);
                    slot.GetComponent<QuickSlot>().slotNumber = slotamount;
                    QuickSlots.Add(slot);
                    Items.Add(new Item());
                    slot.name = "quickSlot" + slotamount;
                    slot.transform.SetParent(this.gameObject.transform);
                    slotamount++;
                }
        addItem(4);
        addItem(5);
        addItem(6);
        addItem(7);
        addItem(8);
    }

    // Update is called once per frame
    void Update()
    {
        if (draggingItem)
        {
            Vector3 posi = (Input.mousePosition - GameObject.FindGameObjectWithTag("Canvas").GetComponent<RectTransform>().localPosition);
            draggedItemObject.GetComponent<RectTransform>().localPosition = new Vector3(posi.x + 21, posi.y - 21, 0);
        }
           
    }
    public bool estPlein()
    {
        int nbitem = 0;
        for (int i = 0; i < Items.Count; i++)
        {
            if (Items[i].itemName != null)
                nbitem++;
        }
        if (nbitem == Items.Count - 1)
            return true;
        else
            return false;
    }

   
    public void checkIfItemExist(int itemID, Item item)
    {
        int nbitem = 0;
        for (int i = 0; i < Items.Count; i++)
        {
            if (Items[i].itemName != null)
                nbitem++;
        }
       
        for (int i = 0; i < Items.Count; i++)
        {
            if (Items[i].itemID == itemID)
            {
                Items[i].itemValue = Items[i].itemValue + 1;
                break;
            }

            else if (i == nbitem)
            {
                addItemAtEmptySlot(item);
            }
        }
    }

    public void addItem(int id)
    {
        for (int i = 0; i < database.items.Count; i++)
        {
           
            if (database.items[i].itemID == id)
            {
              
                Item item = database.items[i];

                if (database.items[i].itemType != Item.ItemType.Tool)
                {
                    checkIfItemExist(id, item);
                    break;
                }
                else
                {
                    addItemAtEmptySlot(item);
                }

            }
        }
    }

    void addItemAtEmptySlot(Item item)
    {
        for (int i = 0; i < Items.Count; i++)
        {
            if (Items[i].itemValue == 0)
            {
                Items[i] = item;
                break;
            }
        }
    }

   
    public void showDraggedItem(Item item, int slotnumber)
    {
        indexDraggedItem = slotnumber;
        draggedItemObject.SetActive(true);
        draggedItem = item;
        draggedItemObject.GetComponent<Image>().sprite = item.itemIcon;
        draggingItem = true;
    }

    public void closeDraggedItem()
    {
        draggingItem = false;
        draggedItemObject.SetActive(false);
    }

    public void deleteItem(int id )
    {
        for (int i = 0; i < Items.Count; i++)
        {
            if (Items[i].itemID == id  )
                Items[i] = new Item();
        }
    }
    
}