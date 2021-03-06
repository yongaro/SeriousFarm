﻿using UnityEngine;
using System.Collections;

[System.Serializable]
public class Item  {

    public string itemName;
    public int itemID;
    public string itemDesc;
    public Sprite[] sprites;
    public Sprite itemIcon;
    public GameObject itemModel;
    public int itemPower;
    public string itemSaison;  // 1 hiver , 2 printemps, 3 ete, 4 hiver
    public int itemValue;
    public int itemPrice;
    public ItemType itemType;

    public enum ItemType
    {
        Tool, Graine, Plante, Pierre, Batton, Engrais, Pesticides, Insecticides, Mobilier 
    }

    /*
     * name : nom de l'objet
     * id : indice dans la itemDatabase
     * desc : text de description du produit
     * power : quantité pour l'eau dans l'arrosoir ou qualité pour une plante
     * saison : quand on peut planter le legume  // à modifier
     * value : quantité de l'item 
     * price : prix de l'item
     * type : categorie 
     */

    public Item (string name, int id, string desc, int power, string saison, int value, int price, ItemType type)
    {
        itemName = name;
        itemID = id;
        itemDesc = desc;
        itemPower = power;
        itemSaison = saison;
        itemValue = value;
        itemType = type;
        itemPrice = price;
        if (type == ItemType.Tool)
        {
            if (name == "WateringCan")
            {
                sprites = Resources.LoadAll<Sprite>("arrosoir");
                   
            }
            else
                sprites = Resources.LoadAll<Sprite>("sprite2");

        }
        else if (type == ItemType.Graine || type == ItemType.Plante)
            sprites = Resources.LoadAll<Sprite>("Graine");
        else if (type == ItemType.Mobilier)
            sprites = Resources.LoadAll<Sprite>("Science Plus2 Math");
        else
            sprites = Resources.LoadAll<Sprite>("Sprite");
        for (int i = 0; i < sprites.Length; i++) {
            
            if (sprites[i].name == name)
            {
                itemIcon = sprites[i];   
            }
            
        }
        if (name == "sprinkler")
        {
            itemIcon = sprites[0];
        }
    }

    public Item()
    {

    }


}
