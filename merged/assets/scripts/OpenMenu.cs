using UnityEngine;
using System.Collections;

/* This script must be modified in case the camera is not placed in the origin)*/


public class OpenMenu : MonoBehaviour {

	float screenratio = 1f;


	public float left1, top1, size1;
	public float left2, top2, size2;
	public float left3, top3, size3;
	public float left4, top4, size4;
	public float left5, top5, size5;

	private GameState gs;

	public Texture2D image1, image2, image3, image4, image5;

	float ratio1, ratio2, ratio3, ratio4, ratio5;

	public GUIStyle style;
	
	void Awake() {	
		// Store screen ratio
		screenratio = (float)(Screen.width) / (float)(Screen.height);
		ratio1 = (float)(image1.width) / (float)(image1.height);
		ratio2 = (float)(image2.width) / (float)(image2.height);
		ratio3 = (float)(image3.width) / (float)(image3.height);
		ratio4 = (float)(image4.width) / (float)(image4.height);
		ratio5 = (float)(image5.width) / (float)(image5.height);

		gs = GameState.GetInstance();
	}



	void OnGUI() {


		if (GUI.Button (new Rect (left1 * Screen.width*ratio1, top1 * Screen.height, size1 * Screen.height * ratio1, size1 * Screen.height), image1, style)) {
			Debug.Log ("New Game");
			gs.GameNew();
			Application.LoadLevel("placa_tutorial");
		}


		if (GUI.Button (new Rect (left2 * Screen.width*ratio2, top2 * Screen.height, size2 * Screen.height * ratio2, size2 * Screen.height), image2, style)) {
			gs.GameLoad();
			//modificacio build jesus

			//Application.LoadLevel(gs.GetInt("scene"));

			Application.LoadLevel("placa");
			Debug.Log ("Load/Save Game");
		}


		if (GUI.Button (new Rect (left3 * Screen.width*ratio3, top3 * Screen.height, size3 * Screen.height * ratio3, size3 * Screen.height), image3, style)) {
			Debug.Log ("Credits");
			Application.LoadLevel("credits");
		}


		if (GUI.Button (new Rect (left4 * Screen.width*ratio4, top4 * Screen.height, size4 * Screen.height * ratio4, size4 * Screen.height), image4, style)) {
			Debug.Log ("Help");
			Application.LoadLevel("help");
		}


		if (GUI.Button (new Rect (left5 * Screen.width*ratio5, top5 * Screen.height, size5 * Screen.height * ratio5, size5 * Screen.height), image5, style)) {
			Debug.Log ("Exit");
			Application.Quit();
		}	




		if (Input.GetKeyDown (KeyCode.Escape)) 
			Application.Quit();
		
	}






}