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

	agitation = 70;
	distanceMin = 1.5f;
	speed = 0.05;
	fps = 30;
	direction = Vector2(0,0);
	lastChange = 0;
	move();
}



function separate (target : Collider2D) {
	var dist = Vector2.Distance(transform.position, target.gameObject.transform.position);
	if ((target.gameObject.tag == "Sheep") && (Vector2.Distance(transform.position, target.gameObject.transform.position) < distanceMin)) {
		vSeparate += new Vector2((transform.position.x - target.gameObject.transform.position.x), (transform.position.y - target.gameObject.transform.position.y));
	} else if ((target.gameObject.name == "Player") && (Vector2.Distance(transform.position, target.gameObject.transform.position) < distanceMin)) {
		vSeparate += 2000 * new Vector2((transform.position.x - target.gameObject.transform.position.x), (transform.position.y - target.gameObject.transform.position.y));
	}
}

function center (target : Collider2D) {
	if (target.gameObject.tag == "Sheep") {
		vCenter -= new Vector2((transform.position.x - target.gameObject.transform.position.x), (transform.position.y - target.gameObject.transform.position.y));
	}
}

function goal (target : Collider2D) {
	if ((target.gameObject.name == "Player") && (Vector2.Distance(transform.position, target.gameObject.transform.position) >= distanceMin)) {
		vGoal -= new Vector2((transform.position.x - target.gameObject.transform.position.x), (transform.position.y - target.gameObject.transform.position.y));
	} 
}

function move() {
	while (true) {

		if (fps == 0) {
			fps = 1;
		}
		var tmp = 1.0 / fps;
		yield  WaitForSeconds (tmp);


			lastChange ++;

		if (lastChange >= 2 * fps) {
			var i=0;
			var hitColliders = Physics2D.OverlapCircleAll(Vector2(transform.position.x, transform.position.y), vision);
			var noise = Vector2(1 - Random.Range(0, 4), 1 - Random.Range(0, 4));

			lastChange = 0;
			vSeparate = new Vector2(0.0f, 0.0f);
			vCenter = new Vector2(0.0f, 0.0f);
			vGoal = new Vector2(0.0f, 0.0f);
			for (i = 0; i < hitColliders.Length; i++) {
				separate(hitColliders[i]);
				center(hitColliders[i]);
				goal(hitColliders[i]);

			}
			//var noise = Vector2(100 - Random.Range(0,200), 100 - Random.Range(0,200));
			//var noise = Vector2(0,0);
			direction = (vSeparate.normalized + 0.5 * vCenter.normalized + 100 * vGoal.normalized + noise.normalized).normalized;
			
			if ( (direction.x * direction.x) > (direction.y * direction.y)) {
				direction.y = 0;
			} else {
				direction.x = 0;	
			}
			
			antiShake();
				
		} else {
			++lastChange; 
		}

		if (Random.Range(0, 100) < agitation) {
			transform.Translate(direction * speed);
		} else {
			transform.Translate(direction * speed / 10);
		} 
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
