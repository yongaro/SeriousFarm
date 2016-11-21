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
        Debug.Log(objectCurrent.itemName);
        tool = transform.GetChild(0).gameObject;
        tool.GetComponent<toolController>().currentTool = toolController.FarmerTools.Pelle;
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
                if (objectCurrent.itemName == "pelle")
                    tool.GetComponent<toolController>().currentTool = toolController.FarmerTools.Pelle;
                if (objectCurrent.itemName == "Axe")
                    tool.GetComponent<toolController>().currentTool = toolController.FarmerTools.Axe;
                if (objectCurrent.itemName == "Hoe")
                    tool.GetComponent<toolController>().currentTool = toolController.FarmerTools.Hoe;
                if (objectCurrent.itemName == "Pickaxe")
                    tool.GetComponent<toolController>().currentTool = toolController.FarmerTools.Pickaxe;
                if (objectCurrent.itemName == "Scythe")
                    tool.GetComponent<toolController>().currentTool = toolController.FarmerTools.Scythe;
                if (objectCurrent.itemName == "WateringCan")
                {
                    tool.GetComponent<toolController>().currentTool = toolController.FarmerTools.WateringCan;
                }
            }
            else
            {
                objet.GetComponent<SpriteRenderer>().sprite = objectCurrent.itemIcon;
                objet.SetActive(true);
                tool.SetActive(false);
                tool.GetComponent<toolController>().currentTool = toolController.FarmerTools.Pelle;
            }
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
            else if (objectCurrent.itemName == "WateringCan")
            {
                if (objectCurrent.itemPower > 0)
                    objectCurrent.itemPower--;

            }
        }
        
    }


}
