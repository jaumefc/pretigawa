using UnityEngine;
using System.Collections;

public class ConversationScriptTest : MonoBehaviour {


	private GameState gs;
	private GameObject player;
	// Use this for initialization
	void Start () {
		gs = GameState.GetInstance ();
		player = GameObject.Find ("Player");
		if(!gs.ExistsBool("CN6"))
			gs.AddBool ("CN6", true);
		if(!gs.ExistsBool("CN7"))
			gs.AddBool ("CN7", true);
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

	void HabilitarOpcions(){
		gs.SetBool ("CN6", true);
		gs.SetBool ("CN7", true);
	}

	void Inhabilita6(){
		gs.SetBool ("CN6", false);
	}
	
	void Inhabilita7(){
		gs.SetBool ("CN7", false);
	}

	void AnimacioNo(){
		player.GetComponent<animationControl> ().enabled = false;
		player.animation.Play("no",PlayMode.StopAll);
	}

	void ActivaAnimacio(){
		player.GetComponent<animationControl> ().enabled = true;
	}
}
