using UnityEngine;
using System.Collections;

public class BalloonScript : MonoBehaviour {

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
		if (rayCollision ())
						Destroy (gameObject);
	}

	bool rayCollision(){
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
				if(hit.collider.gameObject == gameObject)
					ret = true;
			}
		}
		return ret;
	}
}
