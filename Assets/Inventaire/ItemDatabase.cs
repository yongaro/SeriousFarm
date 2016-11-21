using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ItemDatabase : MonoBehaviour {

    public List<Item> items = new List<Item>();

	// Use this for initialization
	void Start () {
        Debug.Log("hhhh");
        items.Add(new Item("pelle", 0, "permet de creuser", 10, 10, 1, Item.ItemType.Tool));
        items.Add(new Item("graineTomate", 1, "graine à planter", 10, 10, 1, Item.ItemType.Graine));
        items.Add(new Item("graineTomate", 2, "graine à planter", 10, 10, 1, Item.ItemType.Graine));
        items.Add(new Item("pomme", 3, "fruit", 10, 10, 1, Item.ItemType.Fruit));
        //tool
        items.Add(new Item("Axe", 4, "coupe les branches et les arbres", 10, 10, 1, Item.ItemType.Tool));
        items.Add(new Item("Hoe", 5, "retourne la terre", 10, 10, 1, Item.ItemType.Tool));
        items.Add(new Item("Pickaxe", 6, "casse les pierres", 10, 10, 1, Item.ItemType.Tool));
        items.Add(new Item("Scythe", 7, "coupe les herbes", 10, 10, 1, Item.ItemType.Tool));
        items.Add(new Item("WateringCan", 8, "reservoir a eau", 40, 10, 1, Item.ItemType.Tool));
        items.Add(new Item("marteau", 9, "tape tape", 10, 10, 1, Item.ItemType.Tool));

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
