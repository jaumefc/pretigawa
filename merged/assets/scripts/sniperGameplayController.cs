using UnityEngine;
using System.Collections;

public class sniperGameplayController : MonoBehaviour {

	public GameObject triggeredCams;
	private bool isSniperGameplay = false;
	float changeTime = 0.0f;

	void Start () {
		triggeredCams = GameObject.Find ("TriggeredCAMS");
	}
	
	void Update () {
		if (isSniperGameplay) {
			GameObject.Find ("CameraSniper").camera.enabled = true;
			triggeredCams.SetActive (false);
		} else {
			GameObject.Find ("CameraSniper").camera.enabled = false;
			triggeredCams.SetActive (true);
		}
		if (Input.GetKey ("h")) {
			if(Time.realtimeSinceStartup - changeTime > 0.1f){
				changeTime = Time.realtimeSinceStartup;
				isSniperGameplay = !isSniperGameplay;
			}
		}
	}
}
