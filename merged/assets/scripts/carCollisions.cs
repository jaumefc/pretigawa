using UnityEngine;
using System.Collections;

public class carCollisions : MonoBehaviour {

	private GameObject mainChar;
	private CapsuleCollider cc;
	private NavMeshAgent na;
	private mouseControl mc;
	private CharacterController chc;
	public ParticleSystem pinyoParticles;
	
	private bool hasBumped = false;
	private bool goingToFall = false;
	private float timeJump;
	private GameObject SoundContainer;

	void Start () {
		mainChar = GameObject.Find ("Player");
		SoundContainer = GameObject.Find ("CarSound2");

		cc = mainChar.GetComponent<CapsuleCollider>();
		na = mainChar.GetComponent<NavMeshAgent>();
		mc = mainChar.GetComponent<mouseControl>();
		chc = mainChar.GetComponent<CharacterController>();
	}

	void Update () {

		if(Time.realtimeSinceStartup - timeJump > 0.1 && hasBumped){
			goingToFall = true;
			hasBumped = false;
		}
		
		bool isGrounded = checkAltitude();
		if(isGrounded && goingToFall){

			Invoke("resetScene", 2.5f);
			pinyoParticles.Play();
		}
	}

	void resetScene(){
		Application.LoadLevel("restart");
	}

	void OnTriggerEnter (Collider other){
		if (other.gameObject == mainChar) {
			SoundContainer.audio.Play();
			cc.enabled = true;
			na.enabled = false;
			mc.enabled = false;
			chc.enabled = false;

			int rndNum = (int)(Random.Range(-50.0f, 50.0f));
			int rndNum2 = (int)(Random.Range(-50.0f, 50.0f));
			int rndNum3 = (int)(Random.Range(-50.0f, 50.0f));
			mainChar.rigidbody.AddRelativeTorque(rndNum, rndNum2, rndNum3);
			mainChar.rigidbody.velocity = new Vector3 (0, 10, 0);

			hasBumped = true;
			timeJump = Time.realtimeSinceStartup;
		}
	}

	bool checkAltitude(){
		return mainChar.transform.position.y < 4;
	}
}
