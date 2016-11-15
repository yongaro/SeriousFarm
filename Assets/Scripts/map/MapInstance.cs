using UnityEngine;
using System.Collections;

public class MapInstance : MonoBehaviour {
	public Map map;
	
	public int width  = 10;
	public int height = 10;
	
	//Sprites a fournir pour creer une zone carree / rectangulaire
	//Un carre de 3x3 demande 9 tuiles differentes(4 coins + tuiles entre les coins + tuile centrale)
	//une zone de n importe quelle dimension peut etre affichee a partir de ces 9 tuiles
	//C=Corner -- D=Down -- L=Left -- M=Middle -- R = Right -- U=Up  
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
		map = new Map();
		map.DLC_Sprite = DLC_Sprite;
		map.DRC_Sprite = DRC_Sprite;
		map.M_Sprite   = M_Sprite;
		map.MD_Sprite  = MD_Sprite;
		map.ML_Sprite  = ML_Sprite;
		map.MR_Sprite  = MR_Sprite;
		map.MU_Sprite  = MU_Sprite;
		map.ULC_Sprite = ULC_Sprite;
		map.ULR_Sprite = ULR_Sprite;
		float tileSize = M_Sprite.bounds.size.x;//test.GetComponent<SpriteRenderer>().sprite.bounds.size.x; //erk
		//test.transform.position = transform.position;
		map.init(width, height, tileSize, transform);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
