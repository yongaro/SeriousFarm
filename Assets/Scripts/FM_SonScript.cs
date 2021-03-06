﻿using UnityEngine;
using System.Collections;

/*
 * Enumération des types de sols
 */
public enum TypeSol{ Herbe, Terre, Nb_TS};

/**
 * script qui gere les differents sons du jeu 
 * */
public class FM_SonScript {
    static FMOD.Studio.EventInstance son;   // different son de l'interface 
    static FMOD.Studio.EventInstance bruitMagasin;  // son de fond lorsque le magasin est ouvert
    static FMOD.Studio.EventInstance marche;    // son de marche du perso
    static bool marchedeja; // si le joueur est deja entrain de marché =  true
    //static bool terre;  // si le perso marche sur de la terre ou de l'herbe
	static TypeSol typeSol;
    static string pathMarche = "event:/Deplacement/humain-herbe"; // chemin du son pour les bruits de pas

    // Use this for initialization
    void Start () {
        marche = FMODUnity.RuntimeManager.CreateInstance("event:/Deplacement/humain-terre");
        marchedeja = false;
        //terre = false;
		FM_SonScript.changerTypeSol(TypeSol.Herbe);
    }

    // Update is called once per frame
    void Update () {
       
    }

    /**
   * son lorqu'on recolte un legume
   * change en fonction de la qualité du produit recolté
   **/

    public static  void  sonQualite(int qualite)
    {
        if (qualite / 25 >= 3)
        {
            
            son = FMODUnity.RuntimeManager.CreateInstance("event:/Legume/excelent");
        }
        else if (qualite / 25 == 2)
        {
            son = FMODUnity.RuntimeManager.CreateInstance("event:/Legume/tresBon");
        }
        else if (qualite / 25 == 1)
        {
            son = FMODUnity.RuntimeManager.CreateInstance("event:/Legume/bon");
        }
        else
        {
            son = FMODUnity.RuntimeManager.CreateInstance("event:/Legume/mauvais");
        }
        son.start();
    }

    /**
   * son quand on achete un objet dans le magasin
   **/


    public static void sonAchat()
    {
        son = FMODUnity.RuntimeManager.CreateInstance("event:/Magasin/achat");
        son.start();
    }

    /**
   * son de monnaie quand on vend un produit dans le magasin 
   * change en fonction de la qualité du produit
   **/

    public static void sonVente(int qualite)
    {
        if (qualite / 25 >= 3)
        {

            son = FMODUnity.RuntimeManager.CreateInstance("event:/Magasin/venteExcelente");
        }
        else if (qualite / 25 == 2)
        {
            son = FMODUnity.RuntimeManager.CreateInstance("event:/Magasin/venteTresBonne");
        }
        else if (qualite / 25 == 1)
        {
            son = FMODUnity.RuntimeManager.CreateInstance("event:/Magasin/venteBonne");
        }
        else
        {
            son = FMODUnity.RuntimeManager.CreateInstance("event:/Magasin/venteNul");
        }
        son.start();
    }

    /**
   * son quand on augmente la quantité de l'objet à vendre
   **/
    public static void sonBoutonPlusVente()
    {
        son = FMODUnity.RuntimeManager.CreateInstance("event:/Magasin/boutonPlus");
        son.start();
    }
    /**
     * son quand on dimunie la quantité de l'objet à vendre
     **/
    public static void sonBoutonMoinsVente()
    {
        son = FMODUnity.RuntimeManager.CreateInstance("event:/Magasin/boutonMoins");
        son.start();
    }

    /**
     * son lorsque la marchande apparait lorsque le legume n'est pas de saison
     **/
    public static void marchande()
    {
        son = FMODUnity.RuntimeManager.CreateInstance("event:/Magasin/Marchande");
        son.start();
    }
    /**
     * son de fond d'un magasin quand le joueur est dans le magasin
     * */
    public static void magasin()
    {
        bruitMagasin = FMODUnity.RuntimeManager.CreateInstance("event:/Magasin/brouhaha");
        bruitMagasin.start();
    }
    /**
     * arret du son du magasin quand le joueur sort le magasin
     * */
    public static void stopMagasin()
    {
        bruitMagasin.stop(0);
    }

    /**
     * son quand on click sur un boutton de l'interface
     * */
    public static void marchandButton()
    {
        son = FMODUnity.RuntimeManager.CreateInstance("event:/Magasin/bouton");
        son.start();
    }

    /**
     * son lorsqu'on selectionne un object que l'on veut acheter
     * */
    public static void marchandSelection()
    {
        son = FMODUnity.RuntimeManager.CreateInstance("event:/Magasin/selection");
        son.start();
    }

    /**
     * son lorsqu'on selectionne un object dans la quickBar
     * */

    public static void quickBarSelection()
    {
        son = FMODUnity.RuntimeManager.CreateInstance("event:/QuickBar/selection");
        son.start();
    }

    /**
     * son lorsqu'on jete un object dans la poubelle
     * */
     public static void poubelle()
    {
        son = FMODUnity.RuntimeManager.CreateInstance("event:/QuickBar/poubelle");
        son.start();
    }


    /**
     * bruit de l'outil utiliser
     * */
    public static void sonOutil(string path)
    {
        FMODUnity.RuntimeManager.PlayOneShot(path);
        Debug.Log(path);
    }

	public static void changerTypeSol(TypeSol type){
		if( type != typeSol ){
			typeSol = type;
			marche.stop(0);
			if( type == TypeSol.Herbe ){ pathMarche = "event:/Deplacement/humain-herbe"; }
			if( type == TypeSol.Terre ){ pathMarche = "event:/Deplacement/humain-terre"; }
			marche = FMODUnity.RuntimeManager.CreateInstance(pathMarche);
			marche.start();
		}
	}
	
    public static void sonPas(bool isMoving, Vector3 pos)
    {
        /*
        if (Map.getTileAt(pos) == null && terre == true)
        {
            marche.stop(0);
            pathMarche = "event:/Deplacement/humain-herbe";
            marche = FMODUnity.RuntimeManager.CreateInstance(pathMarche);
            marche.start();
            terre = false;
        }
        else if (Map.getTileAt(pos) != null && terre == false)
        {
            marche.stop(0);
            pathMarche = "event:/Deplacement/humain-terre";
            marche = FMODUnity.RuntimeManager.CreateInstance(pathMarche);
            marche.start();
            terre = true;
        }
		*/
        if (isMoving && !marchedeja)
        {
            marche = FMODUnity.RuntimeManager.CreateInstance(pathMarche);
            marche.start();
            marchedeja = true;
        }
        if (!isMoving && marchedeja)
        {
            marche.stop(0);
            marchedeja = false;
        }

    }

    public static void sonRobotDialogue()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Ambiance/robotDialogue");
    }

}
