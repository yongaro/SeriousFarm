﻿using UnityEngine;
using System.Collections;

public class ObjectController : MonoBehaviour {

    public Item objectCurrent;
    ItemDatabase itemDatabase;
    GameObject tool;
    GameObject objet;
    string pathSoundObject;

	// Use this for initialization
	void Start () {
        itemDatabase =  GameObject.FindGameObjectWithTag("ItemDatabase").GetComponent<ItemDatabase>();
        
        objectCurrent = itemDatabase.items[4];
        tool = transform.GetChild(0).gameObject;
        tool.GetComponent<toolController>().currentTool = FarmTools.Axe;
        objet = transform.GetChild(1).gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        if (objectCurrent != null)
        {
            if (objectCurrent.itemType == Item.ItemType.Tool)
            {
                //tool.SetActive(true);
                objet.SetActive(false);
                if (objectCurrent.itemName == "Axe")
                {
                    tool.GetComponent<toolController>().currentTool = FarmTools.Axe;
                    pathSoundObject = "event:/Outil/Hache";
                }
                if (objectCurrent.itemName == "Hoe")
                {
                    tool.GetComponent<toolController>().currentTool = FarmTools.Hoe;
                    pathSoundObject = "event:/Outil/Binette";
                }
                if (objectCurrent.itemName == "Pickaxe")
                {
                    tool.GetComponent<toolController>().currentTool = FarmTools.Pickaxe;
                    pathSoundObject = "event:/Outil/Pioche";
                }
                if (objectCurrent.itemName == "Scythe")
                {
                    tool.GetComponent<toolController>().currentTool = FarmTools.Scythe;
                    pathSoundObject = "event:/Outil/Fauche";
                }
                if (objectCurrent.itemName == "WateringCan")
                {
                    tool.GetComponent<toolController>().currentTool = FarmTools.WateringCan;
                    pathSoundObject = "event:/Outil/Arrosoir";
                }
            }
            else
            {
                objet.GetComponent<SpriteRenderer>().sprite = objectCurrent.itemIcon;
                objet.SetActive(true);
                tool.SetActive(false);
                tool.GetComponent<toolController>().currentTool = FarmTools.Axe;
            }
        } 
        else
        {
            objet.GetComponent<SpriteRenderer>().sprite = null;
        }  
	}

    public void useObject(Vector2 direction)
    {
       
        Vector3 pos = transform.position;
        if (objectCurrent != null)
        {
            if (objectCurrent.itemType != Item.ItemType.Tool)
            {
                if (objectCurrent.itemType == Item.ItemType.Graine)
                {
                    for (int i = 0; i < (int)PlantList.plant_number; i++)
                    {
                        if (((PlantList)i).ToString() == objectCurrent.itemName)
                            if (Map.ajoutPlante(((PlantList)i), new Vector3(pos.x + direction.x, pos.y + direction.y, 0)))
                            {
                                objectCurrent.itemValue--;
                                pathSoundObject = "event:/Outil/Semer";
                                FM_SonScript.sonOutil(pathSoundObject);
                            }

                    }
                }
                else if (objectCurrent.itemType == Item.ItemType.Mobilier)
                {
                    MapTile tile = Map.getTileAt(new Vector3(pos.x + direction.x, pos.y + direction.y, 0));
                    if (tile != null)
                    {
                        if (objectCurrent.itemName == "sprinkler")
                        {
                            tile.addObject(new Sprinkler());
                            objectCurrent.itemValue--;
                        }
                    }
                }

                else
                {
                    objectCurrent.itemValue--;
                }
                if (objectCurrent.itemValue <= 0)
                {
                    objet.SetActive(false);
                    objectCurrent = new Item();
                }
            }
            else
            {
                if (objectCurrent.itemName != "WateringCan" || (objectCurrent.itemName == "WateringCan" && objectCurrent.itemPower > 0))
                {
                    Map.useTool(tool.GetComponent<toolController>().currentTool, new Vector3(pos.x + direction.x, pos.y + direction.y, 0));
                    FM_SonScript.sonOutil(pathSoundObject);
                    
                }
                if (objectCurrent.itemName == "WateringCan")
                    if (objectCurrent.itemPower > 0)    
                        objectCurrent.itemPower--;

            }
        }

    }

}
