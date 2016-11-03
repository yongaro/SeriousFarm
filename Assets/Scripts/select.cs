using UnityEngine;
using System.Collections;

public class select : MonoBehaviour {

    InventaireController ic_script;
    toolController tc_script;
  
    // Use this for initialization
    void Start()
    {
        ic_script = GameObject.Find("Canvas").GetComponent<InventaireController>();
        tc_script = GameObject.Find("Outils").GetComponent<toolController>();

    }

    public void Selection()
    {
        
        int numSlot = transform.parent.GetSiblingIndex();
        if (gameObject.tag == "pelle")
        {
            Debug.Log("hhh");
            tc_script.currentTool = "pelle";
        }
        else if (gameObject.tag == "marteau")
        {
            Debug.Log("mmm");
            tc_script.currentTool = "marteau";
        }



    }
    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            int numSlot = transform.parent.GetSiblingIndex();
            ic_script.slots[numSlot] -= 1;
        }
    }
}
