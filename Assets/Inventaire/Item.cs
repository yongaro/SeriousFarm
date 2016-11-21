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
    public int itemSpeed;
    public int itemValue;
    public ItemType itemType;

    public enum ItemType
    {
        Tool, Graine, Fruit, Pierre, Batton
    }


    public Item (string name, int id, string desc, int power, int speed, int value, ItemType type)
    {
        itemName = name;
        itemID = id;
        itemDesc = desc;
        itemPower = power;
        itemSpeed = speed;
        itemValue = value;
        itemType = type;
        if (type == ItemType.Tool)
            sprites = Resources.LoadAll<Sprite>("sprite2");
        else
            sprites = Resources.LoadAll<Sprite>("sprite");
        for (int i = 0; i < sprites.Length; i++) { 
            
            if (sprites[i].name == name)
            {
                itemIcon = sprites[i];
            }
        }
    }

    public Item()
    {

    }


}
