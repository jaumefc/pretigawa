using UnityEngine;
using System.Collections;

public class MusicController: MonoBehaviour {


	public int CurrentMusic;
	public AudioClip[] AudioClips;
	private AudioSource ControlledAS;





	// Use this for initialization
	void Start () {
		ControlledAS = (AudioSource)GetComponent(typeof(AudioSource));
		CurrentMusic = 0;
	}
	
	// Update is called once per frame
	void Update () {
		ControlledAS.clip = AudioClips[CurrentMusic];
		if (!audio.isPlaying)
		audio.Play();
	}


}
