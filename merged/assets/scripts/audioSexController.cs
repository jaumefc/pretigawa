using UnityEngine;
using System.Collections;

public class audioSexController : MonoBehaviour {

	public AudioSource audio1;
	public AudioSource audio2;
	public AudioSource audio3;

	private int audioNum = 0;

	void Start () {
		playAudioToca ();
	}

	void Update () {
	}

	private void playAudioToca(){
		audioNum++;
		if (audioNum > 3) audioNum = 1;

		switch (audioNum) {
			case 1: audio1.Play();
			break;
			case 2: audio2.Play();
			break;
			case 3:
			audio3.Play();
			break;
		}

		Invoke ("playAudioToca", 10.0f);
	}

}
