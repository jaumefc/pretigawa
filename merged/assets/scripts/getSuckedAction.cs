using UnityEngine;
using System.Collections;

public class getSuckedAction : UseAction {

	public InventoryCustom ICustom;
	public string sDestroy;
	private GameObject realTarget;

	public override void Do () {

		realTarget = transform.parent.parent.gameObject;
		GameObject absorcio = Instantiate (GameObject.Find ("part_absorcio_container"), realTarget.transform.position, realTarget.transform.rotation) as GameObject;
		absorcioEffect aE = absorcio.GetComponent<absorcioEffect> ();
		aE.tryStartAbsorcio(realTarget);
		ICustom.taken = true;
	}
}
