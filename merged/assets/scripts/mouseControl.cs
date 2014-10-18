using UnityEngine;
using System.Collections;

//#pragma strict

public class mouseControl : MonoBehaviour, ISaveable  {

	public Camera cam ;
	public float minimumDistanceToMove = 1.0f;
	public GameObject onClickCursor;


	private Transform thisTransform;
	private NavMeshAgent navi;
	private Vector3 targetLocation;
	private Quaternion targetRotation;

	private InventoryControl _inventory;
	private interactuable _interactuable;
	public GameObject _raster;
	private GameObject targetObject;
	private GameObject cRaster;
	private GizmoDebug gd;
	private CameraControl CCScript;
	private DialogCameraScript DCScript;

	private GameState gs;
	private GUIText test;
	private GameObject inventoryBack;

	private bool stayStatic = false;

	enum CharacterAction {
		None = 0,
		Take = 1,
		Dialog =2,
		Look = 3,
		Use = 4
	}

	private CharacterAction _characterAction;

	public void characterStatic(bool enabled){
		stayStatic = enabled;
	}

	void Start () {
		// Cache component lookups at startup instead of every frame
		thisTransform = transform;
		cam = Camera.main;
		navi = GetComponent<NavMeshAgent>();
		_inventory = GetComponent<InventoryControl>();
		gd = GameObject.FindObjectOfType<GizmoDebug>().GetComponent<GizmoDebug>();
		CCScript = GameObject.Find("CameraControl").GetComponent<CameraControl>();
		DCScript = GameObject.Find("DialogLayout").GetComponent<DialogCameraScript>();;
		inventoryBack = GameObject.Find("background");
		_characterAction = CharacterAction.None;
        gs = GameState.GetInstance();
        //ameObject.Find("root").BroadcastMessage("Start");
		GameObject.Find("root").BroadcastMessage("Load");
//		thisTransform.Rotate(gs.GetVector3("PlayerRot"));
//		navi.Warp(gs.GetVector3("PlayerPos"));
//		targetLocation = transform.position = gs.GetVector3("PlayerPos");
	}

	void OnEndGame()
	{
		// Don't allow any more control changes when the game ends	
		this.enabled = false;
	}

	void Update () {
		ReadInput();
		// Move towards the target location
		if(stayStatic == false)
			navi.SetDestination(targetLocation);
		ExecuteAction ();
	}

	void ReadInput () {
		if (Input.GetKeyDown(KeyCode.Escape)) //Exit scene
		{
//			gs.SetVector3("PlayerPos",thisTransform.position);
//			gs.SetVector3("PlayerRot",thisTransform.rotation.eulerAngles);
//			gs.SetInt("scene",Application.loadedLevel);
//			gs.GameSave();
            GameObject.Find("root").BroadcastMessage("Save");
			Application.LoadLevel(0);
		}
		if(Input.GetKeyDown(KeyCode.F9)||Input.touchCount==4)//Toggle wired view
			gd.SetWire(!gd.GetWire());
		if(Input.GetKeyDown(KeyCode.F10)||Input.touchCount==3)//Toggle gizmos
			gd.toggle();
		if (Input.GetKeyDown (KeyCode.F11)||Input.touchCount==3)//Toggle FPS
			gd.toogleFPS ();
		if(Input.GetMouseButtonDown(0))//Mouse button down
		{
			MouseButtonDown();
		}
		else if(Input.GetMouseButtonUp(0))//Mouse button up
		{
			MouseButtonUp();
		}
		else if(Input.GetMouseButton(0))//Mouse button is pressed
		{
			MouseButton();
		}
	}

	/*
	 * Metode que executa les accions que s'han ordenat al Player, (Agafar, Conversar, etc)
	 * que requereixen de varies operacions i/o comprovacions
	 */
	private void ExecuteAction(){
		switch(_characterAction)
		{
			//Anar fins a l'objecte, i afegirlo al inventari
		case CharacterAction.Take: 
			if(Vector3.Distance(new Vector3(navi.transform.position.x,0,navi.transform.position.z), new Vector3(targetLocation.x,0,targetLocation.z))<0.5){
				targetLocation = navi.transform.position;
				_inventory.Add2(targetObject);
				targetObject = null;
				_characterAction = CharacterAction.None;
			}
			break;
			//Anar fins al interlocutor, posarnos davant d'ell cara a cara, fer el transfer a la camara de dialeg
			//i llançar el dialeg
		case CharacterAction.Dialog:
			Vector3 agentPos = new Vector3(navi.transform.position.x,0,navi.transform.position.z);
			Debug.Log("AgentPos: "+agentPos+" TargetLocation: "+targetLocation);
			//Si el Player esta 2 unitats davant del seu interlocutor, ha de girar fins estar cara a cara amb ell
			if(Vector3.Distance(agentPos, targetLocation)<0.3){
				targetLocation = agentPos;
				targetRotation = Quaternion.LookRotation(new Vector3(targetObject.transform.position.x-1,0,targetObject.transform.position.z)-targetLocation);
				//Si el Player mira cara a cara el seu interlocutor, es fa el transfer de camera, sino gira fins mirar el seu interlocutor
				Debug.Log("AgentRotation: "+navi.transform.rotation+" TargetRotation: "+targetRotation);
				if(navi.transform.rotation == targetRotation || navi.transform.rotation == new Quaternion(-targetRotation.x,-targetRotation.y,-targetRotation.z,-targetRotation.w)){
					//CCScript.TransferIn(targetObject.GetComponentInChildren<Camera>());
					DCScript.Init();
					targetObject = null;
					_characterAction = CharacterAction.None;
				}
				else{
					navi.transform.rotation = Quaternion.Lerp(navi.transform.rotation, targetRotation,Time.time * 0.002f);
				}
			}
			break;
		}
	}
	
