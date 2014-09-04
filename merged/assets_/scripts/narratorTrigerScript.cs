using UnityEngine;
using System.Collections;

public class narratorTrigerScript : MonoBehaviour {

	DialogCameraScript DCScript;
	GameObject CharParent;
	public ConversationNodeClass StartNode;
	public ConversationTreeClass StartTree;
	// Use this for initialization
	void Start () {
		DCScript = GameObject.Find ("DialogLayout").GetComponent<DialogCameraScript>();
	
	}

	void OnTriggerEnter(Collider other){
		if(other == GameObject.Find ("Player").collider){
			DCScript.SetRootNodes(StartTree.rootNodes);
			DCScript.Init();
		}
	}
}
