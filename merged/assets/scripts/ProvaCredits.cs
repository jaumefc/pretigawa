using UnityEngine;
using System.Collections;

/* This script must be modified in case the camera is not placed in the origin*/

public class ProvaCredits : MonoBehaviour {

	float stylebgratio = 1f;

	public float left1, top1, size1;

	public Texture[] text;

	public GUIStyle style;


	public GameObject prefab;

	GameObject credit;
	int currCredit = 0;

	public Texture2D image;

	void Awake() {	
		// Store screen ratio
		stylebgratio=(float)(image.width)/(float)(image.height);
	}

	void Start() {
//		for(int i =0; i < text.Length; i++){
//			Vector3 pos = new Vector3(prefab.transform.position.x,prefab.transform.position.y-i*3,prefab.transform.position.z);
//			credit = (GameObject)GameObject.Instantiate(prefab, pos, prefab.transform.rotation);
//			credit.GetComponentInChildren<TextMesh>().text = text[i];
//
//		}

		if (text.Length > 0)
						ShowNextCredit ();
	}

	void Update() {
		if(credit!=null)
		{
//			Debug.Log(credit.transform.FindChild("Cube").position.y);
			if(credit.transform.FindChild("Cube").position.y < -3 || credit.transform.FindChild("Cube").position.y > 5)
			{
				GameObject.Destroy(credit);
				if(currCredit < text.Length)
					ShowNextCredit();
			}
		}
	}

	void ShowNextCredit(){
		credit = (GameObject)GameObject.Instantiate (prefab);
	//	credit.GetComponentInChildren<TextMesh>().text = text[currCredit];
		Renderer boardRend = credit.transform.FindChild("Cube").GetComponent<Renderer>();
//		Material mat = new Material(Shader.Find("Decal"));
		//mat.CopyPropertiesFromMaterial(boardRend.material);
//		mat.SetTexture ("_DecalTex", text [currCredit]);
//		boardRend.material = mat;
		if(boardRend.material.HasProperty("_DecalTex"))
		{
			boardRend.material.SetTexture("_DecalTex",text[currCredit]);
		}
		currCredit++;

	}

	void OnGUI() {


		GUI.skin.button.normal.background = (Texture2D) image;
		GUI.skin.button.hover.background = (Texture2D) image;
		GUI.skin.button.active.background = (Texture2D) image;
		if (GUI.Button (new Rect (left1 * Screen.width*stylebgratio, top1 * Screen.height, size1 * Screen.height * stylebgratio, size1 * Screen.height), image, style)) {
			Debug.Log ("Back");
			Application.LoadLevel("menu");
		}

		if (Input.GetKeyDown (KeyCode.Escape)) {
			Debug.Log ("Back");
			Application.LoadLevel("menu");
		}
	}


}