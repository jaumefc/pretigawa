using UnityEngine;
using System.Collections;

public class MovimentInteriorPlaca : MonoBehaviour {

	public float speed;
	private NavMeshAgent navi;
	private Vector3 startingPosition;
	private bool hasDestination;

	public GameObject[] targetList;
	private Transform target;
	private int oldTarget = 0;
	private bool reassigning = false;

	private Animation anims;

	void Start () {
		anims = GetComponent<Animation> ();
		navi = GetComponent<NavMeshAgent>();
		startingPosition = transform.position;
		recheckSpeed ();
		setNewTarget ();
	}
	
	void Update () {
		moute ();
	}

	private void recheckSpeed(){

		bool run_walk = (Random.Range (0, 2) == 0) ? true : false;

		if (run_walk == true) {
			speed = 1f;
			anims.Play ("Walk");
		} else {
			speed = 2;
			anims.Play ("Run");
		}
		navi.speed = speed;
	}

	public void setTarget(GameObject newTarget){
		target = newTarget.transform;
		hasDestination = true;
	}

	private void setDestination(Vector3 destination){
		target.position = destination;
		hasDestination = true;
	}

	private void setNewTarget(){
		int rndTarget = oldTarget;
		while (rndTarget == oldTarget) {
			rndTarget = Random.Range (0, 8);
		}
		target = targetList [rndTarget].transform;
		hasDestination = true;
		oldTarget = rndTarget;
		reassigning = false;
		recheckSpeed ();
	}

	private void moute(){
		//if (!hasDestination){
		//	Invoke("setNewTarget", 2.0f);
		
		Vector3 movement = Vector3.zero;
		
		navi.SetDestination(target.position);
		Vector3 nextstep = navi.nextPosition;
		movement = nextstep - transform.position;
		movement.y = 0;

		//-->> At Destination
		Vector3 distance = target.position - transform.position;
		float absDistance = distance.sqrMagnitude;
		if(absDistance <= 5){
			if(!reassigning){
				reassigning = true;
				anims.Play ("Idle");
				Invoke("setNewTarget", 4.0f);
			}
		}
		//-->> Moving towards Destination
		else
		{
			movement = movement.normalized * speed;
		}		
	}
}
