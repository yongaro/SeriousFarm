#pragma strict

var mapX : int = 0;
var mapY : int = 0;
var mapGameObjectName : String = "world";
var mapReference : MapInstance;
var tile : MapTile;

//Exemples d'utilisation de K LI T
function ajoutCailloux(){
	 var objTemp : MapObject = MapObject();
	 objTemp.collision = true;
	 tile.addObject(objTemp);
}

function useTool( tool : FarmTools ){
	 tile.useTool(tool);
}

function casserCailloux(){
	 useTool(FarmTools.Pickaxe);
}


function Start () {
	 mapReference = GameObject.Find(mapGameObjectName).GetComponent(MapInstance);
	 tile = mapReference.map.tileAt(mapX,mapY);
}

function Update () {

}
