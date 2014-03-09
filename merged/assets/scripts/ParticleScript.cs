using UnityEngine;
using System.Collections;

public class ParticleScript : MonoBehaviour {

	public float vx, vy, vz;



	// Update is called once per frame
	void Update () {

		transform.Translate(vx, vy, vz, Space.World);
			
	}
}
