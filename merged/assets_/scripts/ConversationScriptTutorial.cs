using UnityEngine;
using System.Collections;

public class ConversationScriptTutorial : MonoBehaviour {


	private GameState gs;
	private GameObject player;
	private GameObject shot;
	private InventoryControl ic;


	private CharacterController CharControler;
	private mouseControl MouControl;
	private NavMeshAgent NMAgent;
	private animationControl AnimControl;
	private HysteresisCamAssigned HCAssigned;
	private CapsuleCollider CapCollider;
	private NavMeshAgent navi;
	public GameObject cams;
	private Camera TutorialCamera;



	// Use this for initialization
	void Start () {
		//cams = GameObject.Find ("TriggeredCAMS");
		gs = GameState.GetInstance ();
		player = GameObject.Find ("Player");
		shot = GameObject.Find ("shot");
		ic = player.GetComponent<InventoryControl> ();
		if(!gs.ExistsBool("RecipeFirstTime"))
			gs.AddBool ("RecipeFirstTime", true);;



		CharControler = player.GetComponent<CharacterController>();
		MouControl = player.GetComponent<mouseControl> ();
		//NMAgent = player.GetComponent<NavMeshAgent> ();
		AnimControl = player.GetComponent<animationControl>();
		HCAssigned = player.GetComponent<HysteresisCamAssigned>();
		CapCollider = player.GetComponent<CapsuleCollider> ();
		navi = player.GetComponent<NavMeshAgent>();
		TutorialCamera = GameObject.Find ("CameraIni").GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void EnableAll(){
		Debug.Log ("EnableAll()!!!!!!");
		//cams.SetActive (false);
		TutorialCamera.tag = "";
		TutorialCamera.enabled=false;
		cams.SetActive (true);
		navi.enabled=true;
		navi.Warp (player.transform.position);
		MouControl.GoTo (player.transform.position);
		MouControl.enabled=true;
		//NMAgent.enabled=true;
		AnimControl.enabled=true;
		HCAssigned.enabled=true;
		//CapCollider.enabled = true;
		CharControler.enabled=true;

	}














	//exemples cambrera

	void FalseRecipeFirstTime(){
		gs.SetBool ("RecipeFirstTime", false);
	}

	
	void GetShot(){
		//shot in inventory
		ic.Add2 (shot);

	}



}
