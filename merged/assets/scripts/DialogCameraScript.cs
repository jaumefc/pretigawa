using UnityEngine;
using System.Collections;

public class DialogCameraScript : MonoBehaviour {
	
	enum DialogState {INIT, EVAL, SHOW, EXEC, END, NONE};
	enum ShowState {SHOW,CLICK_WAIT};
	
	DialogState dialogState;
	private ShowState showState=ShowState.SHOW;
	
	private GameObject[] objBallons = new GameObject[4];
	private Texture texture;
	private bool skip = false;
	
	public GUIStyle style_think;
	public GUIStyle style_alien;
	public GUIStyle style_theother;
	public GUIStyle style_narrator;

	CameraControl CCScript;
	
	//	float ratio = 1f;
	
	float size = 1f;
	//	float x = 0.5f;
	//	float y = 0.5f;
	
	float screenratio;
	
	GameState gs;
	InventoryControl inventoryControl;
	
	private AudioSource playerAS;
	
	[HideInInspector]
	public ConversationNodeClass CurNode;
	float gettime = 0;
	
	IList NodesToShow = new ArrayList();
	ConversationNodeClass[] NodesToEvaluate;
	
	
	// Use this for initialization
	void Start () {
		//render init
		gs = GameState.GetInstance ();
		screenratio = (float)(Screen.width) / (float)(Screen.height);
		CCScript = GameObject.Find("CameraControl").GetComponent<CameraControl>();
		playerAS = GameObject.Find("Player").GetComponent<AudioSource>();
		inventoryControl = GameObject.Find ("Player").GetComponent<InventoryControl> ();
		style_think.fontSize = Mathf.RoundToInt(Screen.height/40 * size);
		style_alien.fontSize = style_think.fontSize;
		style_theother.fontSize = style_think.fontSize;
		style_narrator.fontSize = style_think.fontSize;
		texture = Resources.Load<Texture> ("Textures/GUI_elements/G07");
		for (int i = 0; i<4; i++) 
		{
			objBallons[i] = new GameObject("balloon"+i);
			objBallons[i].AddComponent<GUIText>();
			objBallons[i].AddComponent<GUITexture>();
			objBallons[i].transform.position = new Vector3(0.12f + (0.24f * Screen.width * i)/Screen.width,(0.78f * Screen.height)/Screen.height,-1);
			objBallons[i].transform.localScale = new Vector3(0.24f,0.44f,1);
			objBallons[i].guiTexture.texture = texture;
			objBallons[i].guiTexture.enabled = false;
			objBallons[i].guiText.enabled = false;
			objBallons[i].guiText.fontSize = style_think.fontSize;
			objBallons[i].guiText.anchor = TextAnchor.MiddleCenter;
			objBallons[i].guiText.font = style_think.font;
			objBallons[i].guiText.color = Color.black;
		}
		
		//inicialitzar mides bafarada?
	}
	
	void InitDialog(){
		
		//coses iniciar
		dialogState = DialogState.EVAL;
	}
	
