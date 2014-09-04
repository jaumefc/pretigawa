using UnityEngine;
using System.Collections;

public class ConversationScriptOrange : MonoBehaviour {


	private GameState gs;
	private GameObject player;
	// Use this for initialization
	void Start () {
/*		gs = GameState.GetInstance ();
		player = GameObject.Find ("Player");
		if(!gs.ExistsBool("CN6"))
			gs.AddBool ("CN6", true);
		if(!gs.ExistsBool("CN7"))
			gs.AddBool ("CN7", true);
*/
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void TestPreAtion(){
		Debug.Log("PreAction is Launched!!");
	}

	void TestPostAction(){
		Debug.Log("PostAction is Launched!!");
	}

	void EndGame(){
		Application.LoadLevel ("end");
	}


}
