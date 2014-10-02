using UnityEngine;
using System.Collections;

public class controlVisio : MonoBehaviour {

	private bool ignoreIA = false;

	private GameObject Player;
	private RaycastHit hit;
	private controlMoviment cm;
	private bool firstTimeSeen = true;
	private Animation animations;
	private float lastTimeSeen = 0.0f;

	void Start () {
		Player = GameObject.Find ("Player");
		cm = gameObject.GetComponent<controlMoviment> ();
		animations = GetComponent<Animation>();
	}

	public void changeIAStatus(){
		ignoreIA = !ignoreIA;
	}

	public void iaOff(){
		ignoreIA = true;
	}

	public void iaOn(){
		ignoreIA = false;
	}

	public bool getIA(){
		return !ignoreIA;
	}

	void Update () {

		if (cm.checkingForVision () == true) {

			Vector3 compareVector = Player.transform.position - transform.position;
			float angle = Vector3.Angle (compareVector, transform.forward);
			float distance = compareVector.sqrMagnitude;

			if (distance < 100 && angle < 60 && !ignoreIA) {
					Vector3 eyePosition = new Vector3 (transform.position.x, transform.position.y + 1.0f, transform.position.z);
					Vector3 PlayerEyePosition = new Vector3 (Player.transform.position.x, Player.transform.position.y + 1.3f, Player.transform.position.z);

					Vector3 newVector = PlayerEyePosition - eyePosition;
					Ray visionRay = new Ray (eyePosition, newVector);

					if (Physics.Raycast (visionRay, out hit)) {
							if (hit.collider.gameObject == Player) {
									if(firstTimeSeen){
										cm.setTarget(gameObject);
										animations.CrossFade("Angry");
										Invoke("sawHim", 3);
										return;
									}else{
										if((Time.realtimeSinceStartup - lastTimeSeen) > 10.0f && cm.getTarget() != Player){
											AudioSource metalAlert = GetComponent<AudioSource>();
											metalAlert.Play();
											lastTimeSeen = Time.realtimeSinceStartup;
										}
										cm.setTarget (Player);
										cm.setState (1);
										return;
									}
							}
							cm.setState (2);
					}
			} else if(cm.imAtHome() == false){
				if(cm.hasTarget == false)
					cm.setState (2);
				return;
			}else{
				firstTimeSeen = true;
				cm.setState (0);
			}
		}
	}

	private void sawHim(){
		firstTimeSeen = false;
	}
}