	/*
	 * De tots els node possibles, comprova quins seran els que es mostraran.
	 * Comprova variables al gameState
	 */
	void EvaluateNodes(ConversationNodeClass[] NodeArray){
		
		NodesToShow.Clear();
		
		if (NodeArray.Length == 1){	//automatic conversation
			
			CurNode=NodeArray[0];
			dialogState = DialogState.EXEC;
			gettime=Time.time;
			if(CurNode.preAction!=""){
				CurNode.GetRootNode().BroadcastMessage(CurNode.preAction);
			}
			if(CurNode.clipAudio){
				playerAS.PlayOneShot(CurNode.clipAudio);
			}
			
			
		}
		else //options to decide
		{
			for (int i=0; i<NodeArray.Length; i++){
				
				if(CheckConditional(NodeArray[i]) && CheckCustom(NodeArray[i]))
					NodesToShow.Add (NodeArray[i]);
				
				//				if (NodeArray[i].ShowNode==ConversationNodeClass.show.ALWAYS){
				//					NodesToShow.Add (NodeArray[i]);
				//				}
				//				else if (NodeArray[i].ShowNode==ConversationNodeClass.show.IF&&gs.GetBool(NodeArray[i].var)==true){
				//					NodesToShow.Add (NodeArray[i]);
				//				}
				//				else if (NodeArray[i].ShowNode==ConversationNodeClass.show.IF_NOT&&gs.GetBool(NodeArray[i].var)==false){
				//					NodesToShow.Add (NodeArray[i]);
				//				}
				
			}
			switch (NodesToShow.Count){
			case 0:
				dialogState=DialogState.END;
				break;
				
			case 1:
				dialogState=DialogState.EXEC;
				gettime=Time.time;
				CurNode=(ConversationNodeClass)NodesToShow[0];
				if(CurNode.preAction!=""){
					CurNode.GetRootNode().BroadcastMessage(CurNode.preAction);
				}
				if(CurNode.clipAudio){
					playerAS.PlayOneShot(CurNode.clipAudio);
				}
				break;
				
			default:
				dialogState=DialogState.SHOW;
				ShowNodes(NodesToShow);
				break;
				
			}
		}
	}
	
	/*
	 * Mostra les opcions possibles de dialeg i espera que es seleccioni una d'elles
	 */
	void ShowNodes(IList NodesToShow){
		if (NodesToShow.Count>4){
			Debug.LogError ("More than 4 available options!");
			return;
		}
		if (showState == ShowState.SHOW) //Es mostren les opcions, i es canvia el subestat d'aquest node perque esperi un clic
		{
			for(int i = 0;i<NodesToShow.Count;i++)
			{
				objBallons[i].guiTexture.enabled = true;
				objBallons[i].guiText.text = ((ConversationNodeClass)NodesToShow[i]).sFirstOption;
				objBallons[i].guiText.enabled = true;
				showState = ShowState.CLICK_WAIT;
			}
		}
		else if (showState == ShowState.CLICK_WAIT)//Comprova que hi hagi un clic en una de les opcions mostrades
		{
			if(/*Input.GetMouseButtonDown(0)*/Input.GetMouseButtonUp(0))
			{
				Vector3 screenPoint = Input.mousePosition;
				for(int i=0;i<NodesToShow.Count;i++)
				{
					if(objBallons[i].guiTexture.HitTest(screenPoint))
					{
						Debug.Log (i +" selected");
						CurNode=(ConversationNodeClass)NodesToShow[i];
						skip=false;
						dialogState=DialogState.EXEC;
						gettime=Time.time;
						DisableBalloons();
						showState = ShowState.SHOW;
						if(CurNode.preAction!=""){
							CurNode.GetRootNode().BroadcastMessage(CurNode.preAction);
						}
						if(CurNode.clipAudio){
							playerAS.PlayOneShot(CurNode.clipAudio);
						}
						break;
					}
				}
			}
		}
		//WTF!?!?!?!?! Metode HitTest() del GUITexture!!!!!
	}
	
