using UnityEngine;
using System.Collections;

public class pathController : MonoBehaviour {
	
	public Transform[] path;
	
	//-->> 0 inici // 1 final
	public float posicioActual = 0.0f;
	public float velocitat;
	public float acceleracio = 0.0f;
	public float margeError = 0.5f;
	public float suavitzatCurva = 3.0f;
	public bool goPath = false;

	private Vector3 startingPosition = new Vector3();
	private Transform target;
	private float increment;
	private float incrementActual;
	private int nextPos = 0;
	private float t = 0;
	private float velocitatInici;

	void Start(){
		t = posicioActual / 100.0f;
		if (velocitat == 0)velocitat = 10.0f;
		startingPosition = transform.position;
		target = path [nextPos];
		increment = 100.0f / (path.Length-1);
		velocitatInici = velocitat;
	}

	void Update () {
		if (!goPath)return;

		velocitat += acceleracio / 100;
		transform.position = Spline.MoveOnPath(path, transform.position, ref t, velocitat);

		if (target) {
			Quaternion rotation = Quaternion.LookRotation (target.position - transform.position);
			transform.rotation = Quaternion.Slerp (transform.rotation, rotation, Time.deltaTime * suavitzatCurva);
		}

		posicioActual = t * 100;
		incrementActual = posicioActual - nextPos*increment;

		if (incrementActual >= (-1*margeError)) {
			nextPos++;
			if(nextPos<path.Length){
				target = path [nextPos];
			}else{
				target = null;
				goPath = false;
			}
		}
	}

	public void reset(){
		posicioActual = 0.0f;
		t = 0.0f;
		transform.position = startingPosition;
		nextPos = 0;
		target = path [0];
		velocitat = velocitatInici;
	}

}
