using UnityEngine;
using System.Collections;

//#pragma strict


public enum Custom{
	NAKED,
	JAPANESE,
	MS_FORTUNE,
	POLICEMAN
}

public class InventoryControl : MonoBehaviour {
	public GameObject inventoryRootObj;
	public int speed;

	private bool showInventory = false;
	private float position = 0;
	private GameObject selectedObj = null;
	private float MAX_POSITION;
	private readonly float MIN_POSITION = 0;
	private const int MAX_CUSTOMS =4;

	private float width,height;
	public int iconsize;

    public GameObject[] inventoryObjects = new GameObject[13];
    public GameObject[] inventoryCustoms = new GameObject[MAX_CUSTOMS];

	private int currCostumeShowed = 0;
	private int costumeSelected = 0;

	private GameObject btnCLeft, btnCRight, btnILeft, btnIRight;
	private GameObject playerMesh;



	void Start () {
		width = Display.main.systemWidth;
		height = Display.main.systemHeight;
		MAX_POSITION = Mathf.Min( Mathf.FloorToInt(height/8.1f), 132);
		iconsize = Mathf.Min (Mathf.CeilToInt(height*0.118f), 128);

		btnCLeft = GameObject.Find("cleft");
		btnCRight = GameObject.Find("cright");
		btnILeft = GameObject.Find("ileft");
		btnIRight = GameObject.Find("iright");
		playerMesh = GameObject.Find ("greenMesh");

		btnCRight.guiTexture.pixelInset = new Rect ((10+iconsize) * 2, 0, iconsize, iconsize);
		btnILeft.guiTexture.pixelInset = new Rect (20+iconsize * 3, 0, iconsize, iconsize);
		btnIRight.guiTexture.pixelInset = new Rect (-iconsize, 0, iconsize, iconsize);

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
		for(int i =0;i<inventoryCustoms.Length;i++)
		{
			if(inventoryCustoms[i])
				inventoryCustoms[i].guiTexture.pixelInset = new Rect(10+iconsize, ((MAX_POSITION-iconsize)/2)-oldPosition,
			                                  iconsize, iconsize);
		}

        //Mostrem el objecte que tenim a l'inventari
		int k = 0;
        for (int i = 0; i < inventoryObjects.Length; i++)
        {
            if(inventoryObjects[i]!=null)
				if (inventoryObjects[i].GetComponent<InventoryObject>().state == InventoryObject.InventoryObjectState.TAKEN)
                {
					if(!((GameObject)inventoryObjects[i]).Equals(selectedObj)){
	                    Rect oldRect = ((GameObject)inventoryObjects[i]).guiTexture.pixelInset;
	                    ((GameObject)inventoryObjects[i]).guiTexture.pixelInset =
	                        new Rect(-iconsize * (k + 2), oldRect.y - (position - oldPosition), iconsize, iconsize);
					}
					k++;
                }
        }

		//Per la disfressa no cal fer res, sempre es mostra una disfresa, aquesta canvia quan es prem el boto de dreta o esquerra
		if(Input.GetMouseButtonDown(0)){
			if(btnCLeft.guiTexture.HitTest(Input.mousePosition))
				PreviousCostume();
			if(btnCRight.guiTexture.HitTest(Input.mousePosition))
				NextCostume();
			if(inventoryCustoms[currCostumeShowed].guiTexture.HitTest(Input.mousePosition))
				SelectCostume();
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

		for(int i=0;i<inventoryObjects.Length;i++){
			if(inventoryObjects[i]!=null && (inventoryObjects[i] != selectedObj))
			{
				if(inventoryObjects[i].GetComponent<InventoryObject>().GetState()==InventoryObject.InventoryObjectState.TAKEN){
					if(inventoryObjects[i].guiTexture.HitTest(Input.mousePosition))
						retObj = (GameObject)inventoryObjects[i];
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
                if(obj.renderer)obj.renderer.enabled = false;
                if(obj.collider)obj.collider.enabled = false;
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
	public void AddCostume(Custom c){
		//TODO: Afegir, de forma logica, disfressa al inventari
		//Buscar la disfressa amb el mateix Custom i marcarla com activa
	}

	/*
	 * Metode per mostrar la anterior disfressa disponible
	 */
	private void PreviousCostume(){
		for(int i=1;i<MAX_CUSTOMS;i++){
			int preCustom = Mathf.Abs((currCostumeShowed - i + MAX_CUSTOMS)%MAX_CUSTOMS);
			if(inventoryCustoms[preCustom])
			{
				InventoryCustom preCustomObject = (InventoryCustom)inventoryCustoms[preCustom].GetComponent<InventoryCustom>();
				if(preCustomObject.IsInInventory()){
					inventoryCustoms[currCostumeShowed].guiTexture.enabled = false;
					currCostumeShowed = preCustom;
					inventoryCustoms[currCostumeShowed].guiTexture.enabled = true;
					if(currCostumeShowed == costumeSelected){
						//TODO:Posar marc que denoti que es el seleccionat
					}
					else{
						//TODO:Treure marc
					}
					break;
				}
			}
		}
	}
	
	/*
	 * Metode per mostrar la seguent disfressa disponible
	 */
	private void NextCostume(){
		for(int i=1;i<MAX_CUSTOMS;i++){
			int nextCustom = (currCostumeShowed + i)%MAX_CUSTOMS;
			if(inventoryCustoms[nextCustom])
			{
				InventoryCustom preCustomObject = (InventoryCustom)inventoryCustoms[nextCustom].GetComponent<InventoryCustom>();
				if(preCustomObject.IsInInventory()){
					inventoryCustoms[currCostumeShowed].guiTexture.enabled = false;
					currCostumeShowed = nextCustom;
					inventoryCustoms[currCostumeShowed].guiTexture.enabled = true;
					if(currCostumeShowed == costumeSelected){
						//TODO:Posar marc que denoti que es el seleccionat
					}
					else{
						//TODO:Treure marc
					}
					break;
				}
			}
		}
	}
	
	/*
	 * Metode per seleccionar la actual disfressa
	 */
	private void SelectCostume(){

		if (currCostumeShowed != costumeSelected) {
			ParticleSystem pd = transform.Find("PartsDisfressa").GetComponent<ParticleSystem>();
			pd.Play();
			Invoke("changeCostume", 1.0f);
		}

		costumeSelected = currCostumeShowed;
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemies");

		switch (GetCurrentCostume ()) 
		{
		case Custom.JAPANESE:
//			playerMesh.renderer.material.color = Color.yellow;
//			playerMesh.renderer.material.mainTexture = inventoryCustoms[costumeSelected].GetComponent<InventoryCustom>().texCostume;
			foreach(GameObject enemy in enemies){
				controlVisio cv = enemy.GetComponent<controlVisio>();
				cv.iaOff();
			}
			break;
		case Custom.NAKED:
//			float rgb=150f/256f;
//			playerMesh.renderer.material.color = new Color(rgb,rgb,rgb,1);
			foreach(GameObject enemy in enemies){
				controlVisio cv = enemy.GetComponent<controlVisio>();
				cv.iaOn();
			}
			break; 
		}
	}

	private void changeCostume(){
		playerMesh.renderer.material.mainTexture = inventoryCustoms[costumeSelected].GetComponent<InventoryCustom>().texCostume;
	}

	public Custom GetCurrentCostume(){
		return inventoryCustoms[costumeSelected].GetComponent<InventoryCustom>().custom;
	}



}