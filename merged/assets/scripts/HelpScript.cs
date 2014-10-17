using UnityEngine;
using System.Collections;

/* This script must be modified in case the camera is not placed in the origin)*/



public class HelpScript : MonoBehaviour {

	float screenratio = 1f;
	float stylebgratio = 1f;


	public float left5, top5, size5;
	public float left6, top6, size6;

	public GUIStyle style;
	public GUIStyle style2;

	public Texture2D next;
	
		
	void Awake() {	
		// Store screen ratio
		screenratio = (float)(Screen.width) / (float)(Screen.height);

		stylebgratio=(float)(style2.normal.background.width)/(float)(style2.normal.background.height);

		//set font size
		style2.fontSize = Mathf.RoundToInt (Screen.height / 15);
		
		style2.padding.left = (int)(Screen.height*screenratio / 25);
		style2.padding.right = (int)(Screen.height*screenratio / 25);
	}
	





	void OnGUI() {


		GUI.Button (new Rect (left5 * Screen.width * stylebgratio, top5 * Screen.height, size5 * Screen.height * stylebgratio, size5 * Screen.height),
		            "Tap on an object or person and release on the action to perform.", style2);


		if (GUI.Button (new Rect (left6 * Screen.width * stylebgratio, top6 * Screen.height, size6 * Screen.height * stylebgratio, size6 * Screen.height), next, style)) {
			Debug.Log ("next");
			Application.LoadLevel ("help2");
		}


		if (Input.GetKeyDown (KeyCode.Escape)){
			Application.LoadLevel("menu");
		}

	}






}