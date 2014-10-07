using UnityEngine;
using System.Collections;

public class sagradaFamiliaCrash : MonoBehaviour {


	GameObject player;
	public Animation anim;

	// Use this for initialization
	void Start () {
	
		player = GameObject.Find ("Player");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other)
	{
		if(other == player.collider)
		{
			anim.animation.Play();
			this.enabled = false;
		}
	}
}
