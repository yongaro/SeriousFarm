using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ItemDatabase  : MonoBehaviour{

    public  List<Item> items = new List<Item>();

	// Use this for initialization
	void Start () {
        Debug.Log("hhhh");
        items.Add(new Item("pelle", 0, "permet de creuser", 10, "", 1, 0, Item.ItemType.Tool));
        items.Add(new Item("graineTomate", 1, "graine à planter", 10, "", 1, 0,Item.ItemType.Graine));
        items.Add(new Item("graineTomate", 2, "graine à planter", 10, "", 1, 0,Item.ItemType.Graine));
        items.Add(new Item("pomme", 3, "fruit", 10, "", 1, 0, Item.ItemType.Plante));
        //tool
        items.Add(new Item("Axe", 4, "coupe les branches et les arbres", 10, "", 1, 0,Item.ItemType.Tool));
        items.Add(new Item("Hoe", 5, "retourne la terre", 10, "", 1, 0,Item.ItemType.Tool));
        items.Add(new Item("Pickaxe", 6, "casse les pierres", 10, "", 1, 0,Item.ItemType.Tool));
        items.Add(new Item("Scythe", 7, "coupe les herbes", 10, "", 1, 0, Item.ItemType.Tool));
        items.Add(new Item("WateringCan", 8, "reservoir a eau", 40, "", 1, 0, Item.ItemType.Tool));
        items.Add(new Item("marteau", 9, "tape tape", 10, "", 1, 0, Item.ItemType.Tool));

        // graine
        //items.Add(new Item("ail", 10, "D'origine asiatique, l'ail est maintenant cultivé au travers le monde afin d'épicer vos plats, d'améliorer vos traitements et préventifs en phytothérapie", 10, 2, 1, 10, Item.ItemType.Graine));
        items.Add(new Item("aubergine", 10, "L’aubergine (Solanum melongena L.) est une plante potagère herbacée de la famille des Solanacées, cultivée pour son fruit consommé comme légume-fruit.", 10, "fev, mar, avr", 1, 20,Item.ItemType.Graine));
        items.Add(new Item("ble", 11, "casse les pierres", 100, "sept, oct, nov", 1, 10, Item.ItemType.Graine));
        items.Add(new Item("oignon", 12, "coupe les herbes", 80, "mars, avr", 1, 10, Item.ItemType.Graine));
        items.Add(new Item("carotte", 13, "reservoir a eau", 60, "fev, mars, avr, mai, juin, aou, sept", 1, 10, Item.ItemType.Graine));
        items.Add(new Item("chou_fleur", 14, "tape tape", 10, "mars, avr, mai, juin", 1, 30,Item.ItemType.Graine));
        items.Add(new Item("citrouille", 15, "coupe les branches et les arbres", 10, "avr, mai, juin", 1, 40,Item.ItemType.Graine));
        items.Add(new Item("concombre", 16, "retourne la terre", 10, "mars, avr, mai, juin, jui", 1, 20,Item.ItemType.Graine));
      //items.Add(new Item("haricot", 18, "casse les pierres", 10, 3, 1, 40,Item.ItemType.Graine));
        items.Add(new Item("mais", 17, "coupe les herbes", 10, "avr, mai, juin", 1, 20,Item.ItemType.Graine));
        items.Add(new Item("navet", 18, "reservoir a eau", 10, "mars, avr,mai, juin, jui, aout", 1, 30,Item.ItemType.Graine));
        items.Add(new Item("patate", 19, "tape tape", 40, "fev, mars, avr", 1, 10, Item.ItemType.Graine));
        //items.Add(new Item("poireau", 22, "casse les pierres", 10, 1, 1, 20,Item.ItemType.Graine));
        items.Add(new Item("poivron", 20, "coupe les herbes", 10, "fev, mars, avr, mai", 1, 10,Item.ItemType.Graine));
        items.Add(new Item("tomate", 21, "reservoir a eau", 10, "fev, mars, avr", 1, 10,Item.ItemType.Graine));
        items.Add(new Item("salade", 22, "tape tape", 10, "fev, mars, avr, mai, juin", 1, 10,Item.ItemType.Graine));
        //fraise ?????
        /* ************************* mobilier ****************************************/
        items.Add(new global::Item("sprinkler", 23, "arrosage automatique des 8 voisins", 1, "", 1, 100, Item.ItemType.Mobilier));
    }

    public int addItem(Item item)
    {
        bool present = false;
        int price = 0;
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].itemName == item.itemName && items[i].itemType == item.itemType && (items[i].itemPower / 25 == item.itemPower / 25 || (item.itemPower / 25 >= 3 && items[i].itemPower / 25 >= 3)))
            {
                item = items[i];
                present = true;
            }
            else if (items[i].itemName == item.itemName)
            {
                price = items[i].itemPrice;
                Debug.Log(price);
                break;
                /*if (item.itemPower / 25 != 0)
                    item.itemPrice = items[i].itemPrice * (item.itemPower / 25);
                else
                    item.itemPrice = items[i].itemPrice;*/
            }
        }
        if (!present)
        {
            item.itemID = items.Count;
            if (item.itemPower / 25 != 0)
                item.itemPrice = price * (item.itemPower / 25);
            else
                item.itemPrice = price;
            //Debug.Log(item.itemPower +" "+ item.itemPower / 25);
            items.Add(item);
            Debug.Log("k" + item.itemPrice);
        }
        return item.itemID;
    }
    
	
	// Update is called once per frame
	void Update () {
	    
	}
}
