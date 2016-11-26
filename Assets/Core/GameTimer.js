#pragma strict

var secondSinceBegin : float;
var timer : UI.Text;
var canvasObj : GameObject;
var child : Transform;
var sun : Light;
var modeCouleur : boolean;


var rend : Renderer;
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

var pluie : GameObject;
var neige : GameObject;

var rand : float;

var sunPointIntensity = [ 0f ];
var sunPointTime = [ 0f ];
var mapManager : GameObject;

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
	mapManager = GameObject.Find("MapManager");
	
	if (heure < 0) {
		heure = 0;
	} else if (heure >= 24) {
		heure = heure % 24;
	}


	if (minute < 0) {
		minute = 0;
	} else if (minute >= 60) {
		minute = minute % 60;
	}


	if (jour < 0) {
		jour = 0;
	} else if (jour >= 3) {
		jour = jour % 3;
	}

	if (numMois < 0) {
		numMois = 0;
	} else if (numMois >= 12) {
		numMois = numMois % 12;
	}

	rangeTime += minute * 60;
	rangeTime += heure * 3600;


	// nombre de secondes par jour
	rangeTime += jour * 122400;

	// nombre de secondes par mois (3 jours)
	rangeTime += numMois * 367200;



	mois = month[numMois];
	
	selectSunCurve();

	// On fixe un minimum, en dessous de cette valeur la gestion de la lumière peut bugger.
	if (secondGamedByRealSecond < 60) {
		secondGamedByRealSecond = 60;
	}

	tick();
}

function tick () {
	while (true) {
		var tmp : float;
		tmp = 300.0f / secondGamedByRealSecond;
		//Debug.Log("Temps : " + tmp);
		yield WaitForSeconds(tmp);


		var currentMonth = numMois;
		var lastDay = jour;

		rangeTime += (secondGamedByRealSecond / 2);
		heure = (rangeTime / 3600) % 24;
		minute = ((rangeTime / 300) * 5 ) % 60; // Toutes les 5 minutes
		jour = (rangeTime / (3600 * 24)) % 3;
		numMois = (rangeTime / (3600 * 24 * 3)) % 12;
		mois = month[numMois];
		annee = (rangeTime / (3600 * 24 * 3 * 12));
		saison = (rangeTime / (3600 * 24 * 3)) % 12 / 4;

		if (isMidnight() || lastDay != jour) {
			Debug.Log("Changement de jour : " + jour + " " + mois);
			selectSunCurve();
		}

		if (currentMonth  != numMois) {
			mapManager.SendMessage("setMonth", numMois);
		}
		
		timer.text = " " + heure + "H" + minute + " "  + mois  + " : " + jour + " annee " + annee;

		updateSun();
		
	}
}

function isMidnight() {
	return ((heure == 0) && (minute == 0));
}

function pleuvoir () {
	pluie.SetActive(true);
	if (!pluie.GetComponent.<ParticleSystem>().isPlaying) {
		pluie.GetComponent.<ParticleSystem>().Play();
	}

	mapManager.SendMessage("pleuvoir");
}

function neiger() {
	neige.SetActive(true);
	if (!neige.GetComponent.<ParticleSystem>().isPlaying) {
		neige.GetComponent.<ParticleSystem>().Play();
	}

	mapManager.SendMessage("neiger");
}

function clearMeteo() {
	pluie.SetActive(false);
	neige.SetActive(false);
	if (pluie.GetComponent.<ParticleSystem>().isPlaying) {
		pluie.GetComponent.<ParticleSystem>().Stop();	
	} 

	if (neige.GetComponent.<ParticleSystem>().isPlaying) {
		pluie.GetComponent.<ParticleSystem>().Stop();
	}

}


