using UnityEngine;
using System.Collections;

public class particulesCaiguda : MonoBehaviour {

	GameObject player;
	public ParticleSystem efecteCaiguda;
	
	void Start () {
		player = GameObject.Find ("Player");
	}
	
	void Update () {		
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other == player.collider)
		{
			efecteCaiguda.Play();
			Invoke("autoDestroy", 2.0f);
		}
	}

	private void autoDestroy(){
		Destroy(GameObject.Find("EfecteTerra"));
	}
}
