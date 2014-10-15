using UnityEngine;
using System.Collections;

public class startSniperGameplay : UseAction {

	public GameObject snipergame;

	public override void Do () {
		sniperGameplayController controller = snipergame.GetComponent<sniperGameplayController> ();

		controller.setActive (true);
		GameObject.Find ("SniperSniper").SetActive (false);
		GameObject.Find ("Sniper_02").SetActive (false);
	}
}
