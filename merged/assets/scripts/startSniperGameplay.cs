using UnityEngine;
using System.Collections;

public class startSniperGameplay : UseAction {

	public GameObject snipergame;

	public override void Do () {
		sniperGameplayController controller = snipergame.GetComponent<sniperGameplayController> ();

		controller.setActive (true);
	}
}
