using UnityEngine;
using System.Collections;
using System.Collections.Generic;



/**
 * Enumeration de tous les outils utilisables
 */
public enum FarmTools{  Axe, Hoe, Pickaxe, Scythe, WateringCan }



/**
 * Interface (sens java) de manipulation des objets
 * Définit si les objets peuvent être traversés
 * Détermine un comportement journalier en début et fin de journée
 * Un objet possede ses coordonnées sur la map pour éventuellement agir sur d'autres objets de la map
 * ou vérifier certaines conditions auprès des cases voisines
 */
public abstract class MapObject{
	public int mapX;
	public int mapY;
	public bool collision;
	public FarmTools tool2Destroy;
	public Map map;
	public GameObject objectView;
	
	public MapObject(){
		mapX = 0;
		mapY = 0;
		collision = false;
		tool2Destroy = FarmTools.Pickaxe;
		map = null;
		objectView = new GameObject("MapObject");
		objectView.AddComponent<SpriteRenderer>();
		objectView.GetComponent<SpriteRenderer>().sortingOrder = 1;
	}
	public virtual bool collide(){ return collision; }
	public virtual void beginDay(){}
	public virtual void endDay(){}
	public virtual void activate(){}
	public virtual bool destroyWithTool(FarmTools tool){ return tool == tool2Destroy; }
	public virtual void moveGameObject(){
		Vector3 newPos = new Vector3();
		newPos.x = map.pos.x + mapX * map.tileSize;
		newPos.y = map.pos.y + mapY * map.tileSize;
		objectView.transform.position = newPos;
	}
}

/**
 * Structure permettant de contenir tous les sprites d un legume pour
 * toutes ses etapes de croissance.
 */
/*
public class LegumeSprites{
	public Sprite[] sprites;
	public int nbSprites;
	
	public LegumeSprites(){}
	public LegumeSprites(int nb, string[] spritesPath){
		sprites = new Sprite[nb];
		for( int i = 0; i < nb; ++i ){
			sprites[i] = Resources.Load<Sprite>(spritesPath[i]);
		}
	}
}*/
/**
 * Enumeration de toutes les plantes possibles
 */
public enum PlantList{ carotte, chou_fleur, navet, plant_number } // @TODO : mettre nom ici

/**
 * Classe generique pour toutes les plantes du jeu
 * Des points de parametrage permettent de definir 
 *   la croissance a chaque journee
 *   la consomation d eau
 *   la fin de croissance 
 *   etc
 */
public class Plant : MapObject {
	public static Sprite[] bank;
	public static int[,] bankIndices;
	public PlantList type;
	public int growthCur;
	public int growthMax;
	public int growthStep;
	public int waterCons;

	public Plant(PlantList type) : base(){
		this.type = type;
		initStatic();
		growthCur = 0;
		growthMax = 16;
		growthStep = 1;
		waterCons = 5;
		updateSprite();
	}

	public void growth(){ 
		if( growthCur < growthMax ){ 
			growthCur += growthStep; 
			updateSprite();
		} 
	}
	public override void beginDay(){}
	public override void endDay(){
		MapTile tile = map.tileAt(mapX,mapY);
		if( tile != null ){
			if( tile.waterCur >= waterCons ){ growth(); tile.waterCur -= waterCons; }
		}
	}
	public override void activate(){
		//TODO donner un fruit une fois growthMax
	}


	public static void initStatic(){
		if( bank == null ) {
			bank = Resources.LoadAll<Sprite>("celian");
			bankIndices = new int [(int)PlantList.plant_number, 3];
			bankIndices[(int)PlantList.carotte, 0] = 36;
			bankIndices[(int)PlantList.carotte, 1] = 37;
			bankIndices[(int)PlantList.carotte, 2] = 38;

			bankIndices[(int)PlantList.navet, 0] = 0;
			bankIndices[(int)PlantList.navet, 1] = 1;
			bankIndices[(int)PlantList.navet, 2] = 2;

			bankIndices[(int)PlantList.chou_fleur, 0] = 51;
			bankIndices[(int)PlantList.chou_fleur, 1] = 52;
			bankIndices[(int)PlantList.chou_fleur, 2] = 53;
		}
	}
	