function selectSunCurve() {
    var v : Vector3;
    v = new Vector3();

    clearMeteo();

	mapManager.SendMessage("endDay");
	mapManager.SendMessage("beginDay");
	// Pour une journée normale
	switch (numMois) {
		case 0 :
			sunPointTime = [0.3375, 0.38, 0.42, 0.48, 0.58, 0.70, 0.74, 1];
			sunPointIntensity = [0.2, 0.55, 0.6, 0.8, 0.90, 0.7, 0.2, 0.1];
		break;

		case 1 :
			sunPointTime = [0.317, 0.37, 0.41, 0.47, 0.58, 0.74, 0.78, 1];
			sunPointIntensity = [0.2, 0.55, 0.6, 0.82, 0.93, 0.7, 0.2, 0.1];
		break;

		case 2 :
			sunPointTime = [0.291, 0.36, 0.39, 0.46, 0.58, 0.77, 0.81, 1];
			sunPointIntensity = [0.2, 0.55, 0.6, 0.86, 0.93, 0.6, 0.2, 0.1];
		break;

		case 3 :
			sunPointTime = [0.25, 0.31, 0.38, 0.45, 0.58, 0.79, 0.83, 1];
			sunPointIntensity = [0.2, 0.55, 0.61, 0.86, 0.935, 0.61, 0.2, 0.1];
		break;

		case 4 :
			sunPointTime =      [0.208, 0.27, 0.38, 0.46, 0.6,  0.83, 0.87, 1];
			sunPointIntensity = [0.2,   0.55,  0.62, 0.87, 0.94, 0.62,  0.2,  0.1];
		break;

		case 5 :
			sunPointTime = [     0.18,  0.25,  0.38,  0.46,   0.6,  0.85,  0.9,  1];
			sunPointIntensity = [0.2,   0.55,   0.62,  0.875,  0.96,  0.65,   0.2,   0.1];
		break;

		case 6 :
			sunPointTime =      [0.18,  0.25,  0.38, 0.46, 0.6,   0.875, 0.9, 1];
			sunPointIntensity = [0.2,  0.55,  0.62, 0.88, 0.98,  0.66,  0.22, 0.1];
		break;

		case 7 :
			sunPointTime =      [0.197, 0.28, 0.39, 0.47, 0.6,  0.85, 0.88, 1];
			sunPointIntensity = [0.2,   0.55,  0.61, 0.87, 0.96, 0.65, 0.2, 0.1];
		break;

		case 8 :
			sunPointTime =      [0.236, 0.3, 0.4, 0.48, 0.59, 0.8125, 0.83, 1];
			sunPointIntensity = [0.2,   0.55,  0.6, 0.87, 0.94, 0.63,   0.2,  0.1];
		break;

		case 9 :
			sunPointTime =      [0.277, 0.32, 0.4, 0.5,   0.58,   0.73,  0.78, 1];
			sunPointIntensity = [0.2,   0.55,  0.6,  0.86, 0.935,  0.62,  0.2, 0.1];
		break;

		case 10 :
			sunPointTime =      [0.298, 0.36, 0.41, 0.5,  0.58, 0.69, 0.74, 1];
			sunPointIntensity = [0.2,   0.55,  0.6,  0.82, 0.91, 0.6, 0.2, 0.1];
		break;

		default:
			sunPointTime =      [0.323, 0.38, 0.42, 0.5, 0.58, 0.66, 0.7, 1];
			sunPointIntensity = [0.2,  0.55,  0.46, 0.81, 0.90, 0.6, 0.2, 0.1];
		break;
	} 

	switch (saison) {

		case 0 :
			rand = Random.Range(0.0f, 1.0f);
			if (rand <= 0.40f) {
				pleuvoir();
			} else {
				rand = Random.Range(0.0f, 1.0f);
				if (rand < 0.50) {
					neiger();
				}
			}
		break;

		case 1 :
			rand = Random.Range(0.0f, 1.0f);
			if (rand <= 0.15f) {
				pleuvoir();
			}		
		break;
		
		case 2 :
			rand = Random.Range(0.0f, 1.0f);
			if (rand < 0.10) {
				pleuvoir();
			} 
		break;

		case 3 :
			var rand = Random.Range(0.0f, 1.0f);
			if (rand < 0.20) {
				pleuvoir();
			}
		break;
	}
	
}

function updateSun() {
	var u : float;
	u = (heure * 60 + minute) / (24.0 * 60); // on actualise toutes les minutes

	var tmp : float;
	tmp = sun.intensity;
	//sun.intensity = sunInitialIntensity * intensityMultiplier(u);
	sun.intensity = sunInitialIntensity * intensityMultiplier(u);
	if (modeCouleur) {
		var i : int;
		i = getFragmentOfDay(u);

		// Les courbres d'ensoleillement ont toutes 8 points
		// pour i = 2 on est au levé du soleil, pour i = 5 au couché

		//sun.intensity = sunInitialIntensity * sunPointIntensity[previous] + (sunPointIntensity[i] - sunPointIntensity[previous]) * (( u - sunPointTime[previous]) / ( sunPointTime[i] - sunPointTime[previous]));
		sun.color.r = sun.intensity;
		sun.color.g = sun.intensity;
		sun.color.b = sun.intensity;
		
		
		//sun.intensity *= 1.5;


		if (i < 2 || i >= 6) {
			sun.color.b = 1.25 - sun.intensity;
			sun.color.g /= (1.75 - sun.intensity);
			sun.color.r /= (1.75 - sun.intensity);
			if (sun.intensity < 0.6) {
				sun.intensity = 0.6;
			//	sun.intensity *= 1.8;	
			}
		} else if (i == 2) {
			sun.color.b = 1.8 - sun.intensity;
			sun.color.g /= (1.6 - sun.intensity);
			sun.color.r /= (1.6 - sun.intensity);
			sun.intensity *= 1.1;
		} /*else if (i == 4) {
			sun.color.b = 3 - sun.intensity;
			sun.color.g /= (0.8 - sun.intensity);
			sun.color.r /= (0.8 - sun.intensity);
			sun.intensity *= 1.15;
		} */else if (i == 5) {
			sun.color.b *= (sun.intensity / 1.5);
			//sun.color.g ;
			//sun.color.r ;
			//sun.intensity *= 1.1;
		}
	}
}


/**
* Le principe de l'algo est le suivant
* On suppose l'existance d'une courbe [0;1] -> [0;1]
* Avec en u l'avancement de la journée ( u = 0 -> minuit, u = 0.5 -> midi)
* Comme on est pas dans un espace continu, on prend un certain nombre de point de référence à un temps sunPointTime[i] d'intensité subPointIntensity[i]
* Il suffit alors de trouver dans quel intervalle se trouve notre point, et faire une interpolation linéaire pour en déduire l'intensité lumineuse.
*/
function getFragmentOfDay(u : float) {
	var i = 0;
	
	while ( (u > sunPointTime[i]) ) {
		++i;
	}
	return i;
}

function intensityMultiplier(u : float) {
	var i = getFragmentOfDay(u);
	
	if (i == 0) {
		return sunPointIntensity[i];
    } else {
		var previous = i - 1;
    	return sunPointIntensity[previous] + (sunPointIntensity[i] - sunPointIntensity[previous]) * (( u - sunPointTime[previous]) / ( sunPointTime[i] - sunPointTime[previous]));
    }
}

function Update () { 

}
