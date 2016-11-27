using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShopGlobalScrip : MonoBehaviour {

    public int monnaie;
    Text monnaieText;
    public GameObject monnaiePanel;


    // Use this for initialization
    void Start () {
        monnaie = 100;
        monnaieText = monnaiePanel.transform.GetChild(1).GetComponent<Text>();
        monnaieText.text = " : " + monnaie;
    }
	
	// Update is called once per frame
	void Update () {
        monnaieText.text = " : " + monnaie;
    }
}
