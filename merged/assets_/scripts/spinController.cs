using UnityEngine;
using System.Collections;

public class spinController : MonoBehaviour {

	public Vector3 rotation;

	void Start () {
		rigidbody.angularVelocity = rotation;
	}

	void Update () {
	}
}
