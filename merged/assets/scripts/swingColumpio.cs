using UnityEngine;
using System.Collections;

public class swingColumpio : MonoBehaviour {

	private int swingForward = -1;


	void Start () {
		Invoke ("changeSwing", 0.8f);
	}
	
	void Update () {
		rigidbody.angularVelocity = new Vector3 (1 * swingForward, 0, 0);
	}

	void changeSwing(){
		swingForward *= -1;
		Invoke ("changeSwing", 0.8f);
	}
}
