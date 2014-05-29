using UnityEngine;
using System.Collections;

public class controlVisio : MonoBehaviour {

	private bool ignoreIA = false;

	public GameObject Player;
	private RaycastHit hit;
	private controlMoviment cm;
	private bool firstTimeSeen = true;
	private Animation animations;

	void Start () {
		cm = gameObject.GetComponent<controlMoviment> ();
		animations = GetComponent<Animation>();
	}

	public void changeIAStatus(){
		ignoreIA = !ignoreIA;
	}

	void Update () {

		if (ignoreIA) {
			cm.setState (2);
			return;
		}

		if (cm.checkingForVision () == true) {

			Vector3 compareVector = Player.transform.position - transform.position;
			float angle = Vector3.Angle (compareVector, transform.forward);
			float distance = compareVector.sqrMagnitude;

			if (distance < 100 && angle < 60) {
					Vector3 eyePosition = new Vector3 (transform.position.x, transform.position.y + 1.0f, transform.position.z);
					Vector3 PlayerEyePosition = new Vector3 (Player.transform.position.x, Player.transform.position.y + 1.3f, Player.transform.position.z);

					Vector3 newVector = PlayerEyePosition - eyePosition;
					Ray visionRay = new Ray (eyePosition, newVector);

					if (Physics.Raycast (visionRay, out hit)) {
							if (hit.collider.gameObject == Player) {
									if(firstTimeSeen){
										animations.CrossFade("Angry");
										Invoke("sawHim", 3);
										return;
									}else{
										cm.setTarget (Player);
										cm.setState (1);
										return;
									}
							}
							cm.setState (2);
					}
			} else if(cm.imAtHome() == false){
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