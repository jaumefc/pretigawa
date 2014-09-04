using UnityEngine;
using System.Collections;

public class absorcioEffect : MonoBehaviour {

	private Vector3 startingPosition;
	private pathController pathcontroller;

	public ParticleSystem effecteCobertura;
	public ParticleSystem effecteBrillo;
	public Rigidbody thisRigidBody;

	void Start () {
		pathcontroller = GetComponent<pathController> ();
	}

	void Update () {
		if ((Input.GetKey ("h"))) {
			startAbsorcio();
		}
	}

	public void startAbsorcio(){

		thisRigidBody.angularVelocity = new Vector3 (0, 1, 0);

		startingPosition = transform.position;
		effecteCobertura.Play ();
		effecteBrillo.Play ();
		Invoke ("faseDos", 3.5f);
	}

	private void faseDos(){

	}

	private void faseTres(){
		pathcontroller.goPath = true;
		effecteCobertura.Stop ();
		effecteBrillo.Stop ();
		thisRigidBody.velocity = Vector3.zero;
		thisRigidBody.angularVelocity = Vector3.zero;
	}

	private void resetEffect(){
		transform.position = startingPosition;
	}
}


