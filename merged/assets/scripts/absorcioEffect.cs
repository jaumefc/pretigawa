using UnityEngine;
using System.Collections;

public class absorcioEffect : MonoBehaviour {

	private Vector3 startingPosition;
	private pathController pathcontroller;
	private GameObject conoObj;
	private GameObject pathObj;
	private GameObject mainChar;

	public ParticleSystem effecteCobertura;
	public ParticleSystem effecteBrillo;
	private ParticleSystem effecteCono;
	public ParticleSystem explosioFase2;
	private Rigidbody thisRigidBody;

	private ConversationTreeClass AfterTree;
	private bool started = false;
	private GameObject target;

	private bool goingTowardsTarget = false;

	public GameObject AbsSound;

	public void assigntree(ConversationTreeClass thistree){
		AfterTree = thistree;
	}

	void Start () {
	}

	void Update () {
		if (target == null || goingTowardsTarget == false) return;

		Vector3 vectorMoviment = (target.transform.position - mainChar.transform.position);
		if (vectorMoviment.magnitude < 6) {
			mainChar.GetComponent<NavMeshAgent>().Stop();
			if (!started) startAbsorcio ();
		}
		else {
				mainChar.GetComponent<mouseControl> ().GoTo (target.transform.position);
		}
	}

	public void tryStartAbsorcio(GameObject absTarget){
		target = absTarget;
		pathcontroller = GetComponent<pathController> ();
		mainChar = GameObject.Find ("Player");
		pathObj = GameObject.Find ("PartsAbsorcions");
		conoObj = GameObject.Find ("ConoAbsorcio");
		thisRigidBody = GetComponent<Rigidbody> ();
		pathcontroller.path [0] = mainChar.transform.FindChild("PartsAbsorcions");
		effecteCono = conoObj.GetComponent<ParticleSystem> ();

		goingTowardsTarget = true;
	}

	public void startAbsorcio(){
		AbsSound.audio.Play();
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemies");
		foreach(GameObject enemy in enemies){
			controlVisio cv = enemy.GetComponent<controlVisio>();
			cv.iaOff();
		}
		mainChar.transform.LookAt (target.transform);
		started = true;
		goingTowardsTarget = false;
		thisRigidBody.angularVelocity = new Vector3 (0, 1, 0);
		startingPosition = transform.position;
		mouseControl mc = mainChar.GetComponent<mouseControl> ();
		mc.enabled = false;
		effecteCobertura.Play ();
		effecteBrillo.Play ();
		effecteCono.Play ();
		Invoke ("faseDos", 3.5f);
	}

	private void faseDos(){
		thisRigidBody.angularVelocity = Vector3.zero;
		effecteCobertura.Stop ();
		effecteBrillo.Stop ();
		explosioFase2.Play ();
		animationControl ac = mainChar.GetComponent<animationControl> ();
		ac.enabled = false;
		Animation anims = mainChar.GetComponent<Animation> ();
		anims.Play ("Absorbir");
		effecteCono.Stop ();
		Invoke ("gopath", 1);
		Invoke ("destroyInstance", 2.6f);
	}

	private void gopath(){
		if(target != null)target.SetActive (false);
		pathcontroller.goPath = true;
	}

	private void destroyInstance(){
		if (AfterTree) {
			DialogCameraScript DCScript = GameObject.Find ("DialogLayout").GetComponent<DialogCameraScript>();
			DCScript.Init (AfterTree);
		}
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemies");
		foreach(GameObject enemy in enemies){
			controlVisio cv = enemy.GetComponent<controlVisio>();
			cv.iaOn();
		}
		mouseControl mc = mainChar.GetComponent<mouseControl> ();
		mc.enabled = true;
		pathcontroller.goPath = false;
		pathcontroller.reset ();
		effecteCobertura.Stop ();
		effecteBrillo.Stop ();
		explosioFase2.Stop ();
		thisRigidBody.velocity = Vector3.zero;
		thisRigidBody.angularVelocity = Vector3.zero;
		transform.position = startingPosition;
		started = false;
		animationControl ac = mainChar.GetComponent<animationControl> ();
		ac.enabled = true;
		Destroy (gameObject);
	}

}


