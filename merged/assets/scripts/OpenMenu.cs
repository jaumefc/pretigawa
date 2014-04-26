using UnityEngine;
using System.Collections;

/* This script must be modified in case the camera is not placed in the origin)*/


public class OpenMenu : MonoBehaviour {

	float screenratio = 1f;
	float stylebgratio = 1f;


	public float left1, top1, size1;
	public float left2, top2, size2;
	public float left3, top3, size3;
	public float left4, top4, size4;
	public float left5, top5, size5;
	public float left6, top6, size6;

	public GUIStyle style;
	private GameState gs;



	
	void Awake() {	
		// Store screen ratio
		screenratio = (float)(Screen.width) / (float)(Screen.height);

		stylebgratio=(float)(style.normal.background.width)/(float)(style.normal.background.height);

		//set font size
		style.fontSize = Mathf.RoundToInt (Screen.height / 25);
		
		style.padding.left = (int)(Screen.height*screenratio / 40);
		style.padding.right = (int)(Screen.height*screenratio / 40);
		gs = GameState.GetInstance();
	}
	







	void OnGUI() {


		if (GUI.Button (new Rect (left1 * Screen.width*stylebgratio, top1 * Screen.height, size1 * Screen.height * stylebgratio, size1 * Screen.height), "New Game", style)) {
			Debug.Log ("New Game");
			gs.GameNew();
			Application.LoadLevel("placa");
		}

		if (GUI.Button (new Rect (left2 * Screen.width*stylebgratio, top2 * Screen.height, size2 * Screen.height * stylebgratio, size2 * Screen.height), "Load/Save Game", style)) {
			gs.GameLoad();
			Application.LoadLevel(gs.GetInt("scene"));
			Debug.Log ("Load/Save Game");
		}
		
		if (GUI.Button (new Rect (left3 * Screen.width*stylebgratio, top3 * Screen.height, size3 * Screen.height * stylebgratio, size3 * Screen.height), "Settings", style)) {
			Debug.Log ("Settings");
		}
		
		if (GUI.Button (new Rect (left4 * Screen.width*stylebgratio, top4 * Screen.height, size4 * Screen.height * stylebgratio, size4 * Screen.height), "Credits", style)) {
			Debug.Log ("Credits");
			Application.LoadLevel("credits");
		}

		if (GUI.Button (new Rect (left5 * Screen.width*stylebgratio, top5 * Screen.height, size5 * Screen.height * stylebgratio, size5 * Screen.height), "Help", style)) {
			Debug.Log ("Help");
			Application.LoadLevel("help");
		}


		if (GUI.Button (new Rect (left6 * Screen.width*stylebgratio, top6 * Screen.height, size6 * Screen.height * stylebgratio, size6 * Screen.height), "Exit", style)) {
			Debug.Log ("Exit");
			Application.Quit();
		}	




		if (Input.GetKeyDown (KeyCode.Escape)) 
			Application.Quit();
		
	}






}