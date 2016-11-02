#pragma strict


enum FarmTools{ Axe, Hoe, Pickaxe, Scythe, WateringCan }
//Interface de manipulation des objets
//Définit si les objets peuvent être traversés
//Détermine un comportement journalier en début et fin de journée
//Un objet possede ses coordonnées sur la map pour éventuellement agir sur d'autres objets de la map
//ou vérifier certaines conditions auprès des cases voisines
public class MapObject{
	 var mapX : int;
	 var mapY : int;
	 var collision : boolean;
	 var tool2Destroy : FarmTools;
	 var map : Map;

	 function MapObject(){
		  mapX = 0;
		  mapY = 0;
		  collision = false;
		  tool2Destroy = FarmTools.Pickaxe;
		  map = null;
	 }
	 function collide() : boolean{ return collision; }
	 function beginDay(){}
	 function endDay(){}
	 function activate(){}
	 function destroyWithTool(tool : FarmTools) : boolean { return tool == tool2Destroy; } 
}

//Une plante par défaut prend 16 jours pour donner son produit et doit être arrosée tous les 2 jours. 
public class Plant extends MapObject{
	 var growthCur : int;
	 var growthMax : int;
	 var growthStep : int;
	 var waterCons : int;

	 function Plant(){
		  super();
		  growthCur = 0;
		  growthMax = 16;
		  growthStep = 1;
		  waterCons = 5;
	 }
	 function growth(){ if( growthCur < growthMax ){ growthCur += growthStep; } }
	 function endDay(){
		  var tile : MapTile = map.tileAt(mapX,mapY);
		  if( tile != null ){
				if( tile.waterCur >= waterCons ){ growth(); tile.waterCur -= waterCons; }
		  }
	 }
	 function activate(){
		  //TODO donner un fruit une fois growthMax
	 }
}

public class Sprinkler extends MapObject{

	 function Sprinkler(){
		  super();
		  collision = false;
	 }
	 function beginDay(){
		  var tile : MapTile;
		  for( var x : int = -1; x < 2; ++x ){
				for( var y : int = -1; y < 2; ++y ){
					 tile = map.tileAt(mapX+x,mapY+y);
					 if( tile != null ){ tile.water(); }
				}
		  }
	 }
	 function collide() : boolean{ return true; }
	 function destroyWithTool(tool : FarmTools){ return tool == FarmTools.Pickaxe; }
}




//Objet contenant toutes les informations de chaque case d'une zone cultivable
public class MapTile{
	 var tileX : int;
	 var tileY : int;
	 var waterCur : int;
	 var waterMax : int;
	 var object : MapObject;

	 
	 function MapTile(){
		  tileX = 0;
		  tileY = 0;
		  waterCur = 0;
		  waterMax = 10;
		  object = null;
	 }
	 function collide() : boolean{
		  if( object != null ){ return false; }
		  else{ return object.collide(); }
	 }
	 function beginDay(){ if( object != null ){ object.beginDay(); } }
	 function endDay(){ if( object != null ){ object.endDay(); } }
	 function water(){ waterCur = waterMax; }
	 function addObject(obj : MapObject){
		  object = obj;
		  object.mapX = tileX;
		  object.mapY = tileY;
	 }
	 function removeObject(){ object = null; } //Incomplet ?
	 function useTool(tool : FarmTools){
		  if( tool < FarmTools.WateringCan ){
				if( object.destroyWithTool(tool) ){ removeObject(); }
		  }
		  else{ water(); }
	 }
}


//Classe contenant la map et accessible depuis les autres scripts
public class Map{
	 var width : int;
	 var height : int;
	 var map : MapTile[,];

	 function Map(){
		  width = 32;
		  height = 32;
	 }
	 function Map(w : int, h : int){
		  width = w;
		  height = h;
		  map = new MapTile[width,height];
		  for( var x : int = 0; x < width; ++x ){
				for( var y : int = 0; y < height; ++y ){
					 map[x,y] = MapTile();
					 map[x,y].tileX = x;
					 map[x,y].tileY = y;
				}
		  }
	 }
	 function tileAt(x : int, y : int) : MapTile{
		  if( x < 0 || y < 0 || x >= width || y >= height ){
				//Debug.Log("<color=yellow>WARNING:</color> MapInfos::tileAt -- access out of bound -- x = "+ x +" y = "+y);
				return null;
		  }
		  return map[x,y];
	 }
	 function beginDay(){
		  for( var x : int = 0; x < width; ++x ){
				for( var y : int = 0; y < height; ++y ){
					 map[x,y].beginDay();
				}
		  }
	 }
	 function endDay(){
		  for( var x : int = 0; x < width; ++x ){
				for( var y : int = 0; y < height; ++y ){
					 map[x,y].endDay();
				}
		  }
	 }
}
