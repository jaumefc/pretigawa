﻿using UnityEngine;
using System.Collections;


[RequireComponent(typeof(GUITexture))]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(interactuable))]
public class InventoryObject : MonoBehaviour, ISaveable {

    private GameState gs;
    public InventoryObjectState state = InventoryObjectState.UNTAKEN;
	public GameObject obj;
	public bool hideOnTake = true;
	//public string varName;

    public enum InventoryObjectState
    {
        UNTAKEN = 0,
        TAKEN = 1,
        USED = 2
    };

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Save()
    {
        Debug.Log("Saveing:" + this.gameObject.name);
		if (!gs.ExistsInt (this.name)) {
			gs.SetInt (this.name, (int)state);
			gs.AddBool ("var_" + this.name, (state == InventoryObjectState.UNTAKEN) ? false : true); 
		}
        //throw new System.NotImplementedException();
    }

    public void Load()
    {
        Debug.Log("Loading:" + this.gameObject.name);
        gs = GameState.GetInstance();
        if (!gs.ExistsInt (this.name)) {
			gs.AddInt (this.name, (int)state);
			gs.AddBool("var_"+this.name,(state==InventoryObjectState.UNTAKEN)?false:true); 
		}
        state = (InventoryObjectState)gs.GetInt(this.name);

		switch(state){
		case InventoryObjectState.TAKEN:
			if(obj!=null)obj.SetActive(false);
			if(gameObject.renderer)gameObject.renderer.enabled = false;
			if(gameObject.collider)gameObject.collider.enabled = false;
			gameObject.guiTexture.enabled = true;
			gameObject.guiTexture.transform.localPosition = new Vector3(1, 1, 1);
			gameObject.guiTexture.transform.localScale = new Vector3(0, 0, 1);
			//gameObject.GetComponent<InventoryObject>().SetState(InventoryObject.InventoryObjectState.TAKEN);
			break;
		case InventoryObjectState.USED:
			if(obj!=null)obj.SetActive(false);
			if(gameObject.renderer)gameObject.renderer.enabled = false;
			if(gameObject.collider)gameObject.collider.enabled = false;
			gameObject.guiTexture.enabled = false;
			break;
		default: break;
		}

        //throw new System.NotImplementedException();
    }

    public InventoryObjectState GetState()
    {
        return state;
    }

    public void SetState(InventoryObjectState state)
    {
		this.state = state;
		gs.SetInt(this.name,(int)state);
		gs.SetBool("var_"+this.name,(state==InventoryObjectState.UNTAKEN)?false:true); 


    }
}
