using UnityEngine;
using System.Collections;

public class ConversationScriptWaitress : MonoBehaviour {


	private GameState gs;
	private GameObject player;
	private GameObject shot,bottle;
	private InventoryControl ic;
	public InventoryObject recipe;
	// Use this for initialization
	void Start () {
		gs = GameState.GetInstance ();
		player = GameObject.Find ("Player");
		shot = GameObject.Find ("shot");
		bottle = GameObject.Find ("bottle");
		ic = player.GetComponent<InventoryControl> ();
		if(!gs.ExistsBool("RecipeFirstTime"))
			gs.AddBool ("RecipeFirstTime", true);

		//Get Ingredients
		if(!gs.ExistsBool("BottleGot"))
			gs.AddBool ("BottleGot", false);
		if(!gs.ExistsBool("ShotGot"))
			gs.AddBool ("ShotGot", false);
		if(!gs.ExistsBool("BloodyGot"))
			gs.AddBool ("BloodyGot", false);
		if(!gs.ExistsBool("CosmoGot"))
			gs.AddBool ("CosmoGot", false);


		//FortuneTeller
		if(!gs.ExistsBool("LovingManSaid"))
			gs.AddBool ("LovingManSaid", false);
		if(!gs.ExistsBool("IntelligentManSaid"))
			gs.AddBool ("IntelligentManSaid", false);
		if(!gs.ExistsBool("WealthyManSaid"))
			gs.AddBool ("WealthyManSaid", false);
		if(!gs.ExistsBool("DangerousManChosen"))
			gs.AddBool ("DangerousManChosen", false);




	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void FalseRecipeFirstTime(){
		gs.SetBool ("RecipeFirstTime", false);
		recipe.SetState (InventoryObject.InventoryObjectState.TAKEN);
	}

	void GetShot(){
		//shot in inventory
		//ic.Add2 (shot);
		gs.SetBool ("ShotGot", true);
	}

	void GetBloody(){
		gs.SetBool ("BloodyGot", true);
	}

	void GetCosmo(){
		gs.SetBool ("CosmoGot", true);
	}


	void TrueLovingManSaid(){
		gs.SetBool ("LovingManSaid", true);
	}	
	void TrueIntelligentManSaid(){
		gs.SetBool ("IntelligentManSaid", true);
	}	
	void TrueWealthyManSaid(){
		gs.SetBool ("WealthyManSaid", true);
	}	
	void TrueDangerousManChosen(){
		gs.SetBool ("DangerousManChosen", true);
	}



	




}
