using UnityEngine;
using System.Collections;

public class ConversationScriptWaitress : MonoBehaviour {


	private GameState gs;
	private GameObject player;
	private GameObject shot,bottle;
	private InventoryControl ic;
	public InventoryObject recipe;
	public InventoryObject ioBottle;
	public InventoryObject coctail;
	public GameObject fortuneTeller;
	Transform glassTrans;

	private GameObject glass;
	// Use this for initialization
	void Start () {
		gs = GameState.GetInstance ();
		player = GameObject.Find ("Player");
		shot = GameObject.Find ("shot");
		bottle = GameObject.Find ("bottle");
		glass = GameObject.Find ("glass");
		glassTrans = (Transform)GameObject.Find ("glassControlPoint").GetComponent<Transform> ();
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
		checkObjectsReceipe ();
		clearGlass ();
	}

	void GetBloody(){
		gs.SetBool ("BloodyGot", true);
		checkObjectsReceipe();
		clearGlass ();
	}

	void GetCosmo(){
		gs.SetBool ("CosmoGot", true);
		checkObjectsReceipe ();
		clearGlass ();
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
	
	void checkObjectsReceipe(){
		if (gs.GetBool ("CosmoGot") && gs.GetBool ("ShotGot") && gs.GetBool ("BloodyGot") && gs.GetInt ("cacahuets") == 2 && gs.GetInt ("cigar") == 2) {
			//gs.SetInt("coctail",1);
			//gs.SetInt("bottle",2);
			ioBottle.SetState(InventoryObject.InventoryObjectState.USED);
			coctail.SetState(InventoryObject.InventoryObjectState.TAKEN);
		}
		if (gs.GetBool ("CosmoGot") && gs.GetBool ("ShotGot") && gs.GetBool ("BloodyGot")) {
			fortuneTeller.SetActive (true);
		}
	}

	public void clearGlass(){
		Debug.Log("Soy un puto vasooo1111111111!");
		Invoke ("disableGlass",2f);
	}

	void disableGlass(){
		Debug.Log("Soy un puto vasooo!");
		//glass.transform.position = glassTrans.position;
		glass.SetActive (false);
	}

	void showGlass(){
		glass.SetActive (true);
	}


	




}
