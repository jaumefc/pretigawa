using UnityEngine;
using System.Collections;

public class ConversationTreeClass : MonoBehaviour {

	public enum ConverationStyle{
		BAFARADA,
		THINKING,
		NARRADOR
	};

	public ConversationNodeClass[] rootNodes;
	public bool useDialgogCamera = true;
	public ConverationStyle style = ConverationStyle.BAFARADA;



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
