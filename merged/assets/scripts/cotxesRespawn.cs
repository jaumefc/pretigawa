using UnityEngine;
using System.Collections;

public class cotxesRespawn : MonoBehaviour {

	private float timeSinceLastSpawn = 0.0f;
	private Vector3 tempVel;
	private bool isRotating = false;
	private string rotationTriggered;
	private Quaternion startingAngle;
	private int rndForce;
	
	void Start () {
		rigidbody.AddRelativeForce (1000, 0, 0);
		rndForce = (int)(Random.Range(200.0f, 1000.0f));
	}

	void Update (){
		if(isRotating){
			float angle = Mathf.Abs(Quaternion.Angle( transform.rotation, startingAngle ));
			int factor = int.Parse(rotationTriggered);
			if(angle >= 90){
				Vector3 vec = transform.eulerAngles;
				vec.x = Mathf.Round(vec.x / 90) * 90;
				vec.y = Mathf.Round(vec.y / 90) * 90;
				vec.z = Mathf.Round(vec.z / 90) * 90;
				transform.eulerAngles = vec;

				rigidbody.velocity = new Vector3(0,0,0);
				rigidbody.angularVelocity = Vector3.zero;
				
				rigidbody.AddRelativeForce(rndForce, 0, 0);
				isRotating = false;
			}
		}
	}

	void OnTriggerEnter (Collider other){
		if (other.gameObject.name.Contains ("sqVertex")) {
			int posOrNeg = 1;
			int isGoingToRotate = Random.Range(0, 3);
			if(isGoingToRotate == 0)return;
			else if(isGoingToRotate == 2) posOrNeg = -1;
			
			tempVel = rigidbody.velocity;
			rigidbody.velocity = new Vector3(0,0,0);
			rotationTriggered = other.gameObject.name.Substring(other.gameObject.name.Length - 1, 1);
			startingAngle = transform.rotation;
			rigidbody.AddRelativeTorque(0, 200*(posOrNeg), 0);
			isRotating = true;
		}
		
		if (other.gameObject.name.Contains ("trCar")) {
			if(Time.realtimeSinceStartup - timeSinceLastSpawn < 2.0f) return;
			
			timeSinceLastSpawn = Time.realtimeSinceStartup;
			int rndNum = (int)(Random.Range(0.0f, 8.0f));
			string targetTrigger = gimmeTriggerName(rndNum);

			Transform targetPosition = GameObject.Find(targetTrigger).gameObject.transform;
			transform.position = new Vector3(targetPosition.position.x, 4.3f, targetPosition.position.z);
			transform.rotation = targetPosition.rotation;

			rigidbody.velocity = new Vector3(0,0,0);
			rndForce = (int)(Random.Range(200.0f, 1000.0f));
			rigidbody.AddRelativeForce(rndForce, 0, 0);
		}
	}

	string gimmeTriggerName(int randomNum){
		switch (randomNum) {
			case 0: return "trCarNE1";
			case 1: return "trCarNE2";
			case 2: return "trCarSE1";
			case 3: return "trCarSE2";
			case 4: return "trCarNO1";
			case 5: return "trCarNO2";
			case 6: return "trCarSO1";
			default: return "trCarSO2";
		}
	}

}
