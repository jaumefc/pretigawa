using UnityEngine;
using System.Collections;

public class getSuckedActionPoliceman : UseAction {
	
	public InventoryCustom ICustom;
	public string sDestroy;
		
	DialogCameraScript DCScript;
	public ConversationTreeClass AfterTree;

	void Start () {
		DCScript = GameObject.Find ("DialogLayout").GetComponent<DialogCameraScript>();
	}

	
	public override void Do () {
		ICustom.taken = true;
		GameState.GetInstance().GameSave();
		Destroy(GameObject.Find(sDestroy));
		Debug.Log("Got Sucked !");
		DCScript.Init (AfterTree);

	}
}
