using UnityEngine;
using System.Collections;

public class goUpstairs : UseAction {

	public escalesController escalesController;

	void Start(){
	}

	void Update(){

	}

	public override void Do () {
		escalesController.startTp ();
	}


}
