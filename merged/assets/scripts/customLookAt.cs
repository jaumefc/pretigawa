using UnityEngine;
using System.Collections;

public class customLookAt : MonoBehaviour {


	private Transform tPlayer;

	
	public float damping = 0.5f;
	private bool smooth = true;
	// Use this for initialization
	void Start () {
		tPlayer = GameObject.Find("Player").transform;
		//Per mantenir coherenci amb el que hi havia abans en JS
		// Make the rigid body not change rotation
		if (rigidbody)
			rigidbody.freezeRotation = true;

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void LateUpdate (){
		
		if (tPlayer) {
			if (smooth)
			{
				// Look at and dampen the rotation
				var rotation = Quaternion.LookRotation(tPlayer.position - transform.position);
				transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
			}
			else
			{
				// Just lookat
				transform.LookAt(tPlayer);
			}
		}
	}
}
