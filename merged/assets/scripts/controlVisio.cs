using UnityEngine;
using System.Collections;

public class controlVisio : MonoBehaviour {

	private bool checkForVision = false;
	public GameObject Player;
	private RaycastHit hit;

	private bool imChasing = false;

	void Start () {
	}

	public void hasToCheckForVision(){
		checkForVision = true;
	}

	public void stopCheckingForVision(){
		checkForVision = false;
	}

	void Update () {

		if (!checkForVision) {
			if(imChasing){
				imChasing = false;
				controlMoviment contrm = gameObject.GetComponent<controlMoviment> ();
				contrm.setState(2);
			}
			return;
		}

		MeshCollider mc = GameObject.Find ("pCone2").GetComponent<MeshCollider> ();
		mc.enabled  = false;

		Vector3 newVector = Player.transform.position - transform.position;
		Ray visionRay = new Ray (transform.position, newVector);

		Vector3 eyePosition = new Vector3(transform.position.x, transform.position.y+1.3f, transform.position.z);
		Vector3 PlayerEyePosition = new Vector3(Player.transform.position.x, Player.transform.position.y+1.3f, Player.transform.position.z);

		controlMoviment cm = gameObject.GetComponent<controlMoviment> ();
		if (Physics.Raycast (visionRay, out hit)) {
			Debug.Log (hit.collider.gameObject);
			if (hit.collider.gameObject == Player) {
				cm.setTarget(Player);
				if(!imChasing){
					cm.setState(1);
					imChasing = true;
				}
			} else {
				imChasing = false;
				cm.setState(2);
			}

		}else {
			imChasing = false;
			cm.setState(2);
		}

		mc.enabled  = true;
	}

	/*void OnDrawGizmos(){
		if (!checkForVision)
			return;
		Vector3 eyePosition = new Vector3(transform.position.x, transform.position.y+1.3f, transform.position.z);
		Vector3 PlayerEyePosition = new Vector3(Player.transform.position.x, Player.transform.position.y+1.3f, Player.transform.position.z);
		Ray visionRay = new Ray (transform.position, (Player.transform.position - transform.position).normalized);
		Debug.DrawLine (eyePosition, PlayerEyePosition, Color.red, 0.2f);
		//Debug.DrawLine (transform.position, (Player.transform.position - transform.position).normalized, Color.green, 0.2f);
	}*/
}