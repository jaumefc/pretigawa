using UnityEngine;
using System.Collections;

public class RandomPersonatge : MonoBehaviour {

	public GameObject[] personatges;
	public GameObject[] malotes;

	void Start () {
		for (int i=0; i<personatges.Length; i++) {
			bool bueno_malo = (Random.Range(0, 4)==0)?true:false;
			personatges[i].SetActive(bueno_malo);
			malotes[i].SetActive(!bueno_malo);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
