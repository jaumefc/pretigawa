using UnityEngine;
using System.Collections;

//#pragma strict

public class mouseControl : MonoBehaviour {

	public Camera cam ;
	public Transform cameraPivot;
	public float speed;
	public float minimumDistanceToMove = 1.0f;
	public float minimumTimeUntilMove = 0.25f;
	public bool zoomEnabled;
	public float zoomEpsilon;
	public float zoomRate;
	public float rotateEnabled;
	public float rotateEpsilon = 1; // in degrees
	public GameObject onClickCursor;


	public AnimationClip idleAnimation;
	public AnimationClip walkAnimation;
	public AnimationClip runAnimation;
	public AnimationClip jumpPoseAnimation;

	private Transform thisTransform;
	private CharacterController character;
	private NavMeshAgent navi;
	private Vector3 targetLocation;
	private Quaternion targetRotation;
	private bool moving = false;
	private float rotationTarget;
	private float rotationVelocity;
	private Vector3 velocity;

	private InventoryControl _inventory;
	private Animation _animation;
	private interactuable _interactuable;
	public GameObject _raster;
	private GameObject cRaster;
	private GameObject targetObject;
	private GizmoDebug gd;
	private CameraControl CCScript;

	private GameState gs;

	enum CharacterState {
		Idle = 0,
		Walking = 1,
		Trotting = 2,
		Running = 3,
		Jumping = 4,
	}

	enum CharacterAction {
		None = 0,
		Take = 1,
		Dialog =2,
		Look = 3,
		Use = 4
	}

	CharacterState _characterState;
	CharacterAction _characterAction = CharacterAction.None;


	void Start () {
		// Cache component lookups at startup instead of every frame
		thisTransform = transform;
		cam = Camera.main;
		character = GetComponent<CharacterController>();
		navi = GetComponent<NavMeshAgent>();
		_animation = GetComponent<Animation>();
		_inventory = GetComponent<InventoryControl>();
		gd = GameObject.FindObjectOfType<GizmoDebug>().GetComponent<GizmoDebug>();
		CCScript = GameObject.Find("CameraControl").GetComponent<CameraControl>();
		gs = GameState.GetInstance();
		thisTransform.Rotate(gs.GetVector3("PlayerRot"));
		targetLocation = thisTransform.position = gs.GetVector3("PlayerPos");

//		Vector3 prot = gs.GetVector3("PlayerRot");
//		thisTransform.rotation.eulerAngles.Set(prot.x,prot.y,prot.z);


	}

	void OnEndGame()
	{
		// Don't allow any more control changes when the game ends	
		this.enabled = false;
	}

	void Update () {
		ReadInput();
		// ANIMATION sector
		if(_animation) {
			switch(_characterState){
			case CharacterState.Idle:
				_animation.CrossFade(idleAnimation.name);
				break;
			case CharacterState.Walking:
				_animation[walkAnimation.name].speed = Mathf.Clamp(navi.velocity.magnitude, 0.0f, 1.8f);
				_animation.CrossFade(walkAnimation.name);	
				break;
			case CharacterState.Running:
				_animation[runAnimation.name].speed = Mathf.Clamp(navi.velocity.magnitude, 0.0f, 1.0f);
				_animation.CrossFade(runAnimation.name);	
				break;
			}
		}
		// ANIMATION sector
	}

