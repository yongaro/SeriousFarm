﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ItemDatabase : MonoBehaviour {

    public List<Item> items = new List<Item>();

	// Use this for initialization
	void Start () {
        Debug.Log("hhhh");
        items.Add(new Item("pelle", 0, "permet de creuser", 10, 10, 1, 0, Item.ItemType.Tool));
        items.Add(new Item("graineTomate", 1, "graine à planter", 10, 10, 1, 0,Item.ItemType.Graine));
        items.Add(new Item("graineTomate", 2, "graine à planter", 10, 10, 1, 0,Item.ItemType.Graine));
        items.Add(new Item("pomme", 3, "fruit", 10, 10, 1, 0, Item.ItemType.Fruit));
        //tool
        items.Add(new Item("Axe", 4, "coupe les branches et les arbres", 10, 10, 1, 0,Item.ItemType.Tool));
        items.Add(new Item("Hoe", 5, "retourne la terre", 10, 10, 1, 0,Item.ItemType.Tool));
        items.Add(new Item("Pickaxe", 6, "casse les pierres", 10, 10, 1, 0,Item.ItemType.Tool));
        items.Add(new Item("Scythe", 7, "coupe les herbes", 10, 10, 1, 0, Item.ItemType.Tool));
        items.Add(new Item("WateringCan", 8, "reservoir a eau", 40, 10, 1, 0, Item.ItemType.Tool));
        items.Add(new Item("marteau", 9, "tape tape", 10, 10, 1, 0, Item.ItemType.Tool));

        // graine
        items.Add(new Item("Ail", 10, "D'origine asiatique, l'ail est maintenant cultivé au travers le monde afin d'épicer vos plats, d'améliorer vos traitements et préventifs en phytothérapie", 10, 2, 1, 10, Item.ItemType.Graine));
        items.Add(new Item("Aubergine", 11, "L’aubergine (Solanum melongena L.) est une plante potagère herbacée de la famille des Solanacées, cultivée pour son fruit consommé comme légume-fruit.", 10, 2, 1, 20,Item.ItemType.Graine));
        items.Add(new Item("Ble", 12, "casse les pierres", 10, 3, 1, 10, Item.ItemType.Graine));
        items.Add(new Item("Brocoli", 13, "coupe les herbes", 10, 3, 1, 10, Item.ItemType.Graine));
        items.Add(new Item("Carotte", 14, "reservoir a eau", 40, 3, 1, 10, Item.ItemType.Graine));
        items.Add(new Item("Choufleur", 15, "tape tape", 10, 2, 1, 30,Item.ItemType.Graine));
        items.Add(new Item("Citrouille", 16, "coupe les branches et les arbres", 10, 2, 1, 40,Item.ItemType.Graine));
        items.Add(new Item("Courgette", 17, "retourne la terre", 10, 2, 1, 20,Item.ItemType.Graine));
        items.Add(new Item("Haricot", 18, "casse les pierres", 10, 3, 1, 40,Item.ItemType.Graine));
        items.Add(new Item("Mais", 19, "coupe les herbes", 10, 3, 1, 20,Item.ItemType.Graine));
        items.Add(new Item("Navet", 20, "reservoir a eau", 40, 4, 1, 30,Item.ItemType.Graine));
        items.Add(new Item("Pattate", 21, "tape tape", 10, 2, 1, 10, Item.ItemType.Graine));
        items.Add(new Item("Poireau", 22, "casse les pierres", 10, 1, 1, 20,Item.ItemType.Graine));
        items.Add(new Item("Poivron", 23, "coupe les herbes", 10, 1, 1, 10,Item.ItemType.Graine));
        items.Add(new Item("Tomate", 24, "reservoir a eau", 40, 1, 1, 10,Item.ItemType.Graine));
        items.Add(new Item("Salade", 25, "tape tape", 10, 4, 1, 10,Item.ItemType.Graine));

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
