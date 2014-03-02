using UnityEngine;
using System.Collections;

//#pragma strict

public class InventoryControl : MonoBehaviour {
	public GameObject inventoryRootObj;
	public int speed;

	private bool showInventory = false;
	private float position = 0;
	private readonly float MAX_POSITION = 100;
	private readonly float MIN_POSITION = 0;

	private ArrayList inventoryObjects = new ArrayList();


	void Start () {

	}

	void Update () {
		float oldPosition = position;
		if(showInventory) {
			if(position<MAX_POSITION)
				position = position + speed*Time.deltaTime;
			else
				position = MAX_POSITION;
		}
		else {
			if(position>MIN_POSITION)
				position = position - speed*Time.deltaTime;
			else
				position = MIN_POSITION;
		}
		GUITexture[] textures = inventoryRootObj.GetComponentsInChildren<GUITexture>();

		for(int i =0;i<textures.Length;i++)
		{
			textures[i].pixelInset = new Rect(textures[i].pixelInset.x,textures[i].pixelInset.y - (position-oldPosition),
			                                  textures[i].pixelInset.width,textures[i].pixelInset.height);
		}

		for(int i=0;i<inventoryObjects.Count;i++){
			Rect oldRect = ((GameObject)inventoryObjects[i]).guiTexture.pixelInset;
			((GameObject)inventoryObjects[i]).guiTexture.pixelInset =
				new Rect(-64*(i+2),oldRect.y - (position-oldPosition),oldRect.width,oldRect.height);
			Debug.Log(i);
			Debug.Log(-64*(i+1));
		}
	}


	public void Toggle() {
		showInventory = !showInventory;
	}


	public void Hide() {
		showInventory = false;
	}

	public void Add(GameObject invObj) {
		invObj.renderer.enabled=false;
		invObj.collider.enabled=false;
		invObj.guiTexture.enabled=true;
		invObj.guiTexture.transform.position = new Vector3(1,1,0);
		invObj.guiTexture.transform.localScale = new Vector3(0,0,1);
		inventoryObjects.Add(invObj);
	}

}