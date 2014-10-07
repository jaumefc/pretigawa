using UnityEngine;
using System.Collections;

public class balloonCount : MonoBehaviour {

	public sniperGameplayController sniperController;
	public int totalBalloons = 10;

	public GameObject soundWin;

	private int poppedBalloons = 0;
	private bool isEndingGameplay = false;
	public GUIStyle customGuiStyle;

	void Start () {
	}
	
	void Update () {
		if (poppedBalloons >= totalBalloons) {
			if(!isEndingGameplay){
				isEndingGameplay = true;
				soundWin.audio.Play();
				Invoke("endGameplay", 1.0f);
			}
		}
	}

	private void endGameplay(){
		sniperController.setActive(false);
		Destroy(GameObject.Find ("SniperRifle"));
		GameObject[] dardos = GameObject.FindGameObjectsWithTag("Dardos");
		GameObject[] fxExplosio = GameObject.FindGameObjectsWithTag("Globusplosio");
		
		foreach (GameObject gtemp in dardos)
			Destroy (gtemp);
		
		foreach (GameObject fxtemp in fxExplosio)
			Destroy (fxtemp);
		Destroy (this.transform.parent.gameObject);
	}

	public void popBalloon(){
		poppedBalloons++;
	}

	void OnGUI () {
		GUI.Label(new Rect(20, 20, Screen.height/8, Screen.height/8), Resources.Load("textures/GUI_elements/balloon") as Texture2D);
		GUI.Label(new Rect(30+Screen.height/8, 20, 100, 20+Screen.height/16), poppedBalloons+" / "+totalBalloons, customGuiStyle);
	}

}
