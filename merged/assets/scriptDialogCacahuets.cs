using UnityEngine;
using System.Collections;

public class scriptDialogCacahuets : MonoBehaviour {
	
	private GameState gs;
	public GameObject cacahuets;
	public InventoryObject ioBottle;
	public InventoryObject coctail;
	// Use this for initialization
	void Start () {
		gs = GameState.GetInstance ();
	}
	
	// Update is called once per frame
	void Update () {

		
	}

	void take(){
		Debug.Log ("TAKE CACAHUETS!!");
		gs.SetInt ("cacahuets",2);
		cacahuets.SetActive (false);
		checkObjectsReceipe ();
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
