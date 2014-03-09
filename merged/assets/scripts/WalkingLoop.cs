using UnityEngine;
using System.Collections;

public class WalkingLoop : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.animation.animation.wrapMode = WrapMode.Loop;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
