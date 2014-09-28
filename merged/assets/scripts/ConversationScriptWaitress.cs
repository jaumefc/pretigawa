using UnityEngine;
using System.Collections;

public class ConversationScriptWaitress : MonoBehaviour {


	private GameState gs;
	private GameObject player;
	private GameObject shot;
	private InventoryControl ic;
	// Use this for initialization
	void Start () {
		gs = GameState.GetInstance ();
		player = GameObject.Find ("Player");
		shot = GameObject.Find ("shot");
		ic = player.GetComponent<InventoryControl> ();
		if(!gs.ExistsBool("RecipeFirstTime"))
			gs.AddBool ("RecipeFirstTime", true);
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void FalseRecipeFirstTime(){
		gs.SetBool ("RecipeFirstTime", false);
	}

	
	void GetShot(){
		//shot in inventory
		ic.Add2 (shot);

	}



}
