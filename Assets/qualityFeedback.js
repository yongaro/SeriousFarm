#pragma strict

var good : GameObject;
var medium : GameObject;
var bad : GameObject;

function clear() {
	Debug.Log("clear");
	good.SetActive(false);
	if (good.GetComponent.<ParticleSystem>().isPlaying) {
		good.GetComponent.<ParticleSystem>().Stop();	
	}

	medium.SetActive(false);
	if (medium.GetComponent.<ParticleSystem>().isPlaying) {
		medium.GetComponent.<ParticleSystem>().Stop();	
	}

	bad.SetActive(false);
	if (bad.GetComponent.<ParticleSystem>().isPlaying) {
		bad.GetComponent.<ParticleSystem>().Stop();	
	}
}

function Start () {
	clear();

		good.GetComponent.<ParticleSystem>().Stop();	

		medium.GetComponent.<ParticleSystem>().Stop();	

		bad.GetComponent.<ParticleSystem>().Stop();	
}

function Update () {
}

function showGood() {
	clear();
	good.SetActive(true);
	if (!good.GetComponent.<ParticleSystem>().isPlaying) {
		good.GetComponent.<ParticleSystem>().Play();
	}

	prepareToClear();
}

function showMedium() {
	clear();
	medium.SetActive(true);
	if (!medium.GetComponent.<ParticleSystem>().isPlaying) {
		medium.GetComponent.<ParticleSystem>().Play();
	}

	prepareToClear();
}

function showBad() {
	clear();
	bad.SetActive(true);
	if (!bad.GetComponent.<ParticleSystem>().isPlaying) {
		bad.GetComponent.<ParticleSystem>().Play();
	}
	prepareToClear();
}

function prepareToClear() {
	yield WaitForSeconds(0.2);
	clear();
}