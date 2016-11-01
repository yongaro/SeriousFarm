#pragma strict

var secondSinceBegin : float;
var timer : UI.Text;
var canvasObj : GameObject;
var child : Transform;

var secondGamedByRealSecond : int;
var heure : int;
var minute : int;
var jour : int;
var mois : String;
var saison : int;
var annee : int;
var rangeSecond : int;


var month = [
	"Janvier",
	"Février",
	"Mars",
	"Avril",
	"Mai",
	"Juin",
	"Juillet",
	"Août",
	"Septembre",
	"Octobre",
	"Novembre",
	"Décembre"
];

function Start () {
	secondSinceBegin = 0;
	canvasObj = GameObject.Find("Canvas");
	child = canvasObj.transform.Find("TimerText");
	timer = child.GetComponent(UI.Text);

	heure = 0;
	minute = 0;
	jour = 0;
	mois = month[0];
	saison = 0;
	annee = 0;
	rangeSecond = 0;

	tick();
}

function tick () {
	while (true) {
		yield WaitForSeconds(1);

		++secondSinceBegin;
		++rangeSecond;
		
		var sec : int;
		sec = rangeSecond * secondGamedByRealSecond; 
		heure = (sec / 3600) % 24;
		minute = ((sec / 300) * 5 ) % 60; // Toutes les 5 minutes
		jour = (sec / (3600 * 24)) % 3;
		mois = month[(sec / (3600 * 24 * 3)) % 12];
		annee = (sec / (3600 * 24 * 3 * 12));
		saison = (sec / (3600 * 24 * 3)) % 12 / 4;

		timer.text = heure + "H" + minute + " " + mois + " : " + jour + " annee " + annee;
	}
}

function Update () { }