using UnityEngine;
using System.Collections;

public class triggerCamera : MonoBehaviour {

	//private GameObject trigger;
	public GameObject mainChar;

	void Start () {
		//trigger = this.transform.FindChild("Trigger").gameObject;
	}

	void Update () {

	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject == mainChar)
		{
			Camera cameraActual = Camera.main;
			Camera triggeredCamera = this.transform.parent.gameObject.transform.Find("Camera").camera;

			cameraActual.tag = "";
			cameraActual.enabled = false;
			triggeredCamera.tag = "MainCamera";
			triggeredCamera.transform.LookAt(mainChar.transform);
			triggeredCamera.enabled = true;

			((mouseControl)mainChar.GetComponent<mouseControl>()).cam = triggeredCamera;

			//Debug.Log(nuevaCamera.transform.parent);
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject == mainChar)
		{
			Camera cameraActual = Camera.main;
			Camera triggeredCamera = GameObject.Find ("GeneralCamera").camera;
			
			cameraActual.tag = "";
			cameraActual.enabled = false;
			triggeredCamera.tag = "MainCamera";
			triggeredCamera.enabled = true;
			
			((mouseControl)mainChar.GetComponent<mouseControl>()).cam = triggeredCamera;
			//Debug.Log(nuevaCamera.transform.parent);
		}
	}
}
