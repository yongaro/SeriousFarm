using UnityEngine;
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

    public void useObject()
    {
        Debug.Log("ok");
        if (objectCurrent != null)
        {
            if (objectCurrent.itemType != Item.ItemType.Tool)
            {
                if (objectCurrent.itemType == Item.ItemType.Graine)
                {
                    for (int i = 0; i < (int)PlantList.plant_number; i++)
                    {
                        if (((PlantList) i).ToString() == objectCurrent.itemName)
                           if ( Map.ajoutPlante(((PlantList)i), transform.position))
                            {
                                objectCurrent.itemValue--;
                            }

                    }
                }
                else
                {
                    objectCurrent.itemValue--;
                }
                if (objectCurrent.itemValue <= 0) { 
                    objet.SetActive(false);
                    objectCurrent = new Item();
                        
                }
                
            }
            else 
            {
                FMODUnity.RuntimeManager.PlayOneShot(pathSoundObject);
                
                Map.useTool(tool.GetComponent<toolController>().currentTool,  transform.position);
                if (objectCurrent.itemName == "WateringCan")
                    if (objectCurrent.itemPower > 0)
                    objectCurrent.itemPower--;

            }
        }
        
    }


}
