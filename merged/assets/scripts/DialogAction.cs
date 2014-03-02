using UnityEngine;
using System.Collections;

//#pragma strict

public class DialogAction : Action {

	CameraControl CCScript;
	DialogCameraScript DCScript;
	GameObject CharParent;
	public ConversationNodeClass StartNode;

	// Use this for initialization
	void Start () {
		
		CCScript = GameObject.Find("CameraControl").GetComponent<CameraControl>();
		DCScript = GameObject.Find ("DialogLayout").GetComponent<DialogCameraScript>();
		
	}

	public override void Do () {
		Debug.Log("DialogAction is selected");
		Debug.Log ("MouseDetected");
		
		CCScript.TransferIn(gameObject.transform.parent.parent.gameObject.GetComponentInChildren<Camera>());
		Debug.Log ("Fent el transferIn");
		
		
		//		DCScript.NextNode = (ConversationNodeClass)transform.parent.gameObject.GetComponent ("Char2Conversation").GetComponent ("CN00");
		DCScript.NextNode = StartNode;

	}
}