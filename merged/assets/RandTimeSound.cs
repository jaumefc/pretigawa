using UnityEngine;
using System.Collections;

public class RandTimeSound : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Invoke("PlaySound", Random.Range(5, 20));
	}

	void PlaySound () {
		if (!audio.isPlaying)
			audio.Play();
		
		Invoke("PlaySound", Random.Range(5, 20));
	}
}



