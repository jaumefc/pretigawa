﻿using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	Camera cCamera1, cCamera2, cCameraDialog, cCameraOriginal;
	DialogCameraScript DCScript;
	GUITexture DCTexture;





	// Use this for initialization
	void Start () {
		cCamera1 = GameObject.Find ("Player").GetComponentInChildren<Camera> ();
		//cCamera2 = GameObject.Find ("Camera2").camera;
		cCameraDialog = GameObject.Find ("DialogCamera").camera;

		DCScript = GameObject.Find ("DialogLayout").GetComponent<DialogCameraScript>();
		DCTexture = GameObject.Find ("DialogLayout").guiTexture;
		DCTexture.enabled = false;


		cCamera1.enabled = false;
		//cCamera2.enabled = false;
		cCameraDialog.enabled = false;
		DCScript.enabled = false;
			
	}
	


	public void TransferIn(Camera otherCam) {
		Debug.Log ("Fem el In");
		Debug.Log (Camera.current);
		cCamera2 = otherCam;
		cCameraOriginal = Camera.main;
		cCameraOriginal.enabled = false;
		DCTexture.enabled = true;
		cCamera1.enabled = true;
		cCamera2.enabled = true;
		cCameraDialog.enabled = true;

		DCScript.enabled = true;
	}

	public void TransferOut() {
		Debug.Log ("Fem el out");
		Debug.Log (Camera.main);
		cCameraOriginal.enabled = true;
		DCTexture.enabled = false;
		cCamera1.camera.enabled = false;
		cCamera2.enabled = false;
		cCameraDialog.enabled = false;

		DCScript.enabled = false;
	}


}
