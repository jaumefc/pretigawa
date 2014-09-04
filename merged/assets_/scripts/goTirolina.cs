using UnityEngine;
using System.Collections;

public class goTirolina : UseAction {

	public GameObject mainChar;
	public ParticleSystem teleportParticles;

	public void Start(){
		mainChar = GameObject.Find ("Player");
	}

	public override void Do () {
		teleportParticles.Play();
		Invoke("teleportAmunt", 0.75f);
	}

	private void teleportAmunt() {
		DontGoThroughThings dgt = mainChar.GetComponent ("DontGoThroughThings") as DontGoThroughThings;
		dgt.checkThings = false;

		NavMeshAgent NA = mainChar.GetComponent ("NavMeshAgent") as NavMeshAgent;
		NA.enabled = false;
		mainChar.transform.position = new Vector3 (-2.08499f, 3.679167f, -7.653803f);
		NA.enabled = true;

		dgt.resetStats ();
		dgt.checkThings = true;
	}
}
