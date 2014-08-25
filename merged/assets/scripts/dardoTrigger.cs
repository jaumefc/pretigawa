using UnityEngine;
using System.Collections;

public class dardoTrigger : MonoBehaviour {

	public GameObject explosioGlobus;

	private GameObject hasCollidedWith;

	private GameObject SoundContainer;

	void Start () {
		SoundContainer = GameObject.Find ("BalloonSound");
	}
	

	void Update () {
	}

	void OnTriggerEnter(Collider other){
		hasCollidedWith = other.gameObject;

		if (other.gameObject.name.StartsWith ("globus")) {
			GameObject explosio = Instantiate (explosioGlobus, other.gameObject.transform.position, other.gameObject.transform.rotation) as GameObject;
			SoundContainer.audio.Play();
			Destroy (hasCollidedWith);
		} else if(hasCollidedWith.name != "SniperGameplay" && hasCollidedWith.name != "dardoCollider"){
			gameObject.rigidbody.velocity = Vector3.zero;
			gameObject.rigidbody.angularVelocity = Vector3.zero;
		}
	}

}
