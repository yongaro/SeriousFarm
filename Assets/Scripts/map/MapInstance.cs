﻿using UnityEngine;
using System.Collections;


/**
 * Seule et unique classe a utiliser pour instancier une zone cultivable dans la scene
 * Pour le moment seul le Sprite M_Sprite doit etre initialise depuis l'inspector de l editeur
 * (le prefab a disposition a cote du script propose une configuration par defaut)
 */
public class MapInstance : MonoBehaviour {
	public Map map;
	
	public int width  = 10;
	public int height = 10;
	
	/**
	 * Sprites a fournir pour creer une zone carree / rectangulaire
	 * Un carre de 3x3 demande 9 tuiles differentes(4 coins + tuiles entre les coins + tuile centrale)
	 * une zone de n importe quelle dimension peut etre affichee a partir de ces 9 tuiles
	 * C=Corner -- D=Down -- L=Left -- M=Middle -- R = Right -- U=Up  
	 */
	public Sprite DLC_Sprite;
	public Sprite DRC_Sprite;
	public Sprite M_Sprite;
	public Sprite MD_Sprite;
	public Sprite ML_Sprite;
	public Sprite MR_Sprite;
	public Sprite MU_Sprite;
	public Sprite ULC_Sprite;
	public Sprite URC_Sprite;
	
	// Use this for initialization
	void Start () {
		//Creation de la zone cultivable
		map = new Map();
		//configuration
		map.DLC_Sprite = DLC_Sprite;
		map.DRC_Sprite = DRC_Sprite;
		map.M_Sprite   = M_Sprite;
		map.MD_Sprite  = MD_Sprite;
		map.ML_Sprite  = ML_Sprite;
		map.MR_Sprite  = MR_Sprite;
		map.MU_Sprite  = MU_Sprite;
		map.ULC_Sprite = ULC_Sprite;
		map.URC_Sprite = URC_Sprite;
		float tileSize = M_Sprite.bounds.size.x;
		map.init(width, height, tileSize, transform);
		//ajout de la zone cultivable dans la liste des map
		Map.ajoutMap(map);
		Map.randomFillAll();
		
		demoFill();
	}
	
	// Update is called once per frame
	void Update(){ }
	
	void demoFill(){
		
		MapTile tile1 = map.tileAt(5,5);
		MapTile tile2 = map.tileAt(6,5);
		MapTile tile3 = map.tileAt(7,5);
		MapTile tile4 = map.tileAt(8,5);
		
		tile1.removeObject();
		tile2.removeObject();
		tile3.removeObject();
		
		Plant p1 = new Plant(PlantList.aubergine);
		tile1.addObject(p1);
		p1.growthCur = 999999;
		p1.quality = 150;
		p1.updateSprite();
		
		
		Plant p2 = new Plant(PlantList.aubergine);
		tile2.addObject(p2);
		p2.growthCur = 999999;
		p2.quality = 100;
		p2.updateSprite();
		
		
		Plant p3 = new Plant(PlantList.aubergine);
		tile3.addObject(p3);
		p3.growthCur = 999999;
		p3.quality = 74;
		p3.updateSprite();
	

		Plant p4 = new Plant(PlantList.aubergine);
		tile4.addObject(p4);
		p4.growthCur = 999999;
		p4.quality = 50;
		p4.updateSprite();
	}
	
}
