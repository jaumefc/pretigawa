using UnityEngine;
using System.Collections;

public class ConversationScriptPoliceman: MonoBehaviour {

	private GameState gs;
	private GameObject player;
	private GameObject shot;
	private InventoryControl ic;
	CameraControl CCScript;

	DialogCameraScript DCScript;
	public ConversationTreeClass AfterTree;

	// Use this for initialization
	void Start () {
		gs = GameState.GetInstance ();
		DCScript = GameObject.Find ("DialogLayout").GetComponent<DialogCameraScript>();
		CCScript = GameObject.Find("CameraControl").GetComponent<CameraControl>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void ReturnToTutorial(){
		CCScript.TransferOut ();
		DCScript.Init (AfterTree);
	}


}

