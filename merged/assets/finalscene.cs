using UnityEngine;
using System.Collections;

public class finalscene : MonoBehaviour {


	private GameObject mainChar;
	private Vector3 posini;
	private NavMeshAgent navi;
	private mouseControl MouControl;
	public DialogCameraScript DCScript;
	public ConversationTreeClass StartTree;

	private bool alreadyPanned = false;

	// Use this for initialization
	void Start () {
		mainChar = GameObject.Find ("Player");
		posini = mainChar.transform.position;

		MouControl = mainChar.GetComponent<mouseControl>();
		MouControl.enabled = true;

		Invoke ("repositionPlayer", 0.1f);



	}

	private void repositionPlayer(){
		NavMeshAgent NA = mainChar.GetComponent ("NavMeshAgent") as NavMeshAgent;
		NA.enabled = false;
		mainChar.transform.position = posini;
	}

	void Update () {
		if (alreadyPanned == true)
				return;
		else {
			pathController pc = GameObject.Find ("CameraIni").GetComponent<pathController>();
			if(pc.posicioActual >= 60){
				DCScript.Init(StartTree);
				alreadyPanned = true;
			}
		}
	}


}
