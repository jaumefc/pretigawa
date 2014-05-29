using UnityEngine;
using System.Collections;

public class getSuckedAction : UseAction {

	public InventoryCustom ICjapanese;

	public override void Do () {
		ICjapanese.taken = true;
		Destroy(GameObject.Find("japones2"));
		Debug.Log("Got Sucked !");
	}
}
