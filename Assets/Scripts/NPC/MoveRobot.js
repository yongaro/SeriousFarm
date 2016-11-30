#pragma strict

var agitation : int;
var speed : float;
var fps : float;
var vision : float;
var distanceMin : float;

var vSeparate : Vector2;
var vCenter : Vector2;
var vGoal : Vector2;
var direction : Vector2;
var lastChange : int;

function Start () {
	vSeparate = new Vector2(0.0f, 0.0f);
	vCenter = new Vector2(0.0f, 0.0f);
	vGoal = new Vector2(0.0f, 0.0f);

	//agitation = 70;
	//distanceMin = 1.5f;
	//speed = 0.05;
	//fps = 30;
	direction = Vector2(0,0);
	lastChange = 0;
	move();
}



function separate (target : Collider2D) {
	
}

function center (target : Collider2D) {
	if (target.gameObject.tag == "Sheep") {
		vCenter -= new Vector2((transform.position.x - target.gameObject.transform.position.x), (transform.position.y - target.gameObject.transform.position.y));
	}
}

function goal (target : Collider2D) {
	if ((target.gameObject.name == "Player") && (Vector2.Distance(transform.position, target.gameObject.transform.position) >= distanceMin)) {
		vGoal = new Vector2((target.gameObject.transform.position.x - transform.position.x), (target.gameObject.transform.position.y - transform.position.y));
	}  else if ((target.gameObject.name == "Player") && (Vector2.Distance(transform.position, target.gameObject.transform.position) < distanceMin)) {
		vGoal = new Vector2(0,0);
	}
}

function move() {
	while (true) {

		if (fps == 0) {
			fps = 1;
		}
		var tmp = 1.0 / fps;
		yield  WaitForSeconds (tmp);


		var i=0;
		var hitColliders = Physics2D.OverlapCircleAll(Vector2(transform.position.x, transform.position.y), vision);
		var noise = Vector2(-0.5 + Random.Range(0, 1), -0.5 + Random.Range(0, 1));

		for (i = 0; i < hitColliders.Length; i++) {
			goal(hitColliders[i]);
		}
		//var noise = Vector2(100 - Random.Range(0,200), 100 - Random.Range(0,200));
		//var noise = Vector2(0,0);
		direction = vGoal;
		/*
		if ( (direction.x * direction.x) > (direction.y * direction.y)) {
			direction.y = 0;
		} else {
			direction.x = 0;	
		}
		*/
		//antiShake();
			
	

		transform.Translate(direction * speed);
		 
	}
}

function antiShake() {
			if ((direction.x * direction.x) < 0.9) {
				direction.x = 0;
			}

			if ((direction.y * direction.y) < 0.9) {
				direction.y = 0;
			}	
}
function goAway(target : Collider2D) {
	var velocity = new Vector2((transform.position.x - target.gameObject.transform.position.x) * speed, (transform.position.y - target.gameObject.transform.position.y) * speed);
	GetComponent.<Rigidbody2D>().velocity = -velocity;

}

function repulse (target : Collider2D) {
	//Debug.Log("repulse");
	//var v = new Vector2((transform.position.x - target.gameObject.transform.position.x) * speed, (transform.position.y - target.gameObject.transform.position.y) * speed);
	//direction = v;
	//Debug.Log(v);
}

function Update () {}