	public void updateSprite () {
		float stade = ((float)growthCur / (float)growthMax);
		if (stade < 0.33) {
			objectView.GetComponent<SpriteRenderer>().sprite = bank[ bankIndices[(int)type, 0] ];
		} else if (stade < 0.66) {
			objectView.GetComponent<SpriteRenderer>().sprite = bank[ bankIndices[(int)type, 1] ];
		} else {
			objectView.GetComponent<SpriteRenderer>().sprite = bank[ bankIndices[(int)type, 2] ];
		}
	}
}

/**
 * Objet d arrosage automatique
 * en debut de chaque journee arrose ses 8 voisins
 */
public class Sprinkler : MapObject{

	public Sprinkler() : base(){
		objectView.AddComponent<BoxCollider2D>();
		collision = false;
	}

	public override void beginDay(){
		MapTile tile;
		for( int x = -1; x < 2; ++x ){
			for( int y = -1; y < 2; ++y ){
				tile = map.tileAt(mapX+x,mapY+y);
				if( tile != null ){ tile.water(); }
			}
		}  
	}
	public override bool destroyWithTool(FarmTools tool){ return tool == FarmTools.Pickaxe; }
}



/**
 * Enumeration des differents types d'obstacles 
 */
public enum ObstacleType { Bois, Rocher, obstacles_number }
 
/**
 * Classe gerant tous les objets de types obstacles ( pierres, branches, etc...)
 */
public class Obstacle : MapObject{
	public static Sprite[] obstacleSprites;
	public static int[] obstaclesIndices;
	
	
	public static void initStatic(){
		if( obstacleSprites == null ){
			obstacleSprites = Resources.LoadAll<Sprite>("champ");
			obstaclesIndices = new int[(int)ObstacleType.obstacles_number];
			obstaclesIndices[(int)ObstacleType.Bois] = 297;
			obstaclesIndices[(int)ObstacleType.Rocher] = 292;
		}
	}
	
	public Obstacle() : base(){
		objectView.AddComponent<BoxCollider2D>();
		collision = false;
		initStatic();
	}
	
	public void defineType(ObstacleType type){
		if( type == ObstacleType.Bois ){ objectView.GetComponent<SpriteRenderer>().sprite = obstacleSprites[ obstaclesIndices[(int)ObstacleType.Bois] ]; }
		if( type == ObstacleType.Rocher ){ objectView.GetComponent<SpriteRenderer>().sprite = obstacleSprites[ obstaclesIndices[(int)ObstacleType.Bois] ]; }
	}
}




/**
 * Tuile d une zone cultivable 
 * Represente la terre qui dispose de plusieurs socles pour y inserer divers composants
 * Gere egalement le volume d eau ce morceau de terrain et dispose de quelques informations 
 * pratiques pour d autres utilisations
 */
public class MapTile {
	public int tileX;
	public int tileY;
	public int waterCur;
	public int waterMax;
	public Map map;
	public MapObject m_object;
	
	public GameObject tileView;
	
	public MapTile(){
		tileX = 0;
		tileY = 0;
		waterCur = 0;
		waterMax = 10;
		m_object = null;
	}
	
	
	public bool collide(){
		if( m_object != null ){ return false; }
		else{ return m_object.collide(); }
	}
	
	public void beginDay(){ if( m_object != null ){ m_object.beginDay(); } }
	public void endDay(){ if( m_object != null ){ m_object.endDay(); } }
	public void water(){ waterCur = waterMax; }
	public void addObject(MapObject obj){
		m_object = obj;
		m_object.mapX = tileX;
		m_object.mapY = tileY;
		m_object.map = map;
		m_object.moveGameObject();
	}
	public void removeObject(){ m_object = null; } //Incomplet ?
	public void useTool(FarmTools tool){
		if( tool < FarmTools.WateringCan ){
			if( m_object.destroyWithTool(tool) ){ removeObject(); }
		}
		else{ water(); }
	}
	public void setPosition(int x, int y, Transform worldPos){
		
	}
	public void setSprite(string spriteName){
		//var spriteShiet = Resources.Load<Sprite>("fubar");
	}
}



