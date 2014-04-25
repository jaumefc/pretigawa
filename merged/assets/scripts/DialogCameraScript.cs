using UnityEngine;
using System.Collections;

public class DialogCameraScript : MonoBehaviour {

	public GUIStyle style_think;
	public GUIStyle style_alien;
	public GUIStyle style_theother;

	CameraControl CCScript;

	float ratio = 1f;
	float previousScreenWidth = 1f;
	float previousScreenHeight = 1f;
	
	bool updatePosition = true;
	bool updateSize = true;
	float size = 1f;
	float x = 0.5f;
	float y = 0.5f;

	GameState gs;
	

	
	// Use this for initialization
	void Start () {
		
		//render init
		InvokeRepeating("Adjust", 0f, 0.5f);	
		//gs = GameState.GetInstance ();
	}


	void Awake() {
		
		if(guiTexture) {
			
			// Store texture ratio
			ratio = guiTexture.pixelInset.width / guiTexture.pixelInset.height;
		}
		guiTexture.enabled = false;
		CCScript = GameObject.Find("CameraControl").GetComponent<CameraControl>();

	}
	
	
	
	
	
	// Set new size and offset
	void Adjust() {
		
		// Return if screen size did not change
		if(previousScreenWidth == Screen.width && previousScreenHeight == Screen.height) {
			
			return;	
		}
		
		// Store previous screen dimensions
		previousScreenWidth = Screen.width;
		previousScreenHeight = Screen.height;
		
		//set font size
		style_think.fontSize = Mathf.RoundToInt(Screen.height/40 * size);
		style_alien.fontSize = style_think.fontSize;
		style_theother.fontSize = style_think.fontSize;


		//set texture size
		if (guiTexture) {
			
			// Set size and position
			float left = guiTexture.pixelInset.x;
			float top = guiTexture.pixelInset.y;
			float width = guiTexture.pixelInset.width;
			float height = guiTexture.pixelInset.height;

			if (updateSize && size > 0f) {
				
				height = Screen.height * size;
				width = height * ratio;
			}

			if (updatePosition) {
			
				left = Screen.width * x - width / 2f;
				top = Screen.height * y - height / 2f;
			}
			guiTexture.pixelInset = new Rect (left, top, width, height);
		}
	}



	//Manage conversation
	
	[HideInInspector]
	public ConversationNodeClass NextNode, PreviousNode;
	float gettime = 0;

	
	bool IsAnySelected(ConversationNodeClass Node){
		for (int i=0; i<Node.cncArray.Length; i++){
			if (Node.cncArray[i].bIsSelected) return true;
		}
		return false;
	}




