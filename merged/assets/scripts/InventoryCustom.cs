using UnityEngine;
using System.Collections;


[RequireComponent(typeof(GUITexture))]
public class InventoryCustom : MonoBehaviour, ISaveable {

    private bool selected;
	public bool taken = false;
	public Custom custom;
    private GameState gs;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Save()
	{
		Debug.Log("Saveing:" + this.gameObject.name);
		gs.SetBool(this.name, selected);
        //throw new System.NotImplementedException();
    }

    public void Load()
	{
		Debug.Log("Loading:" + this.gameObject.name);
		gs = GameState.GetInstance();
		if(!gs.ExistsBool(this.name + ".taken"))
			gs.AddBool(this.name + ".taken", taken);
		taken = gs.GetBool(this.name + ".taken");
        //throw new System.NotImplementedException();
    }

    public bool IsSelected()
    {
        return selected;
    }

	public bool IsInInventory(){
		return taken;
	}
}
