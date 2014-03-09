using UnityEngine;
using System.Collections;

//#pragma strict

public class LookAction : Action {

	DialogCameraScript DCScript;
	//public ConversationNodeClass StartNode;

	public override void Do () {
		Debug.Log("LookAction is selected");

		MissatgePantalla MP = GameObject.Find ("CameraControl").GetComponent<MissatgePantalla> ();
		MP.missatge = "It actually looks exactly as any other cube I've seen so far...";
		MP.showMessage(4);
	}

	void Start () {
		/*
		DCScript = GameObject.Find ("DialogLayout").GetComponent<DialogCameraScript>();
		DCScript.NextNode = StartNode;
		DCScript.enabled = true;
		*/
	}
}