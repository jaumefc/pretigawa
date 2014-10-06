using UnityEngine;
using System.Collections;

public class WaitressKick : MonoBehaviour {

	private GameObject mainChar;

	private CharacterController CharControler;
	private mouseControl MouControl;
	private NavMeshAgent NMAgent;
	private animationControl AnimControl;
	private HysteresisCamAssigned HCAssigned;
	private CapsuleCollider CapCollider;

	private bool UpdateEnabled = false;
	private bool Pujada = true;

	public Transform Target1;
	public Transform Target2;
	public Camera boardCam;
	public Camera flyCam;
	public float firingAngle1 = 45.0f;
	public float firingAngle2 = -45.0f;
	public float gravity = 9.8f;

	private float Vx, Vy;
	private float elapse_time;
	float flightDuration1;
	float flightDuration2;
	
	public Transform Projectile;      
	private Transform myTransform;
	private int state =-1;
	private CameraControl ccontrol;
	private float initState;

	void Awake()
	{
		myTransform = transform;  

		mainChar = GameObject.Find ("Player");
		ccontrol = GameObject.Find("CameraControl").GetComponent<CameraControl>();
		
		CharControler = mainChar.GetComponent<CharacterController>();
		MouControl = mainChar.GetComponent<mouseControl> ();
		NMAgent = mainChar.GetComponent<NavMeshAgent> ();
		AnimControl = mainChar.GetComponent<animationControl>();
		HCAssigned = mainChar.GetComponent<HysteresisCamAssigned>();
		CapCollider = mainChar.GetComponent<CapsuleCollider> ();

	}

	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {
		switch(state)
		{
			case 0:showBoardCam();break;
			case 1:showGreenCam();break;
			case 2:showParabolicCam();break;
			case 3:activateAllObjects();break;
		}

		/*
		if (UpdateEnabled==true){

			Projectile.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);
		//	Projectile.transform.rotation = Quaternion.Lerp(myTransform.rotation, Target.rotation, elapse_time*flightDuration);
		//	mainChar.transform.rotation= Quaternion.Lerp(posIni.transform.rotation,posIni2.transform.rotation,Time.time * time12);
				
			elapse_time += Time.deltaTime;

			if ((elapse_time>= flightDuration1)&&(Pujada==true)){
				UpdateEnabled=false;
				Pujada=false;
				Recalculate();
			}
			if ((elapse_time>=flightDuration2)&&(Pujada==false)){
				UpdateEnabled=false;
			}
		}
	*/
	}

	void OnTriggerEnter(Collider other){
		if(other == mainChar.collider){
			
			ccontrol.TransferIn(boardCam);
			initState = Time.timeSinceLevelLoad;
			NMAgent.Stop();
			state = 0;
			Debug.Log(state);
			/*
			CharControler.enabled=false;
			MouControl.enabled=false;
			NMAgent.enabled=false;
			AnimControl.enabled=false;
			HCAssigned.enabled=false;
			CapCollider.enabled = false;


			// Move projectile to the position of throwing object + add some offset if needed.
			Projectile.position = myTransform.position + new Vector3(0, 0.0f, 0);
			
			// Calculate distance to target
			float target_Distance = Vector3.Distance(Projectile.position, Target1.position);
			
			// Calculate the velocity needed to throw the object to the target at specified angle.
			float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle1 * Mathf.Deg2Rad) / gravity);
			
			// Extract the X  Y componenent of the velocity
			Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle1 * Mathf.Deg2Rad);
			Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle1 * Mathf.Deg2Rad);
			
			// Calculate flight time.
			flightDuration1 = target_Distance / Vx;

			// Rotate projectile to face the target.
			Projectile.rotation = Quaternion.LookRotation(Target1.position - Projectile.position);



			elapse_time = 0;
			UpdateEnabled=true;
			*/
		}
	}




	void Recalculate(){

		// Move projectile to the position of throwing object + add some offset if needed.
//		Projectile.position = Target1.position + new Vector3(0, 0.0f, 0);

		Projectile.position = Target1.position;



		
		// Calculate distance to target
		float target_Distance = Vector3.Distance(Projectile.position, Target2.position);
		
		// Calculate the velocity needed to throw the object to the target at specified angle.
		float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle2 * Mathf.Deg2Rad) / gravity);
		
		// Extract the X  Y componenent of the velocity
		Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle2 * Mathf.Deg2Rad);
		Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle2 * Mathf.Deg2Rad);

		// Calculate flight time.
		flightDuration2 = target_Distance / Vx;
		
		// Rotate projectile to face the target.
		Projectile.rotation = Quaternion.LookRotation(Target2.position - Projectile.position);

		elapse_time = 0;
		UpdateEnabled=true;



	}

	void showBoardCam(){
		if(Time.timeSinceLevelLoad-initState>3){
			state = 1;
			//ccontrol.TransferIn(mainChar)
			initState = Time.timeSinceLevelLoad;
			Debug.Log(state);
		}
	}


	void showGreenCam(){
		if(Time.timeSinceLevelLoad-initState>3){
			state = 2;
			ccontrol.TransferIn(flyCam);
			initState = Time.timeSinceLevelLoad;


			CharControler.enabled=false;
			MouControl.enabled=false;
			NMAgent.enabled=false;
			AnimControl.enabled=false;
			HCAssigned.enabled=false;
			CapCollider.enabled = false;
			
			
			// Move projectile to the position of throwing object + add some offset if needed.
			Projectile.position = myTransform.position + new Vector3(0, 0.0f, 0);
			
			// Calculate distance to target
			float target_Distance = Vector3.Distance(Projectile.position, Target1.position);
			
			// Calculate the velocity needed to throw the object to the target at specified angle.
			float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle1 * Mathf.Deg2Rad) / gravity);
			
			// Extract the X  Y componenent of the velocity
			Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle1 * Mathf.Deg2Rad);
			Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle1 * Mathf.Deg2Rad);
			
			// Calculate flight time.
			flightDuration1 = target_Distance / Vx;
			
			// Rotate projectile to face the target.
			Projectile.rotation = Quaternion.LookRotation(Target1.position - Projectile.position);
			
			
			
			elapse_time = 0;
			UpdateEnabled=true;

			
			Debug.Log(state);

		}
	}

	void showParabolicCam(){

		
		if (UpdateEnabled==true){
			
			Projectile.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);
			//	Projectile.transform.rotation = Quaternion.Lerp(myTransform.rotation, Target.rotation, elapse_time*flightDuration);
			//	mainChar.transform.rotation= Quaternion.Lerp(posIni.transform.rotation,posIni2.transform.rotation,Time.time * time12);
			
			elapse_time += Time.deltaTime;
			
			if ((elapse_time>= flightDuration1)&&(Pujada==true)){
				UpdateEnabled=false;
				Pujada=false;
				Recalculate();
			}
			if ((elapse_time>=flightDuration2)&&(Pujada==false)){
				UpdateEnabled=false;
			}
			Debug.Log(state);
		}



	}

	void activateAllObjects(){
	}
}












	