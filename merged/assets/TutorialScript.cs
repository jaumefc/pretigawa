using UnityEngine;
using System.Collections;

public class TutorialScript : MonoBehaviour {


	private GameObject mainChar;
	private NavMeshAgent navi;
	private enum Estat {TRAVELLING, WAITING, FALLING, ESCALATING, ROTATING, STARTTUTORIAL, EMPTY};
	private Estat varEstat=Estat.TRAVELLING;

	//activate/deactivate player
	private CharacterController CharControler;
	private mouseControl MouControl;
	private NavMeshAgent NMAgent;
	private animationControl AnimControl;
	private HysteresisCamAssigned HCAssigned;
	private CapsuleCollider CapCollider;

	private float iniTime;

	//Narrator conversation
	private DialogCameraScript DCScript;
	public ConversationTreeClass StartTree;


	public GameObject posIni, posIni2, posIni3, posTut, posCamTut, posCamIni;
	public float timetravel, timewait, time12, time3tutpos, time3tutrot;
	public Camera TutorialCamera;




	// Use this for initialization
	void Start () {
		mainChar = GameObject.Find ("Player");
				
		CharControler = mainChar.GetComponent<CharacterController>();
		MouControl = mainChar.GetComponent<mouseControl> ();
		NMAgent = mainChar.GetComponent<NavMeshAgent> ();
		AnimControl = mainChar.GetComponent<animationControl>();
		HCAssigned = mainChar.GetComponent<HysteresisCamAssigned>();
		CapCollider = mainChar.GetComponent<CapsuleCollider> ();

		DCScript = GameObject.Find ("DialogLayout").GetComponent<DialogCameraScript>();

		mainChar.transform.position=posIni.transform.position;
		mainChar.transform.rotation=posIni.transform.rotation;
		navi = mainChar.GetComponent<NavMeshAgent>();
		navi.enabled=false;



		CharControler.enabled=false;
		MouControl.enabled=false;
		NMAgent.enabled=false;
		AnimControl.enabled=false;
		HCAssigned.enabled=false;
		CapCollider.enabled = false;

		GameObject.Find ("TriggeredCAMS").SetActive (false);
		//Camera.main.enabled = false;
		TutorialCamera.enabled=true;
		TutorialCamera.tag = "MainCamera";




			
	}
	
	// Update is called once per frame
	void Update () {
		if (varEstat==Estat.TRAVELLING){
			if (TutorialCamera.transform.position!=posCamTut.transform.position){
				TutorialCamera.transform.position= Vector3.Lerp(posCamIni.transform.position,posCamTut.transform.position,Time.time * timetravel);
				TutorialCamera.transform.rotation= Quaternion.Lerp(posCamIni.transform.rotation,posCamTut.transform.rotation,Time.time * timetravel);
			}
			else if (TutorialCamera.transform.position==posCamTut.transform.position){
				varEstat=Estat.WAITING;
				iniTime=Time.time;
			}
		}
		else if (varEstat==Estat.WAITING){
			if (iniTime + timetravel > Time.time)
			varEstat=Estat.FALLING;
			iniTime=Time.time;
		}

			else if (varEstat==Estat.FALLING){
			if (mainChar.transform.position!=posIni2.transform.position){
				mainChar.transform.position= Vector3.Lerp(posIni.transform.position,posIni2.transform.position,(Time.time-iniTime) * time12);
				mainChar.transform.rotation= Quaternion.Lerp(posIni.transform.rotation,posIni2.transform.rotation,(Time.time-iniTime) * time12);
			}
			else if (mainChar.transform.position==posIni2.transform.position){
				mainChar.transform.position=posIni3.transform.position;
				mainChar.transform.rotation=posIni3.transform.rotation;
				varEstat=Estat.ESCALATING;
				iniTime=Time.time;
				posIni2.audio.Play();
			}
		}
		else if (varEstat==Estat.ESCALATING){
			if (mainChar.transform.position!=posTut.transform.position){
				mainChar.transform.position= Vector3.Lerp(posIni3.transform.position,posTut.transform.position,(Time.time-iniTime) * time3tutpos);
			}
			else if (mainChar.transform.position==posTut.transform.position){
				varEstat=Estat.ROTATING;
				iniTime=Time.time;
				posTut.audio.Play();
			}


		}
		else if (varEstat==Estat.ROTATING){
			if (mainChar.transform.rotation!=posTut.transform.rotation){
				mainChar.transform.rotation= Quaternion.Lerp(posIni3.transform.rotation,posTut.transform.rotation,(Time.time-iniTime) * time3tutrot);
			}
			else if (mainChar.transform.rotation==posTut.transform.rotation){
				varEstat=Estat.STARTTUTORIAL;
			}

		}
		else if (varEstat==Estat.STARTTUTORIAL){
			Debug.Log(varEstat);
			//DCScript.SetRootNodes(StartTree.rootNodes);
			DCScript.Init(StartTree);
			varEstat=Estat.EMPTY;

		}
		else if (varEstat==Estat.EMPTY){

		}
	
	}


}
