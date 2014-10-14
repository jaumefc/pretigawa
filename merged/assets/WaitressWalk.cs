using UnityEngine;
using System.Collections;

public class WaitressWalk : MonoBehaviour {

	public GameObject Waitress;
	public Transform WaitressOrigin;
	public Transform WaitressEnd;
	float initTime=-1;
	public GameObject Txan;


	
	
	void OnTriggerEnter(Collider other){
		if (other == GameObject.Find ("Player").collider) {
			initTime = Time.time + 2f;
			Txan.audio.Play();
		}
	}
					
	// Update is called once per frame
	void Update () {
		if ((Waitress.transform.position!=WaitressEnd.transform.position)&&(initTime!=-1)){
			Waitress.transform.position= Vector3.Lerp(WaitressOrigin.transform.position,WaitressEnd.transform.position,(Time.time-initTime) * 3f);
			Waitress.transform.rotation= Quaternion.Lerp(WaitressOrigin.transform.rotation,WaitressEnd.transform.rotation,(Time.time-initTime) * 3f);
		}
	}
}







		
		
		