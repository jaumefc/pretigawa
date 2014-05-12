using UnityEngine;
using System.Collections;

//#pragma strict


public enum Custom{
	NAKED,
	JAPANISH,
	MS_FORTUNE
}

public class InventoryControl : MonoBehaviour {
	public GameObject inventoryRootObj;
	public int speed;

	private bool showInventory = false;
	private float position = 0;
	private GameObject selectedObj = null;
	private float MAX_POSITION;
	private readonly float MIN_POSITION = 0;
	private const int MAX_CUSTOMS =7;

	private float width,height;
	private int iconsize;

    public GameObject[] inventoryObjects = new GameObject[11];
    public GameObject[] inventoryCustoms = new GameObject[MAX_CUSTOMS];

	private int currCustomShowed = 0;



	void Start () {
		width = Display.main.systemWidth;
		height = Display.main.systemHeight;
		MAX_POSITION = Mathf.Min( Mathf.FloorToInt(height/8.1f), 132);
		iconsize = Mathf.Min (Mathf.CeilToInt(height*0.118f), 128);

	}

	void Update () {
		float oldPosition = position;
		//L'inventari s'esta desplegant
		if(showInventory) {
			if(position<MAX_POSITION)
				position = position + speed*Time.deltaTime;
			else
				position = MAX_POSITION;
		}
		//L'inventari s'oculta
		else {
			if(position>MIN_POSITION)
				position = position - speed*Time.deltaTime;
			else
				position = MIN_POSITION;
		}
		//Recuperem les textures basiques del inventari, fletxes, background
		GUITexture[] textures = inventoryRootObj.GetComponentsInChildren<GUITexture>();
        //Pintem elements estatics de la barra d'inventari, fletxes, etc
		textures[0].pixelInset = new Rect(textures[0].pixelInset.x,textures[0].pixelInset.y - (position-oldPosition),
		                                  width,MAX_POSITION);


		for(int i =1;i<textures.Length;i++)
		{
			textures[i].pixelInset = new Rect(textures[i].pixelInset.x, ((MAX_POSITION-iconsize)/2)-oldPosition,
			                                  iconsize, iconsize);
		}
		/*
		for(int i =0;i<textures.Length;i++)
		{
			textures[i].pixelInset = new Rect(textures[i].pixelInset.x,textures[i].pixelInset.y - (position-oldPosition),
			                                  textures[i].pixelInset.width,textures[i].pixelInset.height);
		}
		*/

        //Mostrem el objecte que tenim a l'inventari
		int k = 0;
        for (int i = 0; i < inventoryObjects.Length; i++)
        {
            if(inventoryObjects[i]!=null)
                if (selectedObj == null || selectedObj != ((GameObject)inventoryObjects[i]))
                {
                    Rect oldRect = ((GameObject)inventoryObjects[i]).guiTexture.pixelInset;
                    ((GameObject)inventoryObjects[i]).guiTexture.pixelInset =
                        new Rect(-64 * (k + 2), oldRect.y - (position - oldPosition), oldRect.width, oldRect.height);
					k++;
                }
        }

		//Per la disfressa no cal fer res, sempre es mostra una disfresa, aquesta canvia quan es prem el boto de dreta o esquerra

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

		for(int i=0;i<inventoryObjects.Length;i++){
			if(inventoryObjects[i]!=null)
			{
				if(inventoryObjects[i].GetComponent<InventoryObject>().GetState()==InventoryObject.InventoryObjectState.TAKEN){
					Vector3 aPos = new Vector3(((GameObject)inventoryObjects[i]).guiTexture.pixelInset.x+32,
				                           ((GameObject)inventoryObjects[i]).guiTexture.pixelInset.y+32,0);
					float aDist = Vector3.Distance(aPos,cPos);
					if(aDist<32)
					{
						retObj = (GameObject)inventoryObjects[i];
					}
				}
			}
		}



		return retObj;
	}


    public void Add2(GameObject invObj)
    {
        IEnumerator it = inventoryObjects.GetEnumerator();
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

	/*
	 *Metode per afegir una disfressa al inventari 
	 */
	public void AddCustom(Custom c){
		//TODO: Afegir, de forma logica, disfressa al inventari
		//Buscar la disfressa amb el mateix Custom i marcarla com activa
	}

	/*
	 * Metode per mostrar la anterior disfressa disponible
	 */
	public void PreviousCustom(){
		for(int i=1;i<MAX_CUSTOMS;i++){
			int preCustom = (currCustomShowed - i)%MAX_CUSTOMS;
			InventoryCustom preCustomObject = (InventoryCustom)inventoryCustoms[preCustom].GetComponent<InventoryCustom>();
			if(preCustomObject.IsInInventory()){
				inventoryCustoms[currCustomShowed].guiTexture.enabled = false;
				currCustomShowed = preCustom;
				inventoryCustoms[currCustomShowed].guiTexture.enabled = true;
				if(preCustomObject.IsSelected()){
					//TODO:Posar marc que denoti que es el seleccionat
				}
				break;
			}
		}
	}
	
	/*
	 * Metode per mostrar la seguent disfressa disponible
	 */
	public void NextCustom(){
		for(int i=1;i<MAX_CUSTOMS;i++){
			int nextCustom = (currCustomShowed + i)%MAX_CUSTOMS;
			InventoryCustom preCustomObject = (InventoryCustom)inventoryCustoms[nextCustom].GetComponent<InventoryCustom>();
			if(preCustomObject.IsInInventory()){
				inventoryCustoms[currCustomShowed].guiTexture.enabled = false;
				currCustomShowed = nextCustom;
				inventoryCustoms[currCustomShowed].guiTexture.enabled = true;
				if(preCustomObject.IsSelected()){
					//TODO:Posar marc que denoti que es el seleccionat
				}
				break;
			}
		}
	}
	
	/*
	 * Metode per seleccionar la actual disfressa
	 */
	public void SelectCustom(){

	}



}