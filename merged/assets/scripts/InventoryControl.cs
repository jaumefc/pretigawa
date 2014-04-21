using UnityEngine;
using System.Collections;

//#pragma strict

public class InventoryControl : MonoBehaviour {
	public GameObject inventoryRootObj;
	public int speed;

	private bool showInventory = false;
	private float position = 0;
	private GameObject selectedObj = null;
	private readonly float MAX_POSITION = 100;
	private readonly float MIN_POSITION = 0;

	private float width,height;

	private ArrayList inventoryObjects = new ArrayList();
    public GameObject[] inventoryObjects1 = new GameObject[11];
    public GameObject[] inventoryCustoms = new GameObject[3];


	void Start () {
		width = Display.main.systemWidth;
		height = Display.main.systemHeight;

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
        //Pintem elements estatics de la barra d'inventari, fletxes, etc
		for(int i =0;i<textures.Length;i++)
		{
			textures[i].pixelInset = new Rect(textures[i].pixelInset.x,textures[i].pixelInset.y - (position-oldPosition),
			                                  textures[i].pixelInset.width,textures[i].pixelInset.height);
		}
        //Mostrem el objecte que tenim a l'inventari
        //for(int i=0;i<inventoryObjects.Count;i++){
        //    if(selectedObj==null || selectedObj!=((GameObject)inventoryObjects[i])){
        //        Rect oldRect = ((GameObject)inventoryObjects[i]).guiTexture.pixelInset;
        //        ((GameObject)inventoryObjects[i]).guiTexture.pixelInset =
        //            new Rect(-64*(i+2),oldRect.y - (position-oldPosition),oldRect.width,oldRect.height);
        //        //Debug.Log(i);
        //        //Debug.Log(-64*(i+1));
        //    }
        //}

        //Mostrem el objecte que tenim a l'inventari
		int k = 0;
        for (int i = 0; i < inventoryObjects1.Length; i++)
        {
            if(inventoryObjects1[i]!=null)
                if (selectedObj == null || selectedObj != ((GameObject)inventoryObjects1[i]))
                {
                    Rect oldRect = ((GameObject)inventoryObjects1[i]).guiTexture.pixelInset;
                    ((GameObject)inventoryObjects1[i]).guiTexture.pixelInset =
                        new Rect(-64 * (k + 2), oldRect.y - (position - oldPosition), oldRect.width, oldRect.height);
					k++;
                }
        }
	}


	public void Toggle() {
		showInventory = !showInventory;
	}


	public void Hide() {
		showInventory = false;
	}

	public bool Showing() {
		return showInventory;
	}

	public GameObject GetSelected(){
		return selectedObj;
	}

	public void SetSelected(GameObject obj){
		selectedObj = obj;
	}

	//Retorna l'objecte de l'inventari que es troba sota el punter si n'i ha, sino retorna null
	//mousePos :Posicio del punter
	public GameObject OverObject(Vector3 mousePos){
		GameObject retObj = null;



		Vector3 cPos = Input.mousePosition - new Vector3(width,height,0);
//		for(int i=0;i<inventoryObjects.Count;i++) {
//			Vector3 aPos = new Vector3(((GameObject)inventoryObjects[i]).guiTexture.pixelInset.x+32,
//			                           ((GameObject)inventoryObjects[i]).guiTexture.pixelInset.y+32,0);
//			float aDist = Vector3.Distance(aPos,cPos);
//			if(aDist<32)
//			{
//				retObj = (GameObject)inventoryObjects[i];
//			}
//		}

		for(int i=0;i<inventoryObjects1.Length;i++){
			if(inventoryObjects1[i]!=null)
			{
				if(inventoryObjects1[i].GetComponent<InventoryObject>().GetState()==InventoryObject.InventoryObjectState.TAKEN){
					Vector3 aPos = new Vector3(((GameObject)inventoryObjects1[i]).guiTexture.pixelInset.x+32,
				                           ((GameObject)inventoryObjects1[i]).guiTexture.pixelInset.y+32,0);
					float aDist = Vector3.Distance(aPos,cPos);
					if(aDist<32)
					{
						retObj = (GameObject)inventoryObjects1[i];
					}
				}
			}
		}



		return retObj;
	}



//	public void Add(GameObject invObj) {
//		invObj.renderer.enabled=false;
//		invObj.collider.enabled=false;
//		invObj.guiTexture.enabled=true;
//		invObj.guiTexture.transform.position = new Vector3(1,1,0);
//		invObj.guiTexture.transform.localScale = new Vector3(0,0,1);
//		inventoryObjects.Add(invObj);
//	}

    public void Add2(GameObject invObj)
    {
        IEnumerator it = inventoryObjects1.GetEnumerator();
        while(it.MoveNext()){
            GameObject obj = (GameObject)it.Current;
            if (obj.Equals(invObj))
            {
                obj.renderer.enabled = false;
                obj.collider.enabled = false;
                obj.guiTexture.enabled = true;
                obj.guiTexture.transform.localPosition = new Vector3(1, 1, 1);
                obj.guiTexture.transform.localScale = new Vector3(0, 0, 1);
                obj.GetComponent<InventoryObject>().SetState(InventoryObject.InventoryObjectState.TAKEN);
            }
        }

    }



}