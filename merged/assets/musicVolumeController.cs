using UnityEngine;
using System.Collections;

public class musicVolumeController : MonoBehaviour {

	private mouseControl mouseController;
	private AudioSource musica;

	void Start () {
		mouseController = GetComponent<mouseControl> ();
		musica = GetComponent<AudioSource> ();
	}
	

	void Update () {
		if (mouseController.enabled == false)
			musica.audio.volume = 0.25f;
		else
			musica.audio.volume = 1.0f;
	}
}
