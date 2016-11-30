using UnityEngine;
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
    public int itemSaison;  // 1 hiver , 2 printemps, 3 ete, 4 hiver
    public int itemValue;
    public int itemPrice;
    public ItemType itemType;

    public enum ItemType
    {
        Tool, Graine, Fruit, Pierre, Batton, Engrais, Pesticides, Insecticides, Mobilier 
    }


    public Item (string name, int id, string desc, int power, int saison, int value, int price, ItemType type)
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
        else if (type == ItemType.Graine)
            sprites = Resources.LoadAll<Sprite>("Graine");
        else
            sprites = Resources.LoadAll<Sprite>("Sprite");
        for (int i = 0; i < sprites.Length; i++) {
            
            if (sprites[i].name == name)
            {
                Debug.Log(sprites[i].name);
                itemIcon = sprites[i];
                
            }
        }
    }

    public Item()
    {

    }


}
