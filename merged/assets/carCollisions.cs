using UnityEngine;
using System.Collections;

public class carCollisions : MonoBehaviour {

	public GameObject mainChar;
	private CapsuleCollider cc;
	private NavMeshAgent na;
	private mouseControl mc;
	private CharacterController chc;
	public cotxesRespawn cr;
	public ParticleSystem pinyoParticles;
	
	private bool hasBumped = false;
	private bool goingToFall = false;
	private float timeJump;

	void Start () {
		cc = mainChar.GetComponent<CapsuleCollider>();
		na = mainChar.GetComponent<NavMeshAgent>();
		mc = mainChar.GetComponent<mouseControl>();
		chc = mainChar.GetComponent<CharacterController>();
	}

	void Update () {
		if (Input.GetKeyDown ("f")) {
			cr.enabled = true;
		}

		if(Time.realtimeSinceStartup - timeJump > 0.1 && hasBumped){
			goingToFall = true;
			hasBumped = false;
		}
		
		bool isGrounded = checkAltitude();
		if(isGrounded && goingToFall){

			Invoke("resetScene", 2.5f);
			pinyoParticles.Play();
			/*
			Color originalColor = mainChar.renderer.material.color;
			mainChar.renderer.material.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0.0f);
			*/
			/* Tongo per no morir en cas de ser atropellat
			na.enabled = true;
			mc.enabled = true;
			chc.enabled = true;
			cc.enabled = false;
			goingToFall = false;
			*/
		}
	}

	void resetScene(){
		Application.LoadLevel("restart");
	}

	void OnTriggerEnter (Collider other){
		if (other.gameObject == mainChar) {
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
