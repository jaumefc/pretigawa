using UnityEngine;
using System.Collections;

//#pragma strict

public class LookAction : Action {

	DialogCameraScript DCScript;
	public ConversationNodeClass StartNode;

	void Start(){
		DCScript = GameObject.Find ("DialogLayout").GetComponent<DialogCameraScript>();
	}

	public override void Do () {
		Debug.Log("LookAction is selected");
		DCScript.NextNode = StartNode;
		DCScript.enabled = true;

	}
}