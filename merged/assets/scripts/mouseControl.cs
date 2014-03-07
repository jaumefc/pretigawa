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
	private Vector3 targetRotation;
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
			Application.Quit();
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
				
			RaycastHit hit;
			if( Physics.Raycast(ray, out hit) )
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
			if(_interactuable!=null) {
				float cx= Input.mousePosition.x;
				float cy= Input.mousePosition.y;
				Vector3 cPos = Input.mousePosition - _interactuable.GetScreenPosition();
				for(int i=0;i<_interactuable.Actions.Length;i++) {
					//TODO:Comprovar distancia del puntero al las opciones
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
				Debug.Log("TargetLocation"+targetLocation);
				Debug.Log("naviLoc="+navi.transform.position);
				Debug.Log("Dist="+Vector3.Distance(navi.transform.position, targetLocation));
				Vector3 agentPos = new Vector3(navi.transform.position.x,0,navi.transform.position.z);
				if(Vector3.Distance(agentPos, targetLocation)<0.1){
					targetLocation = navi.transform.position;
					//TODO:targetRotation
					Debug.Log("TransferIn");
					CCScript.TransferIn(targetObject.GetComponentInChildren<Camera>());
					targetObject = null;
					_characterAction = CharacterAction.None;
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
				//TODO: TargetRotation;
		targetRotation = -targetObj.transform.rotation.eulerAngles;
		targetObject = targetObj;
		_characterAction = CharacterAction.Dialog;
	}
}
