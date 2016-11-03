#pragma strict

var secondSinceBegin : float;
var timer : UI.Text;
var canvasObj : GameObject;
var child : Transform;
var sun : Light;


var secondGamedByRealSecond : int;
var heure : int;
var minute : int;
var jour : int;
var mois : String;
var saison : int;
var annee : int;
var rangeSecond : int;
var sunInitialIntensity : float;

var sunPointIntensity = [ 0f ];
var sunPointTime = [ 0f ];

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
	sunInitialIntensity = sun.intensity;
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

	// On fixe un minimum, en dessous de cette valeur la gestion de la lumière peut bugger.
	if (secondGamedByRealSecond < 60) {
		secondGamedByRealSecond = 60;
	}

	tick();
}

function tick () {
	while (true) {
		yield WaitForSeconds(0.5);

		++rangeSecond;
		
		var sec : int;
		sec = (rangeSecond / 2) * secondGamedByRealSecond; 
		heure = (sec / 3600) % 24;
		minute = ((sec / 300) * 5 ) % 60; // Toutes les 5 minutes
		jour = (sec / (3600 * 24)) % 3;
		mois = month[(sec / (3600 * 24 * 3)) % 12];
		annee = (sec / (3600 * 24 * 3 * 12));
		saison = (sec / (3600 * 24 * 3)) % 12 / 4;

		if (isMidnight()) {
			selectSunCurve();
		}
		timer.text = heure + "H" + minute + " " + mois + " : " + jour + " annee " + annee;

		updateSun();
	}
}

function isMidnight() {
	return ((heure == 0) && (minute == 0));
}

function selectSunCurve() {
	// Pour une journée normale
	sunPointTime = [0.16, 0.25, 0.35, 0.5, 0.58, 0.75, 0.83, 1];
	sunPointIntensity = [0.2, 0.25, 0.5, 0.9, 1, 0.6, 0.2, 0.1];

	/*
		// On peut adapter à la saison 
		switch (saison) {
			case 0 :
				// gestion effet météo
			break;

			case 1 :
				// gestion effet météo
			break;
			
			case 2 :
				// gestion effet météo
			break;

			case 3 :
				// gestion effet météo
			break;
		}
	*/
}

function updateSun() {
	var u : float;

	u = (heure * 60 + minute) / (24.0 * 60); // on actualise toutes les minutes
	sun.intensity = sunInitialIntensity * intensityMultiplier(u);
}


/**
* Le principe de l'algo est le suivant
* On suppose l'existance d'une courbe [0;1] -> [0;1]
* Avec en u l'avancement de la journée ( u = 0 -> minuit, u = 0.5 -> midi)
* Comme on est pas dans un espace continu, on prend un certain nombre de point de référence à un temps sunPointTime[i] d'intensité subPointIntensity[i]
* Il suffit alors de trouver dans quel intervalle se trouve notre point, et faire une interpolation linéaire pour en déduire l'intensité lumineuse.
*/
function intensityMultiplier(u : float) {
	// On recherche notre point sur la courbe
	var i = 0;
	while ( u > sunPointTime[i]) {
		++i;
	}

	if (i == 0) {
		return sunPointIntensity[i];
	} else {
		var previous = i - 1; 

		// Interpolation et renvoi de la bonne intensité
		return sunPointIntensity[previous] + (sunPointIntensity[i] - sunPointIntensity[previous]) * (( u - sunPointTime[previous]) / ( sunPointTime[i] - sunPointTime[previous]));
	}
}




function Update () { }