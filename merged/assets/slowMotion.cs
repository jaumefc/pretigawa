using UnityEngine;
using System.Collections;

public class slowMotion : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.timeScale > 1.0f)Time.timeScale = 1.0f;

		if (Input.GetKey ("g")) {
			if(Time.timeScale == 1.0f){
				Time.timeScale = 0.2f;
			}
		} else {
			Time.timeScale = Time.timeScale*1.1f;
		}
	}
}
