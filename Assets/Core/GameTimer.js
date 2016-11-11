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
var rangeTime : int;
var sunInitialIntensity : float;
var numMois : int;

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

function dormir () {
	// Le joueur se lève à 7h

	// 1) On calcule la différence d'heure entre l'heure actuelle et l'heure à laquelle se lever

	if (minute < 60) {
		rangeTime += (60 - minute) * 60;
		minute = 0;
		++heure;
	}

	if (heure > 7) {
		rangeTime += (24 - heure) * 3600 + 7 * 3600;
		selectSunCurve();
	} else {
		rangeTime +=  (7 - heure) * 3600;
	}

}

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
	rangeTime = 0;
	numMois = 0;

	selectSunCurve();

	// On fixe un minimum, en dessous de cette valeur la gestion de la lumière peut bugger.
	if (secondGamedByRealSecond < 60) {
		secondGamedByRealSecond = 60;
	}

	tick();
}

function tick () {
	while (true) {
		yield WaitForSeconds(0.5);


		if (Input.GetAxis("Horizontal")) {
			dormir();
		}

		rangeTime += (secondGamedByRealSecond / 2);
		
		heure = (rangeTime / 3600) % 24;
		minute = ((rangeTime / 300) * 5 ) % 60; // Toutes les 5 minutes
		jour = (rangeTime / (3600 * 24)) % 3;
		numMois = (rangeTime / (3600 * 24 * 3)) % 12;
		mois = month[numMois];
		annee = (rangeTime / (3600 * 24 * 3 * 12));
		saison = (rangeTime / (3600 * 24 * 3)) % 12 / 4;

		if (isMidnight()) {
			selectSunCurve();
		}
		
		timer.text = " " + heure + "H" + minute + " "  + mois  + " : " + jour + " annee " + annee;

		updateSun();
		
	}
}

function isMidnight() {
	return ((heure == 0) && (minute == 0));
}

function selectSunCurve() {
	// Pour une journée normale
	switch (numMois) {
		case 0 :
			sunPointTime = [0.3375, 0.4, 0.45, 0.5, 0.58, 0.75, 0.83, 1];
			sunPointIntensity = [0.2, 0.5, 0.6, 0.86, 0.98, 0.6, 0.2, 0.1];
		break;

		case 1 :
			sunPointTime = [0.317, 0.4, 0.45, 0.5, 0.58, 0.75, 0.83, 1];
			sunPointIntensity = [0.15, 0.4, 0.48, 0.86, 0.98, 0.6, 0.2, 0.1];
		break;

		case 2 :
			sunPointTime = [0.291, 0.4, 0.45, 0.5, 0.58, 0.75, 0.83, 1];
			sunPointIntensity = [0.15, 0.4, 0.48, 0.86, 0.98, 0.6, 0.2, 0.1];
		break;

		case 3 :
			sunPointTime = [0.25, 0.4, 0.45, 0.5, 0.58, 0.75, 0.83, 1];
			sunPointIntensity = [0.15, 0.4, 0.48, 0.86, 0.98, 0.6, 0.2, 0.1];
		break;

		case 4 :
			sunPointTime = [0.208, 0.4, 0.45, 0.5, 0.58, 0.75, 0.83, 1];
			sunPointIntensity = [0.15, 0.4, 0.48, 0.86, 0.98, 0.6, 0.2, 0.1];
		break;

		case 5 :
			sunPointTime = [0.18, 0.4, 0.45, 0.5, 0.58, 0.75, 0.83, 1];
			sunPointIntensity = [0.15, 0.4, 0.48, 0.86, 0.98, 0.6, 0.2, 0.1];
		break;

		case 6 :
			sunPointTime = [0.18, 0.4, 0.45, 0.5, 0.58, 0.75, 0.83, 1];
			sunPointIntensity = [0.15, 0.4, 0.48, 0.86, 0.98, 0.6, 0.2, 0.1];
		break;

		case 7 :
			sunPointTime = [0.197, 0.4, 0.45, 0.5, 0.58, 0.75, 0.83, 1];
			sunPointIntensity = [0.15, 0.4, 0.48, 0.86, 0.98, 0.6, 0.2, 0.1];
		break;

		case 8 :
			sunPointTime = [0.236, 0.4, 0.45, 0.5, 0.58, 0.75, 0.83, 1];
			sunPointIntensity = [0.15, 0.4, 0.48, 0.86, 0.98, 0.6, 0.2, 0.1];
		break;

		case 9 :
			sunPointTime = [0.277, 0.4, 0.45, 0.5, 0.58, 0.75, 0.83, 1];
			sunPointIntensity = [0.15, 0.4, 0.48, 0.86, 0.98, 0.6, 0.2, 0.1];
		break;

		case 10 :
			sunPointTime = [0.298, 0.4, 0.45, 0.5, 0.58, 0.75, 0.83, 1];
			sunPointIntensity = [0.15, 0.4, 0.48, 0.86, 0.98, 0.6, 0.2, 0.1];
		break;

		default:
			sunPointTime = [0.323, 0.4, 0.45, 0.5, 0.58, 0.75, 0.83, 1];
			sunPointIntensity = [0.15, 0.4, 0.48, 0.86, 0.98, 0.6, 0.2, 0.1];
		break;
	} 

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
	
	while ( (u > sunPointTime[i]) ) {
		++i;
	}

	if (i == 0) {
		return sunPointIntensity[i];
	} else {
		var previous = i - 1; 

		// Interpolation et renvoi de la bonne intensité
		return sunPointIntensity[previous] + (sunPointIntensity[i] - sunPointIntensity[previous]) * (( u - sunPointTime[previous]) / ( sunPointTime[i] - sunPointTime[previous]));
	}
	
	//return 1.0;
}




function Update () { 
}