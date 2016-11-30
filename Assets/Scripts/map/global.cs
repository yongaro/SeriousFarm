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
	public static GameObject Meteo;
	
	public MapObject(){
		if (Meteo == null) {
			Meteo = GameObject.Find("GameTimer");
		}
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
	public virtual Item recolt() {
		Debug.Log("Je ne suis pas une plante");
		return null;
	}
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
public enum PlantList{ aubergine, ble, carotte, chou, chou_fleur, citrouille,
					   concombre, fraise, mais, navet, oignon, patate, poivron, salade, tomate, plant_number }

/**
 * Enumeration des mois 
 */
public enum Months{ Janvier, Fevrier, Mars, Avril, Mai, Juin,
					Juillet, Aout, Septembre, Octobre, Novembre, Decembre, months_number }


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
	public int quality;


	public int firstGoodMonth;
	public int lastGoodMonth;
	public int firstEndingMonth;
	public int lastEndingMonth;
	public int bonusCroissance;


	public Plant(PlantList type) : base() {
		initStatic();
		this.type = type;
		growthCur = 0;
		growthMax = 30;
		waterCons = 5;
		quality = 0;
		initGrowthSettings();
		determineGrowthStep();
		updateSprite();
	}

	
	public override Item recolt() {
		
		Debug.Log("Je suis une plante");
        return new Item(type.ToString(), 0, "miam miam", 10, 0, 1, quality, Item.ItemType.Plante);
		
	}

	public int getMonth() {
		return Map.currentMonth;
	}
	public bool propiceMonth() {
		return (getMonth() >= firstGoodMonth) && (getMonth() <= lastGoodMonth);
	}

	public bool growableMonth() {
		return (getMonth() <= lastGoodMonth) && (getMonth() <= firstEndingMonth);
	}

	public bool latestMonth() {
		return (getMonth() >= firstEndingMonth) && (getMonth() <= lastEndingMonth);
	}
	
	public void determineGrowthStep () {
		if (propiceMonth()) {
			bonusCroissance = 30;
			quality = 100;
		} else if (growableMonth()) {
			growthStep /= 2;
			bonusCroissance = 10;
			quality = 70;
		} else if (latestMonth()) {
			growthStep /= 3;
			bonusCroissance = 5;
			quality = 60;
		}
	}

	public void growth() { 
		if ((growthCur < growthMax)){ 
			growthCur += growthStep;
			int bonus = Random.Range(0, 100);
			if (bonus <= bonusCroissance) {
				growthCur += 1;
			}
			updateSprite();
		} else {
			quality -= 2;
		}
	}
	public override void beginDay(){}
	public override void endDay(){
		MapTile tile = map.tileAt(mapX,mapY);
		if (tile != null) {
			if (tile.waterCur >= waterCons) {
				growth();
				tile.waterCur -= waterCons;
				} else {
					quality -= 10;
					if (quality < 0) {
						quality = 0;
					}
				}
		}
	}
	public override void activate(){
		//TODO donner un fruit une fois growthMax
	}


	public static void initStatic(){
        if( bank == null ) {
            bank = Resources.LoadAll<Sprite>("celian");
			//beginMonths = new List<Months>[(int)Months.months_number];
			//endMonths = new List<Months>[(int)Months.months_number];

			bankIndices = new int [(int)PlantList.plant_number, 3];
            bankIndices[(int)PlantList.carotte, 0] = 36;
            bankIndices[(int)PlantList.carotte, 1] = 37;
            bankIndices[(int)PlantList.carotte, 2] = 38;
 
            bankIndices[(int)PlantList.navet, 0] = 0;
            bankIndices[(int)PlantList.navet, 1] = 1;
            bankIndices[(int)PlantList.navet, 2] = 2;
 
            bankIndices[(int)PlantList.chou, 0] = 3;
            bankIndices[(int)PlantList.chou, 1] = 4;
            bankIndices[(int)PlantList.chou, 2] = 5;
 
            bankIndices[(int)PlantList.oignon, 0] = 6;
            bankIndices[(int)PlantList.oignon, 1] = 7;
            bankIndices[(int)PlantList.oignon, 2] = 8;
           
            bankIndices[(int)PlantList.tomate, 0] = 9;
            bankIndices[(int)PlantList.tomate, 1] = 10;
            bankIndices[(int)PlantList.tomate, 2] = 11;
 
            bankIndices[(int)PlantList.patate, 0] = 27;
            bankIndices[(int)PlantList.patate, 1] = 28;
            bankIndices[(int)PlantList.patate, 2] = 29;
 
            bankIndices[(int)PlantList.mais, 0] = 30;
            bankIndices[(int)PlantList.mais, 1] = 31;
            bankIndices[(int)PlantList.mais, 2] = 32;
 
            bankIndices[(int)PlantList.ble, 0] = 33;
            bankIndices[(int)PlantList.ble, 1] = 34;
            bankIndices[(int)PlantList.ble, 2] = 35;
 
            bankIndices[(int)PlantList.aubergine, 0] = 48;
            bankIndices[(int)PlantList.aubergine, 1] = 49;
            bankIndices[(int)PlantList.aubergine, 2] = 50;
 
            bankIndices[(int)PlantList.citrouille, 0] = 54;
            bankIndices[(int)PlantList.citrouille, 1] = 55;
            bankIndices[(int)PlantList.citrouille, 2] = 56;
 
            bankIndices[(int)PlantList.poivron, 0] = 57;
            bankIndices[(int)PlantList.poivron, 1] = 58;
            bankIndices[(int)PlantList.poivron, 2] = 59;
 
            bankIndices[(int)PlantList.concombre, 0] = 72;
            bankIndices[(int)PlantList.concombre, 1] = 73;
            bankIndices[(int)PlantList.concombre, 2] = 74;
           
            bankIndices[(int)PlantList.fraise, 0] = 75;
            bankIndices[(int)PlantList.fraise, 1] = 76;
            bankIndices[(int)PlantList.fraise, 2] = 77;
 
            bankIndices[(int)PlantList.salade, 0] = 78;
            bankIndices[(int)PlantList.salade, 1] = 79;
            bankIndices[(int)PlantList.salade, 2] = 80;
 
 
            bankIndices[(int)PlantList.chou_fleur, 0] = 51;
            bankIndices[(int)PlantList.chou_fleur, 1] = 52;
            bankIndices[(int)PlantList.chou_fleur, 2] = 53;
        }
    }

    public void initGrowthSettings () {
    	switch (type) {
    		case PlantList.aubergine :
	    		firstGoodMonth = (int)Months.Fevrier;
	    		lastGoodMonth = (int)Months.Avril;
				firstEndingMonth = (int)Months.Juin;
				lastEndingMonth = (int)Months.Septembre;
				growthStep = 2;
    		break;
    		case PlantList.ble :
	    		firstGoodMonth = (int)Months.Septembre;
	    		lastGoodMonth = (int)Months.Novembre;
				firstEndingMonth = (int)Months.Juin;
				lastEndingMonth = (int)Months.Aout;
				growthStep = 1;
    		break;
    		case PlantList.carotte :
	    		firstGoodMonth = (int)Months.Fevrier;
	    		lastGoodMonth = (int)Months.Septembre;
				firstEndingMonth = (int)Months.Novembre;
				lastEndingMonth = (int)Months.Janvier;
				growthStep = 4;
    		break;
    		case PlantList.chou :
	    		firstGoodMonth = (int)Months.Septembre;
	    		lastGoodMonth = (int)Months.Juin;
				firstEndingMonth = (int)Months.Juillet;
				lastEndingMonth = (int)Months.Janvier;
				growthStep = 3;
    		break;
    		case PlantList.chou_fleur :
	    		firstGoodMonth = (int)Months.Mars;
	    		lastGoodMonth = (int)Months.Juin;
				firstEndingMonth = (int)Months.Aout;
				lastEndingMonth = (int)Months.Octobre;
				growthStep = 2;
    		break;
    		case PlantList.citrouille :
	    		firstGoodMonth = (int)Months.Avril;
	    		lastGoodMonth = (int)Months.Juin;
				firstEndingMonth = (int)Months.Juillet;
				lastEndingMonth = (int)Months.Aout;
				growthStep = 4;
    		break;
    		case PlantList.concombre :
	    		firstGoodMonth = (int)Months.Mars;
	    		lastGoodMonth = (int)Months.Juillet;
				firstEndingMonth = (int)Months.Juillet;
				lastEndingMonth = (int)Months.Novembre;
				growthStep = 3;
    		break;
    		case PlantList.fraise :
	    		firstGoodMonth = (int)Months.Aout;
	    		lastGoodMonth = (int)Months.Octobre;
				firstEndingMonth = (int)Months.Mai;
				lastEndingMonth = (int)Months.Juillet;
				growthStep = 1;
				//http://www.gnis.fr/index/action/page/id/531/title/Reussir-la-culture-des-fraisiers
    		break;
    		case PlantList.mais :
	    		firstGoodMonth = (int)Months.Avril;
	    		lastGoodMonth = (int)Months.Juin;
				firstEndingMonth = (int)Months.Juin;
				lastEndingMonth = (int)Months.Aout;
				growthStep = 3;
    		break;
    		case PlantList.navet :
	    		firstGoodMonth = (int)Months.Mars;
	    		lastGoodMonth = (int)Months.Aout;
				firstEndingMonth = (int)Months.Septembre;
				lastEndingMonth = (int)Months.Novembre;
				growthStep = 4;
    		break;
    		case PlantList.oignon :
	    		firstGoodMonth = (int)Months.Mars;
	    		lastGoodMonth = (int)Months.Avril;
				firstEndingMonth = (int)Months.Aout;
				lastEndingMonth = (int)Months.Septembre;
				growthStep = 3;
    		break;
    		case PlantList.patate :
	    		firstGoodMonth = (int)Months.Fevrier;
	    		lastGoodMonth = (int)Months.Avril;
				firstEndingMonth = (int)Months.Mai;
				lastEndingMonth = (int)Months.Octobre;
				growthStep = 3;
    		break;
    		case PlantList.poivron :
	    		firstGoodMonth = (int)Months.Fevrier;
	    		lastGoodMonth = (int)Months.Mai;
				firstEndingMonth = (int)Months.Aout;
				lastEndingMonth = (int)Months.Octobre;
				growthStep = 1;
    		break;
    		case PlantList.salade :
    		break;
    		case PlantList.tomate :
	    		firstGoodMonth = (int)Months.Janvier;
	    		lastGoodMonth = (int)Months.Decembre;
				firstEndingMonth = (int)Months.Juillet;
				lastEndingMonth = (int)Months.Septembre;
				growthStep = 3;
    		break; 
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
	public void removeObject(){
		
		m_object = null;
	} //Incomplet ?

	public void useTool(FarmTools tool){
		if( tool < FarmTools.WateringCan ){
            if (m_object != null)
			if( m_object.destroyWithTool(tool) ){ removeObject(); }
		}
		else{ water();  }
	}

	public Item recolt() {
		if (m_object != null) {
			return m_object.recolt();	
		} else {
			return null;
		}
	}
	
	public void setPosition(int x, int y, Transform worldPos){
		
	}
	public void setSprite(string spriteName){
		//var spriteShiet = Resources.Load<Sprite>("fubar");
	}
}

public enum EventType{ rain, snow, event_number };


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
	public static int currentMonth;

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
	
	
	public Map() {
		width = 10;
		height = 10;
		currentMonth = 0;
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

	/**
	 * Fonction permettant de changer le mois courant
	 */
	public static void setMonth (int numMonth){
		currentMonth = numMonth;
	}

	/**
	 * Fonction declenchant le debut de journee sur toutes les zones cultivables listees
	 */
	public static void globalBeginDay(){
		if( mapList != null ){
			foreach( Map mapRef in mapList ){
				mapRef.beginDay();
			}
		}
	}

	/**
	 * Fonction declenchant la fin de journee sur toutes les zones cultivables listees
	 */
	public static void globalEndDay(){
		if( mapList != null ){
			foreach( Map mapRef in mapList ){
				mapRef.endDay();
			}
		}
	}

	/**
	 * Fonction gerant les evenements meteo 
	 */
	public static void globalEvent(EventType type){
		if( type == EventType.rain ){
			foreach( Map mapRef in mapList ){
				for( int x = 0; x < mapRef.width; ++x ){
					for( int y = 0; y < mapRef.height; ++y ){
						mapRef.map[x,y].water();
					}
				}
			}
		}
		
	}

	/**
	 * Utilisation d outils sur une case designe par tilePos sa localisation dans le repere de la scene 
	 */
	public static void useTool(FarmTools tool, Vector3 tilePos){
		MapTile tile = Map.getTileAt(tilePos);
		if( tile != null ){
            tile.useTool(tool); }		
	}

	/**
	 * Ajoute une plante a la case designed par tilePos sa localisation dans le repere de la scene
	 *
	 */
	public static bool ajoutPlante(PlantList type, Vector3 tilePos){
		MapTile tile = Map.getTileAt(tilePos);
		if( tile != null ){
			if( tile.m_object == null ){
				Plant ajout = new Plant(type);
				tile.addObject(ajout);
				return true;
			}
		}
		return false;
	}

	/**
	 * Collecte une plante a la case designed par tilePos sa localisation dans le repere de la scene
	 *
	 */
	public static Item collectPlant(Vector3 tilePos) {
		MapTile tile = Map.getTileAt(tilePos);
		if (tile != null){
			//Debug.Log("Je suis sur une plante");
			return tile.recolt();
			/*
			if( tile.m_object == null ){
				Plant ajout = new Plant(type);
				tile.addObject(ajout);
				return true;
			}*/
		} else {
			Debug.Log("Je ne suis pas sur une plante");
			return null;
		}
		//return plant as Plant;
	}
	
}
