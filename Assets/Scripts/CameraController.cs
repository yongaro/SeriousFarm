using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour{
    public GameObject player;
    private Vector3 offset;
	public GameObject rainParticles;
	public GameObject snowParticles;
	private Vector3 particlesOffset;
	

    void Start(){
        offset = transform.position - player.transform.position;
		rainParticles = GameObject.Find("Pluie");
		snowParticles = GameObject.Find("Neige2");
		particlesOffset = new Vector3( 5.0f, 5.0f, 0.0f);
    }

    void Update(){
        transform.position = player.transform.position + offset;
		if( rainParticles != null ){ rainParticles.transform.position =  transform.position + particlesOffset; }
		if( snowParticles != null ){ snowParticles.transform.position =  transform.position + particlesOffset; }
    }
}
