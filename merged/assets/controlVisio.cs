using UnityEngine;
using System.Collections;

public class controlVisio : MonoBehaviour {

	public float maxRange = 50000;
	public float horizontalAngle;
	public float verticalAngle;

	public GameObject Player;

	private RaycastHit hit;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		Ray visionRay = new Ray (transform.position, Quaternion.FromToRotation(transform.position, Player.transform.position).eulerAngles);

		//Debug.DrawRay (this.transform.position, Player.transform.position, Color.green);

		if(Vector3.Distance(this.transform.position, Player.transform.position) < maxRange )
		{
			Vector3 myPosX = new Vector3(transform.forward.x, 0, transform.forward.z);
			Vector3 myPosY = new Vector3(0, transform.forward.y, transform.forward.z);

			float angleX = Vector3.Angle(myPosX, visionRay.direction);
			float angleY = Vector3.Angle(myPosY, visionRay.direction);

			Debug.Log(angleX);

			if(angleX<0)angleX *= -1;
			if(angleY<0)angleY *= -1;

			Vector3 eyePosition = new Vector3(transform.position.x, transform.position.y+1.3f, transform.position.z);
			Vector3 PlayerEyePosition = new Vector3(Player.transform.position.x, Player.transform.position.y+1.3f, Player.transform.position.z);

			if(Physics.Raycast(visionRay, out hit, maxRange))
			{				

				if(angleX <= horizontalAngle && angleY <= verticalAngle){

					if(hit.collider.gameObject == Player)
					{
						Debug.Log("I see you !");
					}
					else{
						Debug.Log ("Seeing: "+hit.collider.gameObject);
					}

				}
			}
		}
	}

	void OnDrawGizmos(){
		Vector3 eyePosition = new Vector3(transform.position.x, transform.position.y+1.3f, transform.position.z);
		Vector3 PlayerEyePosition = new Vector3(Player.transform.position.x, Player.transform.position.y+1.3f, Player.transform.position.z);

		Debug.DrawLine (eyePosition, PlayerEyePosition, Color.red, 1f);
		Debug.DrawLine (transform.position, transform.position+transform.forward*2, Color.green, 1f);
	}
}