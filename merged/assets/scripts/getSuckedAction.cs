using UnityEngine;
using System.Collections;

public class getSuckedAction : UseAction {

	public InventoryCustom ICustom;
	public GameObject realTarget;
	public ConversationTreeClass AfterTree;
	public GameObject addObject;

	public override void Do () {

		//realTarget = transform.parent.parent.gameObject;
		GameObject absorcio = Instantiate (GameObject.Find ("part_absorcio_container"), realTarget.transform.position, realTarget.transform.rotation) as GameObject;
		absorcioEffect aE = absorcio.GetComponent<absorcioEffect> ();
		aE.tryStartAbsorcio(realTarget);
		if (AfterTree) aE.assigntree (AfterTree);
		ICustom.taken = true;
		if (ICustom.custom == Custom.JAPANESE) {
			Debug.Log("Agafo la camera!!!!!!");
//						InventoryControl _inventory = (InventoryControl)GameObject.Find("Player").GetComponent<InventoryControl>();
//			_inventory.Add2(addObject);
			((InventoryObject)(addObject.GetComponent<InventoryObject>())).state = InventoryObject.InventoryObjectState.TAKEN;
		}
	}
}
