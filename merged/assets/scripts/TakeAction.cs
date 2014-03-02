using UnityEngine;
using System.Collections;

//#pragma strict

public class TakeAction : Action {
	public override void Do () {
		Debug.Log("TakeAction is selected");
		//GameObject.Find("Player").GetComponent<mouseControl>().GoTo(gameObject.transform.parent.position);
		GameObject.Find("Player").GetComponent<mouseControl>().Take(gameObject.transform.parent.parent.gameObject);
	}
	
}