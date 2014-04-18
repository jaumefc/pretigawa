using UnityEngine;
using System.Collections;

public class enemyTriggerControl : MonoBehaviour {


	public GameObject mainChar;

	void Start () {
	}

	void Update () {
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject == mainChar) {
			controlVisio cv = this.transform.parent.gameObject.GetComponent<controlVisio> ();
			cv.hasToCheckForVision ();
		}
	}

	void OnTriggerExit(Collider other){
		if (other.gameObject == mainChar) {
			controlVisio cv = this.transform.parent.gameObject.GetComponent<controlVisio> ();
			cv.stopCheckingForVision ();
		}
	}

}
