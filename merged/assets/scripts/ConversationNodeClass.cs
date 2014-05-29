using UnityEngine;
using System.Collections;

public class ConversationNodeClass : MonoBehaviour {

	public enum costume {NAKED, JAPANESE,MS_FORTUNE, EXTINGUISHER, ALL};//add as many as needed, global enum!
	public enum speaker {ALIEN, THEOTHER};
	public enum show {ALWAYS,IF, IF_NOT};

	//information
	public show ShowNode = show.ALWAYS;
	[HideInInspector]
	public string var;
	public string sFirstOption, sSecondOption;
	public costume cCharacter;
	public speaker sSpeaker;
	public float fSeconds = 3;
	[HideInInspector]
	public bool showNodes = false;
	public ConversationNodeClass[] cncArray;


	public bool bIsSelected=false;
	public bool bIsUsed=false;

	public string preAction;
	public string postAction;
	public AudioClip clipAudio;


	public GameObject GetRootNode (){
		GameObject parent = transform.parent.gameObject;
		if(parent.GetComponent<ConversationTreeClass>())
			return parent;
		else  if (parent.GetComponent<ConversationNodeClass>()){
			ConversationNodeClass cnc = parent.GetComponent<ConversationNodeClass>();
			return cnc.GetRootNode();
		}
		else
			Debug.LogError("[ConversationNodeClass] No NodeClass o TreeClass in parent object");
		return null;
	}

}