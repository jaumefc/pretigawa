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

	private GameState gs;

	public Texture2D image1, image2, image3, image4, image5, image6;



	
	void Awake() {	
		// Store screen ratio
		screenratio = (float)(Screen.width) / (float)(Screen.height);
		gs = GameState.GetInstance();
	}
	







	void OnGUI() {

		GUI.skin.button.fontSize = Mathf.RoundToInt (Screen.height / 25);
		GUI.skin.button.padding.left = (int)(Screen.height*screenratio / 40);
		GUI.skin.button.padding.right = (int)(Screen.height*screenratio / 40);
		GUI.skin.button.normal.textColor = Color.black;
		GUI.skin.button.hover.textColor = Color.black;
		GUI.skin.button.active.textColor = Color.white;


		GUI.skin.button.normal.background = (Texture2D) image1;
		GUI.skin.button.hover.background = (Texture2D) image1;
		GUI.skin.button.active.background = (Texture2D) image1;
		if (GUI.Button (new Rect (left1 * Screen.width*stylebgratio, top1 * Screen.height, size1 * Screen.height * stylebgratio, size1 * Screen.height), "New Game")) {
			Debug.Log ("New Game");
			gs.GameNew();
			Application.LoadLevel("placa");
		}

		GUI.skin.button.normal.background = (Texture2D) image2;
		GUI.skin.button.hover.background = (Texture2D) image2;
		GUI.skin.button.active.background = (Texture2D) image2;
		if (GUI.Button (new Rect (left2 * Screen.width*stylebgratio, top2 * Screen.height, size2 * Screen.height * stylebgratio, size2 * Screen.height), "Load/Save\nGame")) {
			gs.GameLoad();
			Application.LoadLevel(gs.GetInt("scene"));
			Debug.Log ("Load/Save Game");
		}

		GUI.skin.button.normal.background = (Texture2D) image3;
		GUI.skin.button.hover.background = (Texture2D) image3;
		GUI.skin.button.active.background = (Texture2D) image3;
		if (GUI.Button (new Rect (left3 * Screen.width*stylebgratio, top3 * Screen.height, size3 * Screen.height * stylebgratio, size3 * Screen.height), "Settings")) {
			Debug.Log ("Settings");
		}

		GUI.skin.button.normal.background = (Texture2D) image4;
		GUI.skin.button.hover.background = (Texture2D) image4;
		GUI.skin.button.active.background = (Texture2D) image4;
		if (GUI.Button (new Rect (left4 * Screen.width*stylebgratio, top4 * Screen.height, size4 * Screen.height * stylebgratio, size4 * Screen.height), "Credits")) {
			Debug.Log ("Credits");
			Application.LoadLevel("credits");
		}

		GUI.skin.button.normal.background = (Texture2D) image5;
		GUI.skin.button.hover.background = (Texture2D) image5;
		GUI.skin.button.active.background = (Texture2D) image5;
		if (GUI.Button (new Rect (left5 * Screen.width*stylebgratio, top5 * Screen.height, size5 * Screen.height * stylebgratio, size5 * Screen.height), "Help")) {
			Debug.Log ("Help");
			Application.LoadLevel("help");
		}

		GUI.skin.button.normal.background = (Texture2D) image6;
		GUI.skin.button.hover.background = (Texture2D) image6;
		GUI.skin.button.active.background = (Texture2D) image6;
		if (GUI.Button (new Rect (left6 * Screen.width*stylebgratio, top6 * Screen.height, size6 * Screen.height * stylebgratio, size6 * Screen.height), "Exit")) {
			Debug.Log ("Exit");
			Application.Quit();
		}	




		if (Input.GetKeyDown (KeyCode.Escape)) 
			Application.Quit();
		
	}






}