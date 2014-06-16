using UnityEngine;
using System.Collections;


public class guiSniper:MonoBehaviour {

	public Texture2D textura;

	void OnGUI () {

		float startingPosX = Screen.width / 2 - 200;
		float startingPosY = Screen.height - 200;

		if (textura != null) {
			startingPosX = Screen.width / 2 - textura.width / 2;
			startingPosY = Screen.height - textura.height;
			GUI.Label (new Rect (startingPosX, startingPosY, textura.width, textura.height), textura);
		}

	}

}