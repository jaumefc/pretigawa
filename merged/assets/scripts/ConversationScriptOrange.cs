using UnityEngine;
using System.Collections;

public class ConversationScriptOrange : MonoBehaviour {


	private GameState gs;
	private GameObject player;
	// Use this for initialization
	void Start () {

		gs = GameState.GetInstance ();

		if(!gs.ExistsBool("OrangeFainted"))
			gs.AddBool ("OrangeFainted", true);
	}
	
	// Update is called once per frame
	void Update () {
	
	}





}
