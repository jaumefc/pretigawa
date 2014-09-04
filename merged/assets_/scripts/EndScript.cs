using UnityEngine;
using System.Collections;

public class EndScript : MonoBehaviour {

	float screenratio = 1f;
	float stylebgratio = 1f;

	public float left5, top5, size5;
	public float left6, top6, size6;
	public float posx, posy, size;
	public float vx, vy;


	public Texture ImgElement;
	
	public GUIStyle style;
	public GUIStyle style2;
	public GUIStyle style3;

	

	
	void Awake() {	

		screenratio = (float)(Screen.width) / (float)(Screen.height);
		stylebgratio=(float)(style.normal.background.width)/(float)(style.normal.background.height);
		//set font size
		style.fontSize = Mathf.RoundToInt (Screen.height / 25);
		style2.fontSize = Mathf.RoundToInt (Screen.height / 15);
		
		style2.padding.left = (int)(Screen.height*screenratio / 25);
		style2.padding.right = (int)(Screen.height*screenratio / 25);
	}



	




	void OnGUI() {

		posx += (float)(Time.deltaTime * vx - vx);
		posy += (float)(Time.deltaTime * vy);

				
		GUI.Button (new Rect(posx*Screen.height*screenratio,posy*Screen.height,size*Screen.height,2*size*Screen.height), ImgElement, style3);

		GUI.Button (new Rect (left5 * Screen.width * stylebgratio, top5 * Screen.height, size5 * Screen.height * stylebgratio, size5 * Screen.height),
	            "Have a safe flight!", style2);

		if (GUI.Button (new Rect (left6 * Screen.width * stylebgratio, top6 * Screen.height, size6 * Screen.height * stylebgratio, size6 * Screen.height), "Menu", style)) {
			Debug.Log ("Menu");
			Application.LoadLevel ("menu");
		}
		
		if (Input.GetKeyDown (KeyCode.Escape)){
			Application.LoadLevel("menu");
		}
		
	}

}
