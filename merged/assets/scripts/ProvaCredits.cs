using UnityEngine;
using System.Collections;

/* This script must be modified in case the camera is not placed in the origin)*/









public class ProvaCredits : MonoBehaviour {

	float ratio = 1f;
	float previousScreenWidth = 1f;
	float previousScreenHeight = 1f;
	
	bool updatePosition = true;
	bool updateSize = true;
	float size = 1f;
	float x = 0.5f;
	float y = 0.5f;

	public float left, top, right, bottom;


	public string[] text;
	public float delay;
	public float cleft, cwidth, cheight, cbottom;
	public float vy;
	
	float posy=0;




	public GUIStyle style;
	public GUIStyle style2;




	
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
		style2.fontSize = style.fontSize;

		style2.padding.left = Screen.width / 30;
		style2.padding.right = Screen.width / 30;

		
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



		posy += (float)(Time.deltaTime * vy);
		
		for (int i=0; i<text.Length; i++) {
			GUI.Button (new Rect (cleft * Screen.width, ((cbottom * Screen.height) - (posy * Screen.height) +
			              (i*delay * Screen.height) ), cwidth * Screen.width, cheight * Screen.height), text [i], style2);
		}
















		if (GUI.Button (new Rect (left * Screen.width, top * Screen.height, right * Screen.width, bottom * Screen.height), "Exit", style)) {
			Debug.Log ("Back");
			Application.LoadLevel("prova menu");
		}


		if (Input.GetKeyDown (KeyCode.Escape)) {
			Debug.Log ("Exit");
			Application.LoadLevel("prova menu");
		}
	}





}