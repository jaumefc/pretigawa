using UnityEngine;
using System.Collections;

public class triggerCamera : MonoBehaviour {

	//private GameObject trigger;
	public GameObject mainChar;
	HysteresisCamAssigned HystCamera;

	void Start () {
		//trigger = this.transform.FindChild("Trigger").gameObject;

		HystCamera = mainChar.GetComponent<HysteresisCamAssigned>();
		HystCamera.CurrCam.tag = "MainCamera";
		HystCamera.CurrCam.enabled = true;

	}

	void Update () {

	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject == mainChar)
		{
			HystCamera.NextCam = this.transform.parent.gameObject.transform.Find("Camera").camera;



			/*
			Camera cameraActual = Camera.main;
			Camera triggeredCamera = this.transform.parent.gameObject.transform.Find("Camera").camera;

			cameraActual.tag = "";
			cameraActual.enabled = false;
			triggeredCamera.tag = "MainCamera";
			triggeredCamera.transform.LookAt(mainChar.transform);
			triggeredCamera.enabled = true;

			((mouseControl)mainChar.GetComponent<mouseControl>()).cam = triggeredCamera;

			//Debug.Log(nuevaCamera.transform.parent);
			*/
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject == mainChar)
		{

			HystCamera.CurrCam.enabled = false;
			HystCamera.NextCam.enabled = true;
			HystCamera.CurrCam.tag = "Untagged";
			HystCamera.NextCam.tag = "MainCamera";
			HystCamera.CurrCam = HystCamera.NextCam;

			((mouseControl)mainChar.GetComponent<mouseControl>()).cam = HystCamera.CurrCam;



			/*
			Camera cameraActual = Camera.main;
			Camera triggeredCamera = GameObject.Find ("GeneralCamera").camera;
			
			cameraActual.tag = "";
			cameraActual.enabled = false;
			triggeredCamera.tag = "MainCamera";
			triggeredCamera.enabled = true;
			
			((mouseControl)mainChar.GetComponent<mouseControl>()).cam = triggeredCamera;
			//Debug.Log(nuevaCamera.transform.parent);
			*/
		}
	}
}
