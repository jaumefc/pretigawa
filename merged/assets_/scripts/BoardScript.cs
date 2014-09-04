using UnityEngine;
using System.Collections;

public class BoardScript : MonoBehaviour {
	
	private Transform thisTransform;
	private Camera cam;

	// Use this for initialization
	void Start () {
		thisTransform = transform;
//		cam = GameObject.Find ("CreditsCamera").GetComponent<Camera> ();
		cam = Camera.main;
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 point = new Vector3();
		if (rayCollision (out point)) {
			Debug.Log("Hit Board!!");
			//gameObject.rigidbody.AddForce(0,20,60);
			gameObject.rigidbody.AddForceAtPosition(new Vector3(0,20,60),point);
		}
	
	}

	bool rayCollision(out Vector3 point){
		bool ret = false;
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = cam.ScreenPointToRay (Input.mousePosition);
			int count = Input.touchCount;	
			if (count == 1) {
				Touch touch = Input.GetTouch (0);
				ray = cam.ScreenPointToRay (new Vector3 (touch.position.x, touch.position.y));
			}
			
			RaycastHit hit;
			if( Physics.Raycast(ray, out hit))
			{
				if(hit.collider.gameObject == gameObject){
					point = hit.point;
					ret = true;
				}
			}
		}
		point = Vector3.zero;
		return ret;
	}
}
