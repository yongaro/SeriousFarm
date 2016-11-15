using UnityEngine;
using System.Collections;

public enum FarmTools{  Axe, Hoe, Pickaxe, Scythe, WateringCan }



//Interface (sens java) de manipulation des objets
//Définit si les objets peuvent être traversés
//Détermine un comportement journalier en début et fin de journée
//Un objet possede ses coordonnées sur la map pour éventuellement agir sur d'autres objets de la map
//ou vérifier certaines conditions auprès des cases voisines
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
		objectView = new GameObject();
		objectView.AddComponent<SpriteRenderer>();
		objectView.AddComponent<BoxCollider>();
	}
	public virtual bool collide(){ return collision; }
	public virtual void beginDay(){}
	public virtual void endDay(){}
	public virtual void activate(){}
	public virtual bool destroyWithTool(FarmTools tool){ return tool == tool2Destroy; }
	
}

//Une plante par défaut prend 16 jours pour donner son produit et doit être arrosée tous les 2 jours. 
public class Plant : MapObject {
	public int growthCur;
	public int growthMax;
	public int growthStep;
	public int waterCons;

	public Plant() : base(){
		  growthCur = 0;
		  growthMax = 16;
		  growthStep = 1;
		  waterCons = 5;
	}
	public void growth(){ if( growthCur < growthMax ){ growthCur += growthStep; } }
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
}


public class Sprinkler : MapObject{

	public Sprinkler() : base(){
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


//Objet contenant toutes les informations de chaque case d'une zone cultivable
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


//Classe contenant la map et accessible depuis les autres scripts
/**
* Matrice de MapTile
*/
public class Map{
	public int width;
	public int height;
	public Vector3 pos;
	public MapTile[,] map;
	
	public Sprite DLC_Sprite;
	public Sprite DRC_Sprite;
	public Sprite M_Sprite;
	public Sprite MD_Sprite;
	public Sprite ML_Sprite;
	public Sprite MR_Sprite;
	public Sprite MU_Sprite;
	public Sprite ULC_Sprite;
	public Sprite ULR_Sprite;

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
		
		map = new MapTile[width,height];
		Vector3 temp;
		for( int x = 0; x < width; ++x ){
			for( int y = 0; y < height; ++y ){
				map[x,y] = new MapTile();
				map[x,y].tileX = x;
				map[x,y].tileY = y;
				map[x,y].map = this;
				map[x,y].tileView = new GameObject();
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
}