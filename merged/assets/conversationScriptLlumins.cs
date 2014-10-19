using UnityEngine;
using System.Collections;

public class conversationScriptLlumins : MonoBehaviour {

	
	private GameState gs;
	public GameObject llumins;
	public InventoryObject flamingCoctail;
	public InventoryObject coctail;
	// Use this for initialization
	void Start () {
		gs = GameState.GetInstance ();
	}
	
	// Update is called once per frame
	void Update () {
		
		
	}
	
	void take(){
		coctail.SetState (InventoryObject.InventoryObjectState.UNTAKEN);
		flamingCoctail.SetState (InventoryObject.InventoryObjectState.TAKEN);
		llumins.SetActive (false);
	}

}
