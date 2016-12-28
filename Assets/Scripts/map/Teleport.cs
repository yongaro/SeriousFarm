using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour {
	public GameObject target;
	public float offsetX;
	public float offsetY;
	
	// Use this for initialization
	void Start(){}
	// Update is called once per frame
	void Update(){}
	
	void OnTriggerEnter2D(Collider2D other){
		GameObject player = GameObject.Find("Player");
		GameObject robot = GameObject.Find("Robot");
		if( player != null && target != null && robot != null){
			Vector3 newPos = new Vector3();
			newPos.x = target.transform.position.x + offsetX;
			newPos.y = target.transform.position.y + offsetY;
			newPos.z = player.transform.position.z;
			
			player.transform.position = newPos;
			robot.transform.position = newPos;
		}
	}
}
