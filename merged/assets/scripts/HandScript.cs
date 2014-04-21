using UnityEngine;
using System.Collections;

public class HandScript : MonoBehaviour {

	public Texture ImgHand;
	public Texture ImgLook, ImgTake, ImgTalk, ImgUse;
	public float left1, top1, size1;
	public float left2, top2;
	public float left3, top3;
	public float left4, top4;

	public float handleft1, handtop1, handleft2, handtop2, handsize, handratio;


	public float vx;
	public float vy;

	public GUIStyle style;

	float buttonleft, buttontop;

	bool lookEnabled=false;
	bool iconsEnabled=false;
	bool handEnabled=false;

	float delay=0;

	float screenratio;

	// Use this for initialization
	void Start () {
		buttonleft=handleft1;
		buttontop=handtop1;

		screenratio = (float)(Screen.width) / (float)(Screen.height);
		handratio = (float)ImgHand.width / (float)ImgHand.height;

		Debug.Log (Screen.width + ", " + Screen.height);


	}
	


		
	// Update is called once per frame
	void Update () {
		
		delay += Time.deltaTime;
		
		if(delay>2){
			handEnabled=true;
		}
		
		if (delay > 2.5) {
			lookEnabled=true;
			iconsEnabled=true;
		}

		if(delay>3f){
			if((buttonleft>handleft2)&&(buttontop>handtop2)){
				buttonleft+=Time.deltaTime*vx;
				buttontop+=Time.deltaTime*vy;
			}
		}
				
		if(delay>4f){
			iconsEnabled=false;
		}
	}



	void OnGUI() {


		if (lookEnabled){
			GUI.Button (new Rect (left1 * Screen.width, top1 * Screen.height, size1 * Screen.height, size1 * Screen.height), new GUIContent ("Look", ImgLook), style);
		}

		if (iconsEnabled){
			GUI.Button (new Rect (left2 * Screen.width, top2 * Screen.height, size1 * Screen.height, size1 * Screen.height), new GUIContent ("Take", ImgTake), style);
			GUI.Button (new Rect (left3 * Screen.width, top3 * Screen.height, size1 * Screen.height, size1 * Screen.height), new GUIContent ("Talk", ImgTalk), style);
			GUI.Button (new Rect (left4 * Screen.width, top4 * Screen.height, size1 * Screen.height, size1 * Screen.height), new GUIContent ("Use", ImgUse), style);
		}

		if (handEnabled){
	//do not remove this line, it is used to set the starting and the ending point of the hand's path
	//GUI.Button (new Rect (handleft1 * Screen.width, handtop1 * Screen.height, handsize * Screen.height, handsize * Screen.height), ImgHand, style);
			GUI.Button (new Rect (buttonleft * Screen.width, buttontop * Screen.height, handsize * Screen.height * handratio, handsize * Screen.height), ImgHand, style);
		}

		
	}
}
