using UnityEngine;
using System.Collections;

public class endFinalDialog : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void finalDialogEnded(){
		finalscene scriptScene = GameObject.Find ("sceneFinal").GetComponent<finalscene> ();
		scriptScene.dialogEnd ();
	}

}
