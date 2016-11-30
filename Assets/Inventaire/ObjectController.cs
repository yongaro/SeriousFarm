using UnityEngine;
using System.Collections;

public class ObjectController : MonoBehaviour {

    public Item objectCurrent;
    ItemDatabase itemDatabase;
    GameObject tool;
    GameObject objet;


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
                    tool.GetComponent<toolController>().currentTool = FarmTools.Axe;
                if (objectCurrent.itemName == "Hoe")
                    tool.GetComponent<toolController>().currentTool = FarmTools.Hoe;
                if (objectCurrent.itemName == "Pickaxe")
                    tool.GetComponent<toolController>().currentTool = FarmTools.Pickaxe;
                if (objectCurrent.itemName == "Scythe")
                    tool.GetComponent<toolController>().currentTool = FarmTools.Scythe;
                if (objectCurrent.itemName == "WateringCan")
                {
                    tool.GetComponent<toolController>().currentTool = FarmTools.WateringCan;
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
        if (objectCurrent != null)
        {
            if (objectCurrent.itemType != Item.ItemType.Tool)
            {
                objectCurrent.itemValue--;
                if (objectCurrent.itemValue <= 0) { 
                    objet.SetActive(false);
                    objectCurrent = new Item();
                        
                }
            }
            else 
            {
                Map.useTool(tool.GetComponent<toolController>().currentTool,  transform.position);
                if (objectCurrent.itemName == "WateringCan")
                    if (objectCurrent.itemPower > 0)
                    objectCurrent.itemPower--;

            }
        }
        
    }


}