	private void MouseButtonDown(){
		if(cam == null)cam = Camera.main;
		Ray ray = cam.ScreenPointToRay(Input.mousePosition);
		int count = Input.touchCount;	
		if( count == 1 )
		{
			Touch touch = Input.GetTouch(0);
			ray = cam.ScreenPointToRay( new Vector3( touch.position.x, touch.position.y ) );
		}
		
		GameObject invObj=null;
		if(_inventory.Showing()){
			invObj = _inventory.OverObject(new Vector3(Input.mousePosition.x,Input.mousePosition.y,0));
			
			_inventory.SetSelected(invObj);
		}
		
		if(!inventoryBack.guiTexture.HitTest(Input.mousePosition))
		{
			RaycastHit hit;
			if( Physics.Raycast(ray, out hit) && invObj==null )
			{
				if(hit.collider.gameObject == gameObject)
				{
					_inventory.Toggle();
				}
				else if(hit.collider.gameObject.GetComponent<interactuable>())
				{
					//TODO:Desactivar script camera
	//				targetLocation = thisTransform.position;
					cam.GetComponent<customLookAt>().enabled=false;
					_interactuable = hit.collider.gameObject.GetComponent<interactuable>();
					Vector3 screenPos = cam.WorldToScreenPoint(hit.collider.gameObject.transform.position);
					_interactuable.ShowMenu(screenPos);
					_raster.particleSystem.Stop();
					cRaster = (GameObject)GameObject.Instantiate(_raster,hit.collider.gameObject.transform.position,new Quaternion(0,0,0,0));
					cRaster.SetActive(true);
					_inventory.Hide();
				}
				else
				{
					if(invObj==null)
						_inventory.Hide();
					float touchDist = (transform.position - hit.point).magnitude;
					if( touchDist > minimumDistanceToMove )
					{
						targetLocation = hit.point;
					}
					Quaternion quat = Quaternion.AngleAxis(270,new Vector3(1,0,0));
					GameObject.Instantiate(onClickCursor,targetLocation,quat);
				}
			}
		}
	}


	private void MouseButtonUp(){
		//Fem release tenint un objecte del inventari seleccionat
		if(_inventory.Showing()){
			GameObject invObj = _inventory.GetSelected();
			Vector3 mousePos = new Vector3(Input.mousePosition.x,Input.mousePosition.y,0);
			GameObject otherObj = _inventory.OverObject(mousePos);

			if(invObj!=otherObj){
				if(otherObj!=null){//Estem intentant combinar objectes que son dins l'inventari
				}else{//Estem intentant combinar amb un objecte de l'escena
					
					Ray ray = cam.ScreenPointToRay(Input.mousePosition);
					RaycastHit hit;
					if( Physics.Raycast(ray, out hit) ){
						if(hit.collider.gameObject.GetComponent<interactuable>())
						{
							//Cridar a la funcio d combinar objectes
							//Debug.Log ("Desti de combinacio: " + hit.collider.gameObject);
							
							interactuable interDesti = hit.collider.gameObject.GetComponent<interactuable>();
							bool isGood = interDesti.comprobarInteraccio(invObj);
							
							if(isGood){
								//Debug.Log("Combinacio Correcta!");
								//Destroy(interDesti);
								//Application.LoadLevel ("end"); 
							}
						}
						
					}
				}
			}
			if(invObj){
				invObj.transform.position = new Vector3(1,1,0);
				invObj.guiTexture.pixelInset= new Rect(0,-_inventory.iconsize,_inventory.iconsize,_inventory.iconsize);
				invObj.guiTexture.color = new Color(0.5f,0.5f,0.5f,0.5f);
				_inventory.Hide();
				_inventory.SetSelected(null);
			}
		}
		//Fem release tenint un menu contextual desplegat
		if(_interactuable!=null) {
			Vector3 cPos = Input.mousePosition - _interactuable.GetScreenPosition();
			for(int i=0;i<_interactuable.Actions.Length;i++) {
				//Comprovar distancia del puntero al las opciones
//				Vector3 aPos = new Vector3(_interactuable.Actions[i].guiTexture.pixelInset.x+_inventory.iconsize/2,
//				                           _interactuable.Actions[i].guiTexture.pixelInset.y+32,0);
//				float aDist = Vector3.Distance(aPos,cPos);
//				if(aDist<32)
//				{
//					_interactuable.Actions[i].GetComponent<Action>().Do();
//				}
				if(_interactuable.Actions[i].guiTexture.HitTest(Input.mousePosition))
				{
					_interactuable.Actions[i].GetComponent<Action>().Do();
				}
			}
			GameObject.DestroyObject(cRaster);
			_interactuable.HideMenu();
			_interactuable = null;
			cam.GetComponent<customLookAt>().enabled=true;
		}
	}
	
