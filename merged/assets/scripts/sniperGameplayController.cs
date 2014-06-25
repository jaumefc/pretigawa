using UnityEngine;
using System.Collections;

public class sniperGameplayController : MonoBehaviour {

	public GameObject triggeredCams;
	public GameObject guisniper;
	public Camera sniperZoom;

	public Transform snipingDirection;
	public GameObject dardo;

	private GameObject mainChar;
	private CapsuleCollider cc;
	private mouseControl mc;
	private CharacterController chc;

	private bool isSniperGameplay = false;
	private bool moreZoom = false;
	private bool isShooting = false;
	private float changeTime = 0.0f;

	private GameObject SoundBreath;
	private GameObject SoundShoot;
	private GameObject SoundReload;

	void Start () {
		mainChar = GameObject.Find ("Player");
		SoundBreath = GameObject.Find ("soundBreath");
		SoundShoot = GameObject.Find ("soundShoot");
		SoundReload = GameObject.Find ("soundReload");

		mc = mainChar.GetComponent<mouseControl>();
		chc = mainChar.GetComponent<CharacterController>();
	}
	
	void Update () {
		if (isSniperGameplay) {
			mc.enabled = false;
			chc.enabled = false;

			GameObject.Find ("CameraSniper").camera.enabled = true;
			triggeredCams.SetActive (false);
			guisniper.SetActive(true);

			if(moreZoom) sniperZoom.fieldOfView = 6;
			else sniperZoom.fieldOfView = 12;

			if(Input.GetMouseButtonDown(1)){
				moreZoom = !moreZoom;
			}
			if(Input.GetMouseButtonDown(0)){
				if(!isShooting)shoot();
			}
		} 
		if (Input.GetKey ("h")) {

			if(Time.realtimeSinceStartup - changeTime > 0.1f){
				changeTime = Time.realtimeSinceStartup;
				isSniperGameplay = !isSniperGameplay;

				if(!isSniperGameplay){
					SoundBreath.audio.Stop();

					mc.enabled = true;
					chc.enabled = true;
					
					GameObject.Find ("CameraSniper").camera.enabled = false;
					triggeredCams.SetActive (true);
					guisniper.SetActive(false);
				}

				else{
					SoundBreath.audio.Play();
				}
			}
		}
	}

	private void shoot(){
		isShooting = true;
		SoundBreath.audio.Stop();
		SoundShoot.audio.Play();
		Invoke ("reload", 1.0f);
		GameObject dardoCopia = Instantiate (dardo, snipingDirection.position, snipingDirection.rotation) as GameObject;

		var locVel = dardoCopia.transform.TransformDirection(new Vector3(0, 0, 5));
		dardoCopia.rigidbody.velocity = locVel;
		dardoCopia.rigidbody.angularVelocity = -locVel;
	}

	private void reload(){
		SoundReload.audio.Play();
		isShooting = false;
	}

}
