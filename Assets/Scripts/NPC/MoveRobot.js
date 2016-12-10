#pragma strict

var speed : float;
var fps : float;
var vision : float;
var distanceMin : float;

var vGoal : Vector2;
var direction : Vector2;

function Start () {
	vGoal = new Vector2(0.0f, 0.0f);

	direction = Vector2(0,0);
	move();
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
	
		direction = vGoal;
	
		transform.Translate(direction * speed);
		 
	}
}