/**
 * Classe contenant une zone cultivable et accessible depuis les autres scripts
 * Matrice de MapTile
 */
public class Map{
	public int width;
	public int height;
	public Vector3 pos;
	public float tileSize;
	public MapTile[,] map;

	/**
	 * Sprites a fournir depuis une Map Instance pour creer une zone carree / rectangulaire
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

	//Gestion de l'ensemble des maps sur une scene
	private static List<Map> mapList;
	
	
	public Map(){
		width = 10;
		height = 10;
	}
	public void init(int w, int h, float tileSize, Transform worldPos){
		width = w;
		height = h;
		pos = new Vector3();
		pos.x = worldPos.position.x;
		pos.y = worldPos.position.y;
		this.tileSize = tileSize;

		map = new MapTile[width,height];
		Vector3 temp;
		for( int x = 0; x < width; ++x ){
			for( int y = 0; y < height; ++y ){
				map[x,y] = new MapTile();
				map[x,y].tileX = x;
				map[x,y].tileY = y;
				map[x,y].map = this;
				map[x,y].tileView = new GameObject("MapTile");
				map[x,y].tileView.AddComponent<SpriteRenderer>();
				map[x,y].tileView.GetComponent<SpriteRenderer>().sprite = M_Sprite;
				temp = new Vector3();
				temp.x = worldPos.position.x + x*tileSize;
				temp.y = worldPos.position.y + y*tileSize;
				map[x,y].tileView.transform.position = temp;
			}
		}
	}
	public MapTile tileAt(int x, int y){
		if( x < 0 || y < 0 || x >= width || y >= height ){
			return null;
		}
		return map[x,y];
	}
	
	public void beginDay(){
		for( int x = 0; x < width; ++x ){
			for( int y = 0; y < height; ++y ){
				map[x,y].beginDay();
			}
		}
	}
	public void endDay(){
		for( int x = 0; x < width; ++x ){
			for( int y = 0; y < height; ++y ){
				map[x,y].endDay();
			}
		}
	}

	/**
	 * Permet de trouver une tuile grace a ses coordonnees reelles sur la scene
	 * Une tuile est situee selon le centre de son GameObject il faut donc utiliser une offset 
	 * supplementaire de la moitie de la taille d une tuile pour calculer la position dans la matrice
	 */
	public MapTile tileAt(Vector3 posRef){
		float halfSize = tileSize / 2.0f;
		float tileWorldPosX = 0.0f;
		float tileWorldPosY = 0.0f;

		for( int x = 0; x < width; ++x ){
			for( int y = 0; y < height; ++y ){
				tileWorldPosX = pos.x + x*tileSize;
				if( posRef.x >= (tileWorldPosX-halfSize) && posRef.x < (tileWorldPosX+halfSize) ){
					tileWorldPosY = pos.y + y*tileSize;
					if( posRef.y >= (tileWorldPosY-halfSize) && posRef.y < (tileWorldPosY+halfSize) ){
						return tileAt(x,y);
					}
				}
			}
		}
		return null;
	}

	/**
	 * Ajoute une zone cultivable a une liste statique qui repertorie toutes les zones.
	 * Cette structure sert a modifier une tuile de n importe quelle zone cultivable
	 * en utilisant uniquement une position sur le repere de la scene.
	 */
	public static void ajoutMap(Map mapRef){
		if( mapList == null ){ mapList = new List<Map>(); }
		if( !mapList.Contains(mapRef) ){ mapList.Add(mapRef); }
	}

	/**
	 * Parcours toutes les zones cultivables repertoriees et renvoie 
	 * une reference vers MapTile a la position designee par posRef(repere de la scene)
	 * retourne null si aucune tuile n a ete trouve
	 */
	public static MapTile getTileAt(Vector3 posRef){
		MapTile res = null;
		foreach( Map mapRef in mapList ){
			res = mapRef.tileAt(posRef);
			if( res != null ){ return res; }
		}
		return null;
	}
	
}
