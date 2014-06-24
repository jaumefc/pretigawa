using UnityEngine;
using System.Collections;

public class getSuckedAction : UseAction {

	public InventoryCustom ICustom;
	public string sDestroy;

	public override void Do () {
		ICustom.taken = true;
		Destroy(GameObject.Find(sDestroy));
		Debug.Log("Got Sucked !");
	}
}