	void OnGUI() {

		ConversationNodeClass curNode = NextNode;
		if (curNode == null)
						return;
		if (curNode.cncArray.Length==0){
			Debug.Log ("Now we are supposed to leave");
			//check if any action to do

			//if (curNode.sNextAction!=null){
			//	BroadcastMessage(curNode.sNextAction, curNode.GOApplied);
			//}



			//sortir
			PreviousNode = null;
			Debug.Log(Camera.current);
			if(CCScript.IsIn())
				CCScript.TransferOut();
			this.enabled = false;
		}
		if (PreviousNode == null) {
			
			GUI.Button (new Rect (0.005f * Screen.width, 0.01f * Screen.height, 0.24f * Screen.width, 0.22f * Screen.height), 
			            curNode.sFirstOption, style_alien);
			if (gettime < curNode.fSeconds) {
				//curNode.cncArray [0].bIsSelected = false;
				//NextNode = curNode.cncArray [0];
				gettime +=Time.deltaTime;
				return;
			}
			else{
				gettime =0;
			}

		}
		int length = 0;
//		for (int i=0; i<curNode.cncArray.Length; i++) {
//		switch(curNode.cncArray[i].ShowNode){
//			case ConversationNodeClass.show.ALWAYS:length++;break;
//			case ConversationNodeClass.show.IF : break;
//				//if(gs.GetVariable(curNode.cncArray[i].var))
//				}
//		}


		//Automatic dialog (only one option)
		if (curNode.cncArray.Length == 1) {
			if (curNode.cncArray [0].sSpeaker == ConversationNodeClass.speaker.ALIEN) {

				GUI.Button (new Rect (0.005f * Screen.width, 0.01f * Screen.height, 0.24f * Screen.width, 0.22f * Screen.height), 
				            curNode.cncArray [0].sFirstOption, style_alien);

				if (!curNode.cncArray [0].bIsSelected) {
					Debug.Log ("Automatic Button1");
					gettime = Time.time;
					curNode.cncArray [0].bIsSelected = true;
				
				}else{
					if (gettime + curNode.fSeconds < Time.time) {
						curNode.cncArray [0].bIsSelected = false;
						NextNode = curNode.cncArray [0];
						gettime = 0;
					}
				}//selected
			} else if (curNode.cncArray [0].sSpeaker == ConversationNodeClass.speaker.THEOTHER) {
				GUI.Button (new Rect (0.74f * Screen.width, 0.05f * Screen.height, 0.24f * Screen.width, 0.22f * Screen.height), 
				            curNode.cncArray [0].sFirstOption, style_theother);

				if (!curNode.cncArray [0].bIsSelected) {
					Debug.Log ("Automatic Other");
					gettime = Time.time;
					curNode.cncArray [0].bIsSelected = true;
				
				} else { 
					if (gettime + curNode.fSeconds < Time.time) {
						curNode.cncArray [0].bIsSelected = false;
						NextNode = curNode.cncArray [0];
						gettime = 0;
					}
				}//selected
			}//alien or the other
		}//single option



		//TODO: set as single when selected???
		
		if(curNode.cncArray.Length>1){
			if(!IsAnySelected(curNode)){
				if (GUI.Button (new Rect (0.005f*Screen.width, 0.01f*Screen.height, 0.24f*Screen.width, 0.22f*Screen.height), 
				                curNode.cncArray[0].sFirstOption, style_think)){
					Debug.Log ("Button1");
					gettime = Time.time;
					curNode.cncArray[0].bIsSelected = true;
				}
			}else{ //node selected
				if(curNode.cncArray[0].bIsSelected){
					GUI.Button (new Rect (0.005f * Screen.width, 0.01f * Screen.height, 0.24f * Screen.width, 0.22f * Screen.height), 
					            curNode.cncArray[0].sFirstOption, style_alien);
					if (gettime + curNode.fSeconds < Time.time) {
						curNode.cncArray[0].bIsSelected = false;
						NextNode = curNode.cncArray[0];
						gettime = 0;
					}
				}
			}//Button1
		
			if(!IsAnySelected(curNode)){
				if (GUI.Button (new Rect (0.25f*Screen.width, 0.05f*Screen.height, 0.24f*Screen.width, 0.22f*Screen.height), 
				                curNode.cncArray[1].sFirstOption, style_think)){
					Debug.Log ("Button2");
					gettime = Time.time;
					curNode.cncArray[1].bIsSelected = true;
				}
			}else{ //node selected
				if(curNode.cncArray[1].bIsSelected){
					GUI.Button (new Rect (0.005f * Screen.width, 0.01f * Screen.height, 0.24f * Screen.width, 0.22f * Screen.height), 
					            curNode.cncArray [1].sFirstOption, style_alien);
					if (gettime + curNode.fSeconds < Time.time) {
						curNode.cncArray[1].bIsSelected = false;
						NextNode = curNode.cncArray[1];
						gettime = 0;
					}
				}
			}//Button2
		}//2 options


		if(curNode.cncArray.Length>2){
			if(!IsAnySelected(curNode)){
				if (GUI.Button (new Rect (0.495f*Screen.width, 0.01f*Screen.height, 0.24f*Screen.width, 0.22f*Screen.height), 
				                curNode.cncArray[2].sFirstOption, style_think)){
					Debug.Log ("Button3");
					gettime = Time.time;
					curNode.cncArray[2].bIsSelected = true;
				}
			}else{ //node selected
				if(curNode.cncArray[2].bIsSelected){
					GUI.Button (new Rect (0.005f * Screen.width, 0.01f * Screen.height, 0.24f * Screen.width, 0.22f * Screen.height), 
					            curNode.cncArray [2].sFirstOption, style_alien);
					if (gettime + curNode.fSeconds < Time.time) {
						curNode.cncArray [2].bIsSelected = false;
						NextNode = curNode.cncArray[2];
						gettime = 0;
					}
				}
			}
		}//3 options



		if(curNode.cncArray.Length>3){
			if(!IsAnySelected(curNode)){
				if (GUI.Button (new Rect (0.74f*Screen.width, 0.05f*Screen.height, 0.24f*Screen.width, 0.22f*Screen.height), 
				                curNode.cncArray[3].sFirstOption, style_think)){
					Debug.Log ("Button4");
					gettime = Time.time;
					curNode.cncArray[3].bIsSelected = true;
				}
			}else{ //node selected
				if(curNode.cncArray[3].bIsSelected){
					GUI.Button (new Rect (0.005f * Screen.width, 0.01f * Screen.height, 0.24f * Screen.width, 0.22f * Screen.height), 
					            curNode.cncArray [3].sFirstOption, style_alien);
					if (gettime + curNode.fSeconds < Time.time) {
						curNode.cncArray [3].bIsSelected = false;
						NextNode = curNode.cncArray[3];
						gettime = 0;
					}
				}
			}
		}//4 options
		PreviousNode = curNode;
	}//OnGui




}
	
	
	
	
	
