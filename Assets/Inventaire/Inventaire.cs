using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Inventaire : MonoBehaviour
{
    public GameObject slots;
    public List<GameObject> Slots = new List<GameObject>();
    public List<Item> Items = new List<Item>();
    ItemDatabase database;
    public int slotamount;

    public GameObject tooltip;

    public GameObject draggedItemObject;
    public bool draggingItem;
    public Item draggedItem;
    public int indexDraggedItem;



    void Start()
    {
        draggingItem = false;
        database = GameObject.FindGameObjectWithTag("ItemDatabase").GetComponent<ItemDatabase>();

        slotamount = 0;
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 12; j++)
            {
                GameObject slot = (GameObject)Instantiate(slots);
                slot.GetComponent<SlotScript>().slotNumber = slotamount;
                Slots.Add(slot);
                Items.Add(new Item());
                slot.name = "Slot" + i + "." + j;
                slot.transform.SetParent(this.gameObject.transform);
                slotamount++;
            }
        }

        addItem(4);
    }

    public void showTooltip(Vector3 toolPosition, Item item)
    {
        tooltip.GetComponent<RectTransform>().localPosition = new Vector3(toolPosition.x + 60, toolPosition.y - 100, 0);
        tooltip.SetActive(true);
        tooltip.transform.GetChild(0).GetComponent<Text>().text = item.itemName;
        tooltip.transform.GetChild(1).GetComponent<Text>().text = item.itemDesc;
        tooltip.transform.GetChild(2).GetComponent<Text>().text = item.itemName;
    }

    public void hideTooltip()
    {
        tooltip.SetActive(false);
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


    public void checkIfItemExist(int itemID, Item item)
    {
        int nbitem = 0;
        for (int i = 0; i < Items.Count; i++)
        {
            if (Items[i].itemName != null)
                nbitem++;
        }
        for (int i = 0; i < database.items.Count; i++)
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

    void addItem(int id)
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
            if (Items[i].itemName == null)
            {
                Items[i] = item;

                break;
            }
        }
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
}
