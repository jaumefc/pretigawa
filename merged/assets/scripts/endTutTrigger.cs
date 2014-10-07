using UnityEngine;
using System.Collections;

public class endTutTrigger : MonoBehaviour {
	
	private GameObject mainChar;
	private CharacterController CharControler;
	private mouseControl MouControl;
	private NavMeshAgent NMAgent;
	private animationControl AnimControl;
	private HysteresisCamAssigned HCAssigned;
	private CapsuleCollider CapCollider;
	// Use this for initialization
	void Start () {
		mainChar = GameObject.Find ("Player");
		CharControler = mainChar.GetComponent<CharacterController>();
		MouControl = mainChar.GetComponent<mouseControl> ();
		NMAgent = mainChar.GetComponent<NavMeshAgent> ();
		AnimControl = mainChar.GetComponent<animationControl>();
		HCAssigned = mainChar.GetComponent<HysteresisCamAssigned>();
		CapCollider = mainChar.GetComponent<CapsuleCollider> ();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject == mainChar)
		{
			NMAgent.enabled=true;
			NMAgent.SetDestination(mainChar.transform.position);
			AnimControl.enabled=true;
			HCAssigned.enabled=true;
			this.gameObject.SetActive(false);
		}
	}
}
