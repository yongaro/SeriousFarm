using UnityEngine;
using System.Collections;

public class FM_BruitDePas : MonoBehaviour {
    FMOD.Studio.EventInstance marche;
    PlayerController pc;
    
    void Start()
    {
        pc = transform.GetComponent<PlayerController>();
        marche = FMODUnity.RuntimeManager.CreateInstance("event:/Deplacement/humain-terre");
    }

    void Update()
    {
        if (!pc.playerMoving)
            marche.start();
    }



}
