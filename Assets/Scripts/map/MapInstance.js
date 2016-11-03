#pragma strict

var mapW : int = 32;
var mapH : int = 32;
var map : Map;

function beginDay(){
	 map.beginDay();
}

function endDay(){
	 map.endDay();
}

function Start () {
	 map = Map(mapW, mapH);
}

function Update () {

}
