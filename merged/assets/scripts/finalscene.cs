﻿using UnityEngine;
using System.Collections;

public class finalscene : MonoBehaviour {


	private GameObject mainChar;
	private Vector3 posini;
	private NavMeshAgent navi;
	private mouseControl MouControl;
	public DialogCameraScript DCScript;
	public ConversationTreeClass StartTree;

	public GameObject extintor;
	public AudioSource extintorSo;
	public ParticleSystem particlesExtintor;
	public GameObject videofinal;

	private bool alreadyPanned = false;
	private bool dialogEnded = false;
	private bool hasToThrowExtinguisher = false;

	public GameObject audioMusic;

	// Use this for initialization
	void Start () {
		mainChar = GameObject.Find ("Player");
		posini = mainChar.transform.position;

		MouControl = mainChar.GetComponent<mouseControl>();
		MouControl.enabled = true;
		MouControl.characterStatic (true);
		Invoke ("repositionPlayer", 0.1f);

	}

	public void droppedExting(){
		hasToThrowExtinguisher = true;
	}

	public void dialogEnd(){
		dialogEnded = true;
	}

	private void repositionPlayer(){
		NavMeshAgent NA = mainChar.GetComponent ("NavMeshAgent") as NavMeshAgent;
		NA.Stop ();
		NA.enabled = false;

		mainChar.transform.position = posini;
	}

	private void llencaextintor(){
		extintor.SetActive (true);
	}

	private void partsExtintor(){
		extintor.SetActive (false);
		particlesExtintor.Play ();
		particlesExtintor.gameObject.audio.Play ();
	}

	private void cutscene(){
		audioMusic.audio.Stop ();
		mainChar.GetComponent<AudioListener>().enabled = false;
		videofinal.SetActive (true);
	}

	void Update () {
		if (Input.GetKey (KeyCode.Escape)) {
			Application.LoadLevel("menu");
		}
		if (alreadyPanned == true && dialogEnded == false)
				return;
		else if (dialogEnded == false) {
				pathController pc = GameObject.Find ("CameraIni").GetComponent<pathController> ();
				if (pc.posicioActual >= 60) {
						DCScript.Init (StartTree);
						alreadyPanned = true;
				}
		} else if(hasToThrowExtinguisher == true){
			hasToThrowExtinguisher = false;
			animationControl animcontrol = mainChar.GetComponent<animationControl>();
			Animation anims = mainChar.GetComponent<Animation>();
			animcontrol.enabled = false;
			anims.Play("Jump");
			
			extintorSo.Play();
			Invoke("llencaextintor", 0.75f);
			Invoke("partsExtintor", 2.5f);
			Invoke ("cutscene", 4.0f);
		}
	}


}
