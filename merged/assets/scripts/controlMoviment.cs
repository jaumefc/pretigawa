using UnityEngine;
using System.Collections;

public class controlMoviment : MonoBehaviour {
	
	public float speed;

	private Transform target;
	private Vector3 startingPosition;
	private NavMeshAgent navi;

	private bool hasTarget = false;
	private bool stopMoving = false;

	private Vector3 thisNextstep;

	enum enemyState {
		idle = 0,
		move = 1,
		getBack = 2,
		attack = 3,
		die = 4,
	}

	enemyState _enemyState;

	void Start(){
		startingPosition = transform.position;
		navi = GetComponent<NavMeshAgent>();
	}

	public void setState (int state){
		switch (state) {
		case 0: _enemyState = enemyState.idle;break;
		case 1: _enemyState = enemyState.move;break;
		case 2: _enemyState = enemyState.getBack;break;
		case 3: _enemyState = enemyState.attack;break;
		case 4: _enemyState = enemyState.die;break;
		default: _enemyState = enemyState.idle;break;
		}
	}

	public void setTarget(GameObject newTarget){
		target = newTarget.transform;
		hasTarget = true;
	}

	void Update () {

		switch (_enemyState) {
			case enemyState.idle:
			break;

			case enemyState.move:
				moute();
			break;

			case enemyState.getBack:
				torna ();
			break;

			case enemyState.attack:
			break;

			case enemyState.die:
			break;

			default:
				_enemyState = enemyState.idle;
			break;
		}

	}

	private void moute(){
		if (!hasTarget) return;
		if (stopMoving) return;

		Vector3 movement = Vector3.zero;

		navi.SetDestination(target.position);
		Vector3 nextstep = navi.nextPosition;
		movement = nextstep - transform.position;
		movement.y = 0;
		
		float dist = movement.magnitude;

		if( navi.velocity.sqrMagnitude < 0.5 )
		{
			_enemyState = enemyState.idle;
		}
		else
		{
			movement = movement.normalized * speed;
			_enemyState = enemyState.move;
		}


		//float step = speed * Time.deltaTime;
		//transform.position = Vector3.MoveTowards(transform.position, target.position, step);
	}

	private void torna(){
		if (stopMoving) return;

		Vector3 movement = Vector3.zero;

		navi.SetDestination(startingPosition);
		Vector3 nextstep = navi.nextPosition;

		thisNextstep = nextstep;

		movement = nextstep - transform.position;
		movement.y = 0;
		
		float dist = movement.magnitude;
		if( navi.velocity.sqrMagnitude < 0.5 )
		{
			_enemyState = enemyState.idle;
		}
		else
		{
			movement = movement.normalized * speed;
			_enemyState = enemyState.move;
		}
	}

}
