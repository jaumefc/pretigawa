using UnityEngine;
using System.Collections;

public class particleChange : MonoBehaviour {

	public ParticleSystem ps;

	private Object[] materials;

	void Start () {
		materials = Resources.LoadAll ("Materials/ParticulesRoba");
		canviaParticula ();
	}

	void Update () {
	
	}

	private void canviaParticula(){
		int rnd = Random.Range (0, 10);
		ps.renderer.material = materials[rnd] as Material;

		Invoke ("canviaParticula", 2.0f);
	}
}