	void ReadInput () {
		if (Input.GetKeyDown(KeyCode.Escape)) //Exit scene
		{
			gs.SetVector3("PlayerPos",thisTransform.position);
			gs.SetVector3("PlayerRot",thisTransform.rotation.eulerAngles);
			gs.SetInt("scene",Application.loadedLevel);
			gs.GameSave();
			Application.LoadLevel(0);
		}
		if(Input.GetKeyDown(KeyCode.F9)||Input.touchCount==4)//Toggle wired view
			gd.SetWire(!gd.GetWire());
		if(Input.GetKeyDown(KeyCode.F10)||Input.touchCount==3)//Toggle gizmos
			gd.toggle();
		if (Input.GetKeyDown (KeyCode.F11)||Input.touchCount==3)
			gd.toogleFPS ();
		if(Input.GetMouseButtonDown(0))
		{
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

			RaycastHit hit;
			if( Physics.Raycast(ray, out hit) && invObj==null )
			{
				Debug.Log(hit.collider.gameObject);
				Debug.Log(hit.collider.gameObject.GetComponent<interactuable>());
				if(hit.collider.gameObject == gameObject)
				{
					_inventory.Toggle();
				}
				else if(hit.collider.gameObject.GetComponent<interactuable>())
				{
					Debug.Log("Interactuable collide!!!");
					_interactuable = hit.collider.gameObject.GetComponent<interactuable>();
					Vector3 screenPos = cam.WorldToScreenPoint(hit.collider.gameObject.transform.position);
					_interactuable.ShowMenu(screenPos);
					_raster.particleSystem.Stop();
					cRaster = (GameObject)GameObject.Instantiate(_raster,hit.collider.gameObject.transform.position,new Quaternion(0,0,0,0));
					cRaster.SetActive(true);
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
					GameObject curTarget = (GameObject)GameObject.Instantiate(onClickCursor,targetLocation,quat);
					moving = true;
				}
			}
		}
		else if(Input.GetMouseButtonUp(0))
		{
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
								Debug.Log ("Desti de combinacio: " + hit.collider.gameObject);

								interactuable interDesti = hit.collider.gameObject.GetComponent<interactuable>();
								bool isGood = interDesti.comprobarInteraccio(invObj);

								if(isGood){
									Debug.Log("Combinacio Correcta!");
									Application.LoadLevel ("prova end"); 
								}
							}

						}
					}
				}
				if(invObj){
					invObj.transform.position = new Vector3(1,1,0);
					invObj.guiTexture.pixelInset= new Rect(0,18-100,64,64);
					invObj.guiTexture.color = new Color(0.5f,0.5f,0.5f,0.5f);
					_inventory.Hide();
					_inventory.SetSelected(null);
				}
			}
			if(_interactuable!=null) {
				float cx= Input.mousePosition.x;
				float cy= Input.mousePosition.y;
				Vector3 cPos = Input.mousePosition - _interactuable.GetScreenPosition();
				for(int i=0;i<_interactuable.Actions.Length;i++) {
					//Comprovar distancia del puntero al las opciones
					Vector3 aPos = new Vector3(_interactuable.Actions[i].guiTexture.pixelInset.x+32,
												_interactuable.Actions[i].guiTexture.pixelInset.y+32,0);
					 float aDist = Vector3.Distance(aPos,cPos);
					//Debug.Log("Distance:"+aDist);
					if(aDist<32)
					{
						_interactuable.Actions[i].GetComponent<Action>().Do();
					}
				} 
				
				GameObject.DestroyObject(cRaster);
				_interactuable.HideMenu();
				_interactuable = null;
			}
			//Debug.Log("ButtonUp");
			
		}
		else if(Input.GetMouseButton(0))
		{
			GameObject invObj;
			if(_inventory.Showing()){
				invObj = _inventory.GetSelected();
				if(invObj){
					invObj.transform.position=new Vector3(Input.mousePosition.x/Display.main.systemWidth,Input.mousePosition.y/Display.main.systemHeight,0);
					invObj.guiTexture.pixelInset = new Rect(-invObj.guiTexture.pixelInset.width/2,
					                                        -invObj.guiTexture.pixelInset.height/2,
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
					Vector3 aPos = new Vector3(_interactuable.Actions[i].guiTexture.pixelInset.x+32,
												_interactuable.Actions[i].guiTexture.pixelInset.y+32,0);
					float aDist = Vector3.Distance(aPos,cPos);
					if(aDist<32)
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
		
		
		
		
		
		Vector3 movement = Vector3.zero;
				// Move towards the target location
		navi.SetDestination(targetLocation);
		Vector3 nextstep = navi.nextPosition;
		movement = nextstep - thisTransform.position;
		movement.y=0;
		
		float dist = movement.magnitude;
		//Debug.Log (dist);
		if( navi.velocity.sqrMagnitude < 0.1 )
		{
			moving = false;
			_characterState = CharacterState.Idle;
		}
		else
		{
			movement = movement.normalized * speed;
			_characterState = CharacterState.Walking;
		}
		
		if(!movement.normalized.Equals(Vector3.zero))
			thisTransform.rotation = Quaternion.LookRotation(movement.normalized);
		//thisTransform.active/
			
		//movement += velocity;		
		//movement += Physics.gravity;
		//movement *= Time.deltaTime;

		// Actually move the character
		//  character.Move( movement );

		switch(_characterAction)
		{
			case CharacterAction.Take: 
				if(Vector3.Distance(navi.transform.position, targetLocation)<0.5){
					targetLocation = navi.transform.position;
						_inventory.Add(targetObject);
						targetObject = null;
						_characterAction = CharacterAction.None;
				}
				break;
			case CharacterAction.Dialog:
				Vector3 agentPos = new Vector3(navi.transform.position.x,0,navi.transform.position.z);
				if(Vector3.Distance(agentPos, targetLocation)<0.1){
					targetLocation = agentPos;
					targetRotation = Quaternion.LookRotation(new Vector3(targetObject.transform.position.x,0,targetObject.transform.position.z)-targetLocation);
					Debug.Log("TargetRotation="+targetRotation);
					Debug.Log("CurrRotation="+navi.transform.rotation);
					if(navi.transform.rotation == targetRotation){
						Debug.Log("TransferIn");
						CCScript.TransferIn(targetObject.GetComponentInChildren<Camera>());
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
		_characterAction = CharacterAction.Dialog;
	}
}
