using UnityEngine;
using System.Collections;

public class aliensNotAllowedScript : MonoBehaviour {

	GameObject cartell;

	// Use this for initialization
	void Start () {
		cartell = GameObject.Find ("noAliensAllowedCartell");
	}
	
	// Update is called once per frame
	void Update () {
	
	}	



	public void disableNoAlliensAllowedCartell(){
		Invoke ("disable", 2f);
	}


	void disable(){
		cartell.SetActive (false);
	}
}
