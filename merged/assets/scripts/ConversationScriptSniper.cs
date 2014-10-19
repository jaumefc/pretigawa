using UnityEngine;
using System.Collections;

public class ConversationScriptSniper: MonoBehaviour {
	
	
	private GameState gs;
	private GameObject player;
	public InventoryObject ioBottle;
	public InventoryObject coctail;
	public GameObject sniper;
	public GameObject waitress;
	public GameObject sex;
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

	void takeCigar(){
		gs.SetInt ("cigar",2);
		checkObjectsReceipe ();
		sniper.SetActive (false);
		waitress.SetActive (false);
		sex.SetActive (true);

	}
	
	void checkObjectsReceipe(){
		if (gs.GetBool ("CosmoGot") && gs.GetBool ("ShotGot") && gs.GetBool ("BloodyGot") && gs.GetInt ("cacahuets") == 2 && gs.GetInt ("cigar") == 2) {
//			gs.SetInt("coctail",1);
//			gs.SetInt("bottle",2);
			ioBottle.SetState(InventoryObject.InventoryObjectState.USED);
			coctail.SetState(InventoryObject.InventoryObjectState.TAKEN);
		}
	}

}
