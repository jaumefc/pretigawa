using UnityEngine;
using System.Collections;

//#pragma strict

public class DialogAction : Action {
	
	//CameraControl CCScript;
	mouseControl MCScript;
	DialogCameraScript DCScript;
	GameObject CharParent;
	public ConversationNodeClass StartNode;
	public ConversationTreeClass StartTree;
	
	// Use this for initialization
	void Start () {
		
		//CCScript = GameObject.Find("CameraControl").GetComponent<CameraControl>();
		DCScript = GameObject.Find ("DialogLayout").GetComponent<DialogCameraScript>();
		MCScript = GameObject.Find ("Player").GetComponent<mouseControl> ();
	}
	
	public override void Do () {
		Debug.Log("DialogAction is selected");
		Debug.Log ("MouseDetected");
		MCScript.Dialog(gameObject.transform.parent.parent.gameObject);
		
		//CCScript.TransferIn(gameObject.transform.parent.parent.gameObject.GetComponentInChildren<Camera>());
		//Debug.Log ("Fent el transferIn");
		
		
		//DCScript.NextNode = StartNode;
		//DCScript.SetRootNodes(StartTree.rootNodes);
		DCScript.SetConvesationTree (StartTree);
		
	}
}
