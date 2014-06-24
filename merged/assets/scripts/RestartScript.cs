using UnityEngine;
using System.Collections;

public class RestartScript : MonoBehaviour {


	float screenratio = 1f;
	float stylebgratio = 1f;
	
	public float left1, top1, size1;
	public float left2, top2, size2;


	public GUIStyle style;
	public GUIStyle style2;




	void Awake() {	
		// Store screen ratio
		screenratio = (float)(Screen.width) / (float)(Screen.height);
		stylebgratio=(float)(style.normal.background.width)/(float)(style.normal.background.height);

		//set font size
		style.fontSize = Mathf.RoundToInt (Screen.height / 25);
		style2.fontSize = Mathf.RoundToInt (Screen.height / 15);
		
		style2.padding.left = (int)(Screen.height*screenratio / 25);
		style2.padding.right = (int)(Screen.height*screenratio / 25);
	}


	void OnGUI() {
		
		
		GUI.Button (new Rect (left1 * Screen.width * stylebgratio, top1 * Screen.height, size1 * Screen.height * stylebgratio, size1 * Screen.height),
		            "Next time, try to be more careful...", style2);
				

		if (GUI.Button (new Rect (left2 * Screen.width * stylebgratio, top2 * Screen.height, size2 * Screen.height * stylebgratio, size2 * Screen.height), "Back to Game", style)) {
			Debug.Log ("RestartScene");
			Application.LoadLevel ("placa");
		}
		
		
		if (Input.GetKeyDown (KeyCode.Escape)){
			Application.LoadLevel("menu");
		}
		
	}
	
}
