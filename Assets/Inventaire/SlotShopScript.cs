using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class SlotShopScript : MonoBehaviour, IPointerDownHandler
{

    public Item item;
    Image itemImage;
    public int slotNumber;
    PanelShopScript shopScript;
    QuickBar quickBar;
 


    void Start()
    {
        shopScript = GameObject.FindGameObjectWithTag("Shop").GetComponent<PanelShopScript>();
        itemImage = gameObject.transform.GetChild(0).GetComponent<Image>();
        quickBar = GameObject.FindGameObjectWithTag("QuickBar").GetComponent<QuickBar>();
    }

   

    // Update is called once per frame
    void Update()
    {
        if (shopScript.Items[slotNumber].itemName != null)
        {
            item = shopScript.Items[slotNumber];
            itemImage.enabled = true;
            itemImage.sprite = shopScript.Items[slotNumber].itemIcon;
        }
        else
        {
            itemImage.enabled = false;
        }

        if (slotNumber == shopScript.indexSlotSelected)
        {
            GetComponent<Image>().color = Color.gray;
        }
        else
            GetComponent<Image>().color = Color.white;


    }

    public void OnPointerDown(PointerEventData eventData)
    {
        shopScript.boolMarchande = false;
        shopScript.indexSlotSelected = slotNumber;
        FM_SonScript.marchandSelection();
    }



}
