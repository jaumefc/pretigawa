using UnityEngine;
using System.Collections;

public class extintorLlencat : MonoBehaviour {

	void Start () {
	}
	
	void Update () {
	}

	public void llencatExtinguisher(){ 
		GameObject.Find ("sceneFinal").GetComponent<finalscene> ().droppedExting();
	}

}
