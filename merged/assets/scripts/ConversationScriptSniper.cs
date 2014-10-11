using UnityEngine;
using System.Collections;

public class ConversationScriptSniper: MonoBehaviour {
	
	
	private GameState gs;
	private GameObject player;
	// Use this for initialization
	void Start () {
		gs = GameState.GetInstance ();
		player = GameObject.Find ("Player");
		if(!gs.ExistsBool("SniperFirst"))
			gs.AddBool ("SniperFirst", true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	

	
	void FalseSniperFirst(){
		gs.SetBool ("SniperFirst", false);
	}

}
