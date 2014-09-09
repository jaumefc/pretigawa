using UnityEngine;
using System.Collections;

public class TriggerDialog : MonoBehaviour {

	DialogCameraScript DCScript;
	public ConversationTreeClass StartTree;
	// Use this for initialization
	void Start () {
		DCScript = GameObject.Find ("DialogLayout").GetComponent<DialogCameraScript>();
	
	}

	void OnTriggerEnter(Collider other){
		if(other == GameObject.Find ("Player").collider){
			//DCScript.SetRootNodes(StartTree.rootNodes);
			DCScript.Init(StartTree);
		}
	}
}
