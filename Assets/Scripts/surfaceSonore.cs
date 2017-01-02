using UnityEngine;
using System.Collections;

public class surfaceSonore : MonoBehaviour {
	public TypeSol type;
	
	void Start(){}
	void Update(){}
	
	void OnTriggerEnter2D(Collider2D other){
		FM_SonScript.changerTypeSol(type);
	}
	
	void OnTriggerExit2D(Collider2D other) {
		FM_SonScript.changerTypeSol(TypeSol.Herbe);
	}
}
