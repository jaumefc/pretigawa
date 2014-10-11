using UnityEngine;
using System.Collections;

public class PictureDialogScript : MonoBehaviour {
	
	
	private GameState gs;
	private GameObject player;
	private GameObject shot;
	private InventoryControl ic;
	// Use this for initialization
	void Start () {
		gs = GameState.GetInstance ();
		player = GameObject.Find ("Player");
		ic = player.GetComponent<InventoryControl> ();
		if(!gs.ExistsBool("WCEnabled"))
			gs.AddBool ("WCEnabled", false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void EnableWC(){
		gs.SetBool ("WCEnabled", true);
	}
	
	
	
	
	
	
}
