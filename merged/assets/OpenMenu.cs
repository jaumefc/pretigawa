using UnityEngine;
using System.Collections;

/* This script must be modified in case the camera is not placed in the origin)*/



public class OpenMenu : MonoBehaviour {

	float ratio = 1f;
	float previousScreenWidth = 1f;
	float previousScreenHeight = 1f;
	
	bool updatePosition = true;
	bool updateSize = true;
	float size = 1f;
	float x = 0.5f;
	float y = 0.5f;

	public float left, top, right, bottom;
	public float left2, top2, right2, bottom2;
	public float left3, top3, right3, bottom3;
	public float left4, top4, right4, bottom4;
	public float left5, top5, right5, bottom5;


	public GUIStyle style;




	
	// Use this for initialization
	void Start () {
		//render init
		InvokeRepeating("Adjust", 0f, 0.5f);		
	}
	
	
	void Awake() {	
		if(guiTexture) {
			// Store texture ratio
			ratio = guiTexture.pixelInset.width / guiTexture.pixelInset.height;
		}
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
				width = height * ratio;
			}
			
			if (updatePosition) {
				
				left = Screen.width * x - width / 2f;
				top = Screen.height * y - height / 2f;
			}
			guiTexture.pixelInset = new Rect (left, top, width, height);
		}
	}








	void OnGUI() {


		if (GUI.Button (new Rect (left * Screen.width, top * Screen.height, right * Screen.width, bottom * Screen.height), "New Game", style)) {
			Debug.Log ("New Game");
			Application.LoadLevel("placa");
		}

		if (GUI.Button (new Rect (left2 * Screen.width, top2 * Screen.height, right2 * Screen.width, bottom2 * Screen.height), "Load/Save Game", style)) {
			Debug.Log ("Load/Save Game");
		}
		
		if (GUI.Button (new Rect (left3 * Screen.width, top3 * Screen.height, right3 * Screen.width, bottom3 * Screen.height), "Settings", style)) {
			Debug.Log ("Settings");
		}
		
		if (GUI.Button (new Rect (left4 * Screen.width, top4 * Screen.height, right4 * Screen.width, bottom4 * Screen.height), "Extras", style)) {
			Debug.Log ("Extras");
			Application.LoadLevel("prova credits");
		}
		
		if (GUI.Button (new Rect (left5 * Screen.width, top5 * Screen.height, right5 * Screen.width, bottom5 * Screen.height), "Exit", style)) {
			Debug.Log ("Exit");
			Application.Quit();
		}	

		
		if (Input.GetKeyDown (KeyCode.Escape)) 
			Application.Quit();
		
	}





}