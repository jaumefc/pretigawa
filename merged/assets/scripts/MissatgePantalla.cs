using UnityEngine;
using System.Collections;


public class MissatgePantalla:MonoBehaviour {
	public Texture2D textura;
	public string missatge;
	private bool hasToShow = false;
	private int timeToShow = 3;

	void OnGUI () {

		if (!hasToShow)return;

		float startingPosX = Screen.width / 2 - 200;
		float startingPosY = Screen.height - 200;
		GUI.skin.label.fontSize = 32;

		if (textura != null) {
			startingPosX = Screen.width / 2 - textura.width / 2;
			startingPosY = Screen.height - textura.height;
			GUI.Label (new Rect (startingPosX, startingPosY, textura.width, textura.height), textura);
		}

		GUI.Label (new Rect (startingPosX+20, startingPosY+20, textura.width-20, textura.height-20), missatge);

	}

	public void showMessage(int timeToShow){
		hasToShow = true;
		Invoke("hideMessage", timeToShow);
	}

	public void hideMessage(){
		hasToShow = false;
	}
}