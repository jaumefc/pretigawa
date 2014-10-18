using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	Camera cCamera1, cCamera2, cCameraDialog, cCameraOriginal;
	//DialogCameraScript DCScript;
	GUITexture DCTexture;
	bool stateIn;
	mouseControl MCScript;



	// Use this for initialization
	void Start () {
		//cCamera1 = GameObject.Find ("Player").GetComponentInChildren<Camera> ();
		MCScript = GameObject.Find("Player").GetComponent<mouseControl>();
		//cCamera2 = GameObject.Find ("Camera2").camera;
		cCameraDialog = GameObject.Find ("DialogCamera").camera;
		cCameraOriginal = GameObject.FindWithTag("MainCamera").camera;//

		//DCScript = GameObject.Find ("DialogLayout").GetComponent<DialogCameraScript>();
		//DCTexture = GameObject.Find ("DialogLayout").guiTexture;
		//DCTexture.enabled = false;


		//cCamera1.enabled = false;
		//cCamera2.enabled = false;
		cCameraDialog.enabled = false;
		//DCScript.enabled = false;
		stateIn = false;
	}
	


	public void TransferIn(Camera otherCam) {
		Debug.Log ("Fem el In");
		Debug.Log (Camera.current);
		cCamera2 = otherCam;
		if(!stateIn)
		{
			cCameraOriginal = Camera.current;
		}
		foreach(Camera cam in Camera.allCameras){
			cam.enabled = false;
		}
//		cCameraOriginal = Camera.main;
//		if(cCameraOriginal!=null){
//			cCameraOriginal.enabled = false;
//		}
//		else{
//			cCameraOriginal = Camera.current;
//			cCameraOriginal.enabled = false;
//		}
		//DCTexture.enabled = true;
		//cCamera1.enabled = true;
		cCamera2.enabled = true;
		//cCameraDialog.enabled = true;
		MCScript.enabled=false;
		//DCScript.enabled = true;
		//DCScript.Init ();
		stateIn = true;
	}

	public void TransferOut() {
		Debug.Log ("Fem el out");
		Debug.Log (Camera.main);
		cCameraOriginal.enabled = true;
		//DCTexture.enabled = false;
		//cCamera1.camera.enabled = false;
		cCamera2.enabled = false;
		//cCameraDialog.enabled = false;
		MCScript.enabled=true;
		stateIn = false;
		//DCScript.enabled = false;
	}

	public bool IsIn() {
		return stateIn;
	}


}
