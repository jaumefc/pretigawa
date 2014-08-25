using UnityEngine;
using System.Collections;

public class absorcioEffect : MonoBehaviour {

	private Vector3 startingPosition;

	public GameObject effecteAbsorcio;
	public Rigidbody thisRigidBody;

	void Start () {
	}

	void Update () {
	}

	public void startAbsorcio(){
		startingPosition = transform.position;
		Invoke ("resetEffect", 3);
	}

	private void resetEffect(){
		transform.position = startingPosition;
		thisRigidBody.velocity = Vector3.zero;
		thisRigidBody.angularVelocity = Vector3.zero;
	}
}

