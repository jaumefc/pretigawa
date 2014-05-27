using UnityEngine;
using System.Collections;

public class goUpstairs : UseAction {

	public GameObject mainChar;
	public ParticleSystem teleportParticles;

	public override void Do () {
		teleportParticles.Play();
		Invoke("teleportAmunt", 0.75f);
	}

	private void teleportAmunt() {
		NavMeshAgent NA = mainChar.GetComponent ("NavMeshAgent") as NavMeshAgent;
		NA.enabled = false;
		mainChar.transform.position = new Vector3 (-7.440165f, 16.49909f, -11.75213f);
		NA.enabled = true;
	}
}
