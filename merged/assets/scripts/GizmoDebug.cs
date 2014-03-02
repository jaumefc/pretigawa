using UnityEngine;
using System.Collections;

public class GizmoDebug : MonoBehaviour {


	private bool showAll = false;
	private bool showWired = false;
	private FPSCounter fpsScript;
	// Use this for initialization

	void Start () {
		fpsScript = gameObject.GetComponent<FPSCounter> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnDrawGizmos () {
//		if(showAll) {
//			//Show player
//			GameObject player = GameObject.Find("Player");
//			Gizmos.color = Color.green;
//			Gizmos.DrawWireCube(player.collider.bounds.center,player.collider.bounds.size); 
//
//			//Show trigger cam zone
//			Gizmos.color = Color.yellow;
//			GameObject[] triggers = GameObject.FindGameObjectsWithTag("CameraZone");
//			for (int i=0;i<triggers.Length; i++)
//			{
//				//Gizmos.matrix = triggers[i].collider.transform.worldToLocalMatrix;
//				Gizmos.DrawWireCube(triggers[i].collider.bounds.center,triggers[i].collider.bounds.size);
//				//Graphics.DrawMesh(((MeshFilter)triggers[i].GetComponent("MeshFilter")).mesh,triggers[i].transform.position,Quaternion.identity,triggers[i].renderer.material,0);
//				Graphics.DrawMeshNow(((MeshFilter)triggers[i].GetComponent("MeshFilter")).mesh,triggers[i].transform.position,Quaternion.identity);
//
//			}
//				
//
//			//Show interactuable objects
//			Gizmos.color = Color.blue;
//			interactuable[] interactObjs = GameObject.FindObjectsOfType<interactuable>();
//			for (int i=0;i<interactObjs.Length; i++)
//				Gizmos.DrawWireCube(interactObjs[i].collider.bounds.center,interactObjs[i].collider.bounds.size);
//
//			//Other objects
//		}

	}

	public void toggle () {
		showAll =!showAll;
	}

	public void SetWire(bool wired){
		showWired = wired;
	}
	public bool GetWire(){
		return showWired;
	}

	public bool GetGizmos() {
		return showAll;
	}

	public void toogleFPS(){
		fpsScript.enabled = !fpsScript.enabled;
		gameObject.guiText.enabled = !gameObject.guiText.enabled;
	}
}
