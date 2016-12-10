using UnityEngine;
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
	public Sprite ULR_Sprite;
	
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
		map.ULR_Sprite = ULR_Sprite;
		float tileSize = M_Sprite.bounds.size.x;
		map.init(width, height, tileSize, transform);
		//ajout de la zone cultivable dans la liste des map
		Map.ajoutMap(map);
		test_tileFromWorldPos();
		Map.randomFillAll();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	/**
	 * Test unitaire a effectuer avec au moins une map placee en (0,0) et w et h > 1
	 * doit renvoyer vrai et afficher "1 1"
	 */
	bool test_tileFromWorldPos(){
		Vector3 testPos = new Vector3();
		testPos.x = -15.0f;
		testPos.y = 1.0f;
		testPos.z = 0.0f;
		MapTile test1 = Map.getTileAt(testPos);
		testPos.x = -18.0f;
		testPos.y = 1.0f;
		testPos.z = 0.0f;
		MapTile test2 = Map.getTileAt(testPos);
		if( test1 == null && test2 == null ){ print("NURUPO"); }
		else{
			Sprinkler obj1 = new Sprinkler();
			test1.addObject(obj1);
			
			GenericObject obj2 = new GenericObject();
			obj2.defineType(GenericObjectTypes.Torche);
			test2.addObject(obj2);

			
			Map.randomFillAll();
			return true;
			
		}
		
		
		return false;
	}

	bool test_ajoutObjet(){

		return false;
	}
}
