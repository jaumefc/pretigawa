using UnityEngine;
using System.Collections;

public class sniperGameplayController : MonoBehaviour {

	public GameObject triggeredCams;
	public Camera sniperZoom;

	public GameObject sniperGameplay;

	public Transform snipingDirection;
	public GameObject dardo;
	public ParticleSystem fxShoot;

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
				
				if (moreZoom)
						sniperZoom.fieldOfView = 6;
				else
						sniperZoom.fieldOfView = 12;

				if (Input.GetMouseButtonDown (1)) {
						moreZoom = !moreZoom;
				}
				if (Input.GetMouseButtonDown (0)) {
						if (!isShooting)
								shoot ();
				}
		}
	}

	public bool isActive(){
		return isSniperGameplay;
	}

	public void setActive(bool isActive){

		isSniperGameplay = isActive;

		if(!isSniperGameplay){
			SoundBreath.audio.Stop();
			CancelInvoke ("audioHeart");
			mainChar.audio.volume = 1.0f;
			mc.enabled = true;
			chc.enabled = true;
			
			GameObject.Find ("CameraSniper").camera.enabled = false;
			triggeredCams.SetActive (true);
			sniperGameplay.SetActive(false);
		}
		
		else{
			sniperGameplay.SetActive(true);
			
			mc.enabled = false;
			chc.enabled = false;
			
			GameObject.Find ("CameraSniper").camera.enabled = true;
			triggeredCams.SetActive (false);
			
			mainChar.audio.volume = 0.25f;
			SoundBreath.audio.Play();
		}
	}

	private void shoot(){
		isShooting = true;
		fxShoot.Play ();
		SoundBreath.audio.Stop();
		SoundShoot.audio.Play();
		Invoke ("reload", 0.75f);
		CancelInvoke ("audioHeart");
		Invoke ("audioHeart", 2.5f);
		GameObject dardoCopia = Instantiate (dardo, snipingDirection.position, snipingDirection.rotation) as GameObject;

		var locVel = dardoCopia.transform.TransformDirection(new Vector3(0, 0, 20));
		dardoCopia.rigidbody.velocity = locVel;
		dardoCopia.rigidbody.angularVelocity = -locVel;
	}

	private void audioHeart(){
		SoundBreath.audio.Play();
	}

	private void reload(){
		SoundReload.audio.Play();

		isShooting = false;
	}

}
