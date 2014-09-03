using UnityEngine;
using System.Collections;

public class absorcioEffect : MonoBehaviour {

	private Vector3 startingPosition;
	private pathController pathcontroller;
	private GameObject conoObj;

	public ParticleSystem effecteCobertura;
	public ParticleSystem effecteBrillo;
	public ParticleSystem effecteCono;
	public ParticleSystem explosioFase2;
	public Rigidbody thisRigidBody;

	void Start () {
		pathcontroller = GetComponent<pathController> ();
		conoObj = effecteCono.gameObject;
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
		effecteCono.Play ();
		Invoke ("faseDos", 3.5f);
	}

	private void faseDos(){
		thisRigidBody.angularVelocity = Vector3.zero;
		effecteCobertura.Stop ();
		effecteBrillo.Stop ();
		explosioFase2.Play ();
		Invoke ("gopath", 1);
		Invoke ("resetEffect", 2.6f);
	}

	private void gopath(){
		pathcontroller.goPath = true;
	}

	private void resetEffect(){
		pathcontroller.goPath = false;
		pathcontroller.reset ();
		effecteCobertura.Stop ();
		effecteBrillo.Stop ();
		effecteCono.Stop ();
		explosioFase2.Stop ();
		thisRigidBody.velocity = Vector3.zero;
		thisRigidBody.angularVelocity = Vector3.zero;
		conoObj.rigidbody.angularVelocity = Vector3.zero;
		transform.position = startingPosition;
	}

}


