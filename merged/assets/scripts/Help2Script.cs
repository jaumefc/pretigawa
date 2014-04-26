using UnityEngine;
using System.Collections;

/* This script must be modified in case the camera is not placed in the origin)*/



public class Help2Script : MonoBehaviour {

	float screenratio = 1f;
	float stylebgratio = 1f;
	float previousScreenWidth = 1f;
	float previousScreenHeight = 1f;
	
	bool updatePosition = true;
	bool updateSize = true;
	float size = 1f;
	float x = 0.5f;
	float y = 0.5f;



	public float left1, top1, size1;
	public float left2, top2;
	public float left3, top3;
	public float left4, top4;
	public float left5, top5;
	public float left6, top6;
	public float left7, top7, size7;

	public Texture ImgAbsorb, ImgTake, ImgHide, ImgLook, ImgTalk, ImgSteal;
	
	public GUIStyle style;
	public GUIStyle style3;



	
	// Use this for initialization
	void Start () {
		//render init
//		InvokeRepeating("Adjust", 0f, 0.5f);
		Adjust ();
	}
	
	
	void Awake() {	
		// Store screen ratio
		screenratio = (float)(Screen.width) / (float)(Screen.height);

		stylebgratio=(float)(style.normal.background.width)/(float)(style.normal.background.height);
		style3.fontSize = Screen.height / 20;
	}
	

	
	// Set new size and offset
	void Adjust() {
		
		// Return if screen size did not change
		if(previousScreenWidth == Screen.width && previousScreenHeight == Screen.height) {
			
			return;	
		}
		
		// Store previous screen dimensions
		previousScreenWidth = Screen.width;
		previousScreenHeight = Screen.height;


		//set font size
		style.fontSize = Mathf.RoundToInt (Screen.height / 25);



		//set texture size
		if (guiTexture) {
			
			// Set size and position
			float left = guiTexture.pixelInset.x;
			float top = guiTexture.pixelInset.y;
			float width = guiTexture.pixelInset.width;
			float height = guiTexture.pixelInset.height;
			
			if (updateSize && size > 0f) {
				
				height = Screen.height * size;
				width = height * screenratio;
			}
			
			if (updatePosition) {
				
				left = Screen.width * x - width / 2f;
				top = Screen.height * y - height / 2f;
			}
			guiTexture.pixelInset = new Rect (left, top, width, height);
		}
	}






	void OnGUI() {


		/*	if (GUI.Button (new Rect (left * Screen.width, top * Screen.height, right * Screen.width, size * Screen.height), "New Game", style)) {
			Debug.Log ("New Game");
			Application.LoadLevel("placa");
		}*/



			GUI.Button (new Rect (left1 * Screen.width * stylebgratio, top1 * Screen.height, size1 * Screen.height * stylebgratio, size1 * Screen.height), new GUIContent ("Absorb", ImgAbsorb), style3);
			GUI.Button (new Rect (left2 * Screen.width * stylebgratio, top2 * Screen.height, size1 * Screen.height * stylebgratio, size1 * Screen.height), new GUIContent ("Take", ImgTake), style3);
			GUI.Button (new Rect (left3 * Screen.width * stylebgratio, top3 * Screen.height, size1 * Screen.height * stylebgratio, size1 * Screen.height), new GUIContent ("Hide", ImgHide), style3);
			GUI.Button (new Rect (left4 * Screen.width * stylebgratio, top4 * Screen.height, size1 * Screen.height * stylebgratio, size1 * Screen.height), new GUIContent ("Look", ImgLook), style3);
			GUI.Button (new Rect (left5 * Screen.width * stylebgratio, top5 * Screen.height, size1 * Screen.height * stylebgratio, size1 * Screen.height), new GUIContent ("Talk", ImgTalk), style3);
			GUI.Button (new Rect (left6 * Screen.width * stylebgratio, top6 * Screen.height, size1 * Screen.height * stylebgratio, size1 * Screen.height), new GUIContent ("Steal", ImgSteal), style3);


			


		if (GUI.Button (new Rect (left7 * Screen.width * stylebgratio, top7 * Screen.height, size7 * Screen.height * stylebgratio, size7 * Screen.height), "Menu", style)) {
			Debug.Log ("menu");
			Application.LoadLevel ("menu");
		}





		if (Input.GetKeyDown (KeyCode.Escape)) 
			Application.LoadLevel("menu");
		
	}






}