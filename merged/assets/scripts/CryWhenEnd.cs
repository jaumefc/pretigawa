using UnityEngine;
using System.Collections;

public class CryWhenEnd : MonoBehaviour {

	public GameObject[] Balloons;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (NoMoreBalloons()==true){
 			audio.Play();
			this.enabled=false;
		}
	
	}

	bool NoMoreBalloons(){
		bool res = true;
		int i = 0;
		while (i<Balloons.Length && res==true){
			if (Balloons[i]!=null){
				res=false;
			}
			i++;
		}
		return res;
	}
}