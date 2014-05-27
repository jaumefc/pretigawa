using UnityEngine;
using System.Collections;

public class controlVisio : MonoBehaviour {

	private bool ignoreIA = false;

	public GameObject Player;
	private RaycastHit hit;
	private controlMoviment cm;
	private bool isOutOfVision = false;

	void Start () {
		cm = gameObject.GetComponent<controlMoviment> ();
	}

	void Update () {
		if (Input.GetKeyDown ("space"))ignoreIA = !ignoreIA;

		if (ignoreIA) {
			cm.setState(2);
			return;
		}

		Vector3 compareVector = Player.transform.position - transform.position;
		float angle = Vector3.Angle (compareVector, transform.forward);
		float distance = compareVector.sqrMagnitude;

		if (distance < 100 && angle < 60) {
			Vector3 eyePosition = new Vector3 (transform.position.x, transform.position.y + 1.0f, transform.position.z);
			Vector3 PlayerEyePosition = new Vector3 (Player.transform.position.x, Player.transform.position.y + 1.3f, Player.transform.position.z);

			Vector3 newVector = PlayerEyePosition - eyePosition;
			Ray visionRay = new Ray (eyePosition, newVector);

			if (Physics.Raycast (visionRay, out hit)) {
					//Debug.Log (hit.collider.gameObject);
					if (hit.collider.gameObject == Player) {
							cm.setTarget (Player);
							cm.setState (1);
							return;
					}
					cm.setState (2);
			}
		} else {
			cm.setState(2);
			return;
		}
	}

	void OnDrawGizmos(){
		Vector3 eyePosition = new Vector3(transform.position.x, transform.position.y+1.0f, transform.position.z);
		Vector3 PlayerEyePosition = new Vector3(Player.transform.position.x, Player.transform.position.y+1.3f, Player.transform.position.z);
		//Debug.DrawLine (eyePosition, PlayerEyePosition, Color.red, 0.2f);
	}
}