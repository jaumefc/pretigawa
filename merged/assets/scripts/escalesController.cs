using UnityEngine;
using System.Collections;

public class escalesController : MonoBehaviour {

	private GameObject mainChar; 
	public ParticleSystem teleportParticles;
	
	private bool hasToTp = false;

	void Start () {
		mainChar = GameObject.Find ("Player");
	}

	public void startTp(){
		Debug.Log ("starting tp");

		hasToTp = true;
	}

	public void stopTp(){
		hasToTp = false;
	}

	void Update () {
		if (hasToTp == false) return;

		Vector3 vectorMoviment = (GameObject.Find("escales").transform.position - mainChar.transform.position);
		if (vectorMoviment.magnitude < 3) {
			Debug.Log("comencant tp");
			mainChar.GetComponent<mouseControl> ().GoTo (mainChar.transform.position);
			hasToTp = false;
			teleportParticles.Play();
			Invoke("teleportAmunt", 0.75f);
		}
		else {
			Debug.Log("anant a escales");
			mainChar.GetComponent<mouseControl> ().GoTo (GameObject.Find("escales").transform.position);
		}
	}

	private void teleportAmunt() {
		DontGoThroughThings dgt = mainChar.GetComponent ("DontGoThroughThings") as DontGoThroughThings;
		dgt.checkThings = false;
		
		NavMeshAgent NA = mainChar.GetComponent ("NavMeshAgent") as NavMeshAgent;
		NA.enabled = false;
		mainChar.transform.position = new Vector3 (-7.440165f, 16.49909f, -11.75213f);
		NA.enabled = true;
		
		dgt.resetStats ();
		dgt.checkThings = true;
	}
}
