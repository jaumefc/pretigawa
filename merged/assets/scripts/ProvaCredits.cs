using UnityEngine;
using System.Collections;

/* This script must be modified in case the camera is not placed in the origin*/

public class ProvaCredits : MonoBehaviour {

	float screenratio = 1f;
	float stylebgratio = 1f;
	float stylebgratio2 = 1f;

	public float left1, top1, size1;


	public string[] text;
	public float delay;
	public float cleft, csize;
	public float vy;
	
	float posy=-0.75f;


	public GUIStyle style;
	public GUIStyle style2;



	void Awake() {	
		// Store screen ratio
		screenratio = (float)(Screen.width) / (float)(Screen.height);

		stylebgratio=(float)(style.normal.background.width)/(float)(style.normal.background.height);
		stylebgratio2=(float)(style2.normal.background.width)/(float)(style2.normal.background.height);

		//set font size
		style.fontSize = Mathf.RoundToInt (Screen.height / 25);
		style2.fontSize = style.fontSize;
		
		style2.padding.left = (int)(Screen.height*screenratio / 35);
		style2.padding.right = (int)(Screen.height*screenratio / 35);

	}




	void OnGUI() {



		posy += (float)(Time.deltaTime * vy);
		
		for (int i=0; i<text.Length; i++) {
			GUI.Button (new Rect (cleft * Screen.width, ((csize * Screen.height) - (posy * Screen.height) +
			          (i*delay * Screen.height) ), csize * Screen.height * stylebgratio2, csize * Screen.height), text [i], style2);
		}



		if (GUI.Button (new Rect (left1 * Screen.width*stylebgratio, top1 * Screen.height, size1 * Screen.height * stylebgratio, size1 * Screen.height), "Back", style)) {
			Debug.Log ("Back");
			Application.LoadLevel("menu");
		}

		if (Input.GetKeyDown (KeyCode.Escape)) {
			Debug.Log ("Back");
			Application.LoadLevel("menu");
		}
	}


}