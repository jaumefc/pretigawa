using UnityEngine;
using System.Collections;

public class CryWhenEnd : MonoBehaviour {

	public GameObject[] Balloons;
	public Texture crying;
	public GameObject nen;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (NoMoreBalloons()==true){
 			audio.Play();
			nen.gameObject.renderer.material.SetTexture ("_MainTex", crying);
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