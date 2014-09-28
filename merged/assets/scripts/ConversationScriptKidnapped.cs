using UnityEngine;
using System.Collections;

public class ConversationScriptKidnapped : MonoBehaviour {


	private GameState gs;
	private GameObject player;
	private GameObject shot;
	private InventoryControl ic;
	// Use this for initialization
	void Start () {
		gs = GameState.GetInstance ();
		player = GameObject.Find ("Player");
		ic = player.GetComponent<InventoryControl> ();
		if(!gs.ExistsBool("FirstWC"))
			gs.AddBool ("FirstWC", true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		
	void ChangeFirstWC(){
		gs.SetBool ("FirstWC", false);
	}
	

	



}
