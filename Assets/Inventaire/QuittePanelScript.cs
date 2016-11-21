using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

public class QuittePanelScript : MonoBehaviour {

    Button quitterButton;
    Button pauseButton;
    Button saveButton;
    bool isPause;


	// Use this for initialization
	void Start () {
        quitterButton = this.transform.GetChild(0).gameObject.GetComponent<Button>();
        quitterButton.onClick.AddListener(quitterJeu);
        saveButton = this.transform.GetChild(1).gameObject.GetComponent<Button>();
        saveButton.onClick.AddListener(saveJeu);
        pauseButton = this.transform.GetChild(2).gameObject.GetComponent<Button>();
        pauseButton.onClick.AddListener(pauseJeu);
	}
	
    
    void quitterJeu()
    {
        Debug.Log("Quuuiiiiter");
        Application.Quit();
    }

    void saveJeu()
    {
        Debug.Log("savvvvve");
    }
	
    void pauseJeu()
    {
        if (isPause)
        {
            Debug.Log("pause");
            Time.timeScale = 0;
            isPause = false;
        }
        else
        {
            Debug.Log("pasPause");
            Time.timeScale = 1;
            isPause = true;
        }
    }


}