	/*
	 * Executa el node seleccionat. Sexecuta la accio previa al node, em mostra el node amb la bafarada de parlar
	 * segons sigui l'alien, l'altre interlocutor o el narrador qui digui la frase. S'executa tambe el clip d'audio
	 * que pertany a aquell troç de conversa
	 */
	void ExecuteNode(ConversationNodeClass Node){
		float Balloonx, Balloony, Balloondx, Balloondy;
		GUIStyle style = style_narrator;
		
		Balloonx = 0.005f;
		Balloony = 0.01f;

		Balloondx = 0.24f;
		Balloondy = 0.44f;


		if (Node.sSpeaker == ConversationNodeClass.speaker.ALIEN) {
			style = style_alien;
		} else if (Node.sSpeaker == ConversationNodeClass.speaker.THEOTHER) {
			style = style_theother;
			Balloonx = 0.74f;
		} else if (Node.sSpeaker == ConversationNodeClass.speaker.NARRATOR) {
			style = style_narrator;
			Balloony=1f-Balloondy;
			//Balloondx = Balloondx*2;
			//Balloony = 0.7f;
			//Balloondy = 0.2f;
		}

   		if (gettime + Node.fSeconds > Time.time && !(Input.GetMouseButtonUp(0) && skip)) {                                                                                                                                                                                                                                                                                                                                                                                                     

			GUI.Button (new Rect (Balloonx * Screen.width, Balloony * Screen.height, Balloondx * Screen.width, Balloondy * Screen.height), 
			            Node.sFirstOption, style);
			if(Input.GetMouseButtonDown(0)){
				skip=true;
			}
		}
		else 	//Time is over
		{
			skip=false;
			playerAS.Stop();
			if (CurNode.cncArray.Length==0){	//EOC
				dialogState=DialogState.END;
			}
			else{
				dialogState=DialogState.EVAL;
				NodesToEvaluate=CurNode.cncArray;
			}
			if(CurNode.postAction!=""){
				CurNode.GetRootNode().BroadcastMessage(CurNode.postAction);
			}
		}
	}
	
	/*
	 * Finalitza la conversa, para la maqina d'estats de la conversa
	 */
	void EndDialog(){
		
		Debug.Log(Camera.current);
		if(CCScript.IsIn()){
			CCScript.TransferOut();
		}
		dialogState = DialogState.NONE;
		this.enabled = false;
	}
	
	/*
	 *Deshabilita les bafarades de opcions 
	 */
	void DisableBalloons()
	{
		for (int i = 0; i<4; i++) 
		{
			objBallons[i].guiText.enabled = false;
			objBallons[i].guiTexture.enabled=false;
		}	
	}
	
	void OnGUI() {
		switch (dialogState) {
		case DialogState.INIT:
			InitDialog();
			break;
		case DialogState.EVAL:
			EvaluateNodes(NodesToEvaluate);
			break;
		case DialogState.EXEC:
			ExecuteNode (CurNode);
			break;
		case DialogState.SHOW:
			ShowNodes(NodesToShow);
			break;
		case DialogState.END:
			//executar el que sigui que calgui pel joc!
			EndDialog();
			break;
		case DialogState.NONE:
			//????
			break;
		}
	}
	
	/*
	 * Metode per afegir els nodes principals d'una arbre de conversa.
	 * Previ a iniciar la conversa
	 */
	public void SetRootNodes(ConversationNodeClass[] rootNodes){
		NodesToEvaluate = rootNodes;
	}
	
	/*
	 * Metode per iniciar la maquina destats, la conversa
	 */
	public void Init()
	{
		this.enabled = true;
		dialogState = DialogState.INIT;
	}
	
	bool CheckCustom(ConversationNodeClass node)
	{
		bool ret = false;
		switch (node.cCharacter) {
		case ConversationNodeClass.costume.ALL:
			ret = true;
			break;
		case ConversationNodeClass.costume.NAKED:
			if(inventoryControl.GetCurrentCostume()==Custom.NAKED) ret = true;
			break;
		case ConversationNodeClass.costume.MS_FORTUNE:
			if(inventoryControl.GetCurrentCostume()==Custom.MS_FORTUNE ) ret = true;
			break;
		case ConversationNodeClass.costume.JAPANESE:
			if(inventoryControl.GetCurrentCostume()==Custom.JAPANESE ) ret = true;
			break;
			
		}
		return ret;
	}
	
	bool CheckConditional(ConversationNodeClass node)
	{
		bool ret = false;
		if (node.ShowNode==ConversationNodeClass.show.ALWAYS){
			ret = true;
		}
		else if (node.ShowNode==ConversationNodeClass.show.IF&&gs.GetBool(node.var)==true){
			ret = true;
		}
		else if (node.ShowNode==ConversationNodeClass.show.IF_NOT&&gs.GetBool(node.var)==false){
			ret = true;
		}
		return ret;
	}
}





