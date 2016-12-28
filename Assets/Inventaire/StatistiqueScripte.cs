using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatistiqueScripte : MonoBehaviour {

    Text textarrosoir;
    int arrosoirStat;

	// Use this for initialization
	void Start () {
	    textarrosoir = gameObject.transform.GetChild(1).GetComponent<Text>();
        arrosoirStat = 0;

    }
	
	// Update is called once per frame
	void Update () {
        textarrosoir.text = " / jour     :     " + arrosoirStat;
        arrosoirStat = Map.getDailyWaterCons(); 
	}
}