	private void MouseButton(){
		GameObject invObj;
		if(_inventory.Showing()){
			invObj = _inventory.GetSelected();
			if(invObj){
//				invObj.transform.position=new Vector3(Input.mousePosition.x/Screen.width,Input.mousePosition.y/Screen.height,0);
				invObj.guiTexture.pixelInset = new Rect(-invObj.guiTexture.pixelInset.width/2-(Screen.width-Input.mousePosition.x),
				                                        -invObj.guiTexture.pixelInset.height/2-(Screen.height-Input.mousePosition.y),
				                                        invObj.guiTexture.pixelInset.width,
				                                        invObj.guiTexture.pixelInset.height);
				invObj.guiTexture.color = new Color(0.7f,0.7f,0.7f,0.5f);
			}
		}
		if(_interactuable!=null) {
			Ray rray = cam.ScreenPointToRay(Input.mousePosition);
			RaycastHit rhit;
			Physics.Raycast(rray,out rhit);
			
			cRaster.transform.position = new Vector3(rhit.point.x,rhit.point.y +0.5f,rhit.point.z);
			
			Vector3 cPos = Input.mousePosition - _interactuable.GetScreenPosition();
			for(int i=0;i<_interactuable.Actions.Length;i++) {
//				Vector3 aPos = new Vector3(_interactuable.Actions[i].guiTexture.pixelInset.x+32,
//				                           _interactuable.Actions[i].guiTexture.pixelInset.y+32,0);
//				float aDist = Vector3.Distance(aPos,cPos);
//				if(aDist<32)
//				{
//					_interactuable.Actions[i].guiTexture.color = new Color(0.7f,0.7f,0.7f,0.5f);
//				}
//				else
//				{
//					_interactuable.Actions[i].guiTexture.color = new Color(0.5f,0.5f,0.5f,0.5f);
//				}
				if(_interactuable.Actions[i].guiTexture.HitTest(Input.mousePosition))
				{
					_interactuable.Actions[i].guiTexture.color = new Color(0.7f,0.7f,0.7f,0.5f);
				}
				else
				{
					_interactuable.Actions[i].guiTexture.color = new Color(0.5f,0.5f,0.5f,0.5f);
				}
			}
		}
	}
	
	public void GoTo(Vector3 position) {
		targetLocation = position;
	}

	public void Take(GameObject targetObj) {
		targetLocation = targetObj.transform.position;
		targetObject = targetObj;
		_characterAction = CharacterAction.Take;
	}

	public void Dialog(GameObject targetObj) {
		targetLocation = targetObj.transform.position + 2*targetObj.transform.forward;
		targetLocation = new Vector3(targetLocation.x,0,targetLocation.z);
		targetObject = targetObj;
		Debug.Log (targetLocation);
		_characterAction = CharacterAction.Dialog;
	}

	public void Save(){
		Debug.Log ("InitSave::"+this.GetType());
		gs.SetVector3("PlayerPos",thisTransform.position);
		gs.SetVector3("PlayerRot",thisTransform.rotation.eulerAngles);
		gs.SetInt("scene",Application.loadedLevel);
		gs.GameSave();
		Debug.Log ("EndSave::"+this.GetType());
	}

	public void Load(){
		Debug.Log ("Loading:"+this.gameObject.name);
		gs = GameState.GetInstance();
        if (!gs.ExistsVector3("PlayerRot"))
            gs.AddVector3("PlayerRot", new Vector3(0.0f, 0.0f, 0.0f));
        thisTransform.Rotate(gs.GetVector3("PlayerRot"));

        if (!gs.ExistsVector3("PlayerPos"))
            gs.AddVector3("PlayerPos", new Vector3(10.0024f, 3.67965f, 8.96460f));
		navi.Warp(gs.GetVector3("PlayerPos"));
		targetLocation = transform.position = gs.GetVector3("PlayerPos");
        if (!gs.ExistsInt("scene"))
            gs.AddInt("scene", Application.loadedLevel);

		Debug.Log ("EndLoad::"+this.GetType());
	}
}

