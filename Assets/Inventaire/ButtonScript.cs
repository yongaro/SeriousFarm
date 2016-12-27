using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public Button button;
    // Use this for initialization
    void Start()
    {
        button.onClick.AddListener(playSong);
    }

    private void playSong()
    {
        FM_SonScript.marchandButton();
    }

    void stopSonMagasin()
    {
        FM_SonScript.stopMagasin();
    }

}