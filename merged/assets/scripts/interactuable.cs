using UnityEngine;
using System.Collections;

//#pragma strict

public class interactuable : MonoBehaviour {

	public GameObject[] Actions;

	public float Radius = 0.2f;

	private float r;
	private float ratio;
	private int iconsize;

	private float width;
	private float height;
	private float posx;
	private float posy;

	public GameObject objecteBo;


	public erronis[] objectesErronis;
	public ConversationTreeClass defaultNode;
	
	void Start () {
		object outMaterial = Resources.Load<Material>("Materials/out");
		AnimationClip anim = Resources.Load<AnimationClip>("Animations/outline");
		RuntimeAnimatorController animControl = Resources.Load<RuntimeAnimatorController>("Animations/outlineController");
		Debug.Log(outMaterial);
		Renderer [] renderers = gameObject.GetComponentsInChildren<Renderer>();
		for(int i =0; i< renderers.Length;i++){
			//Afegim el material per fer outliner
//			Material[] mats = new Material[renderers[i].materials.Length+1];
//			renderers[i].materials.CopyTo(mats,0);
//			mats.SetValue(outMaterial,renderers[i].materials.Length);
//			renderers[i].materials = mats;
//			//Afegim animacio
//			renderers[i].gameObject.AddComponent<Animation>();
//			renderers[i].gameObject.animation.wrapMode = WrapMode.Loop;
//			renderers[i].gameObject.animation.playAutomatically = true;
//			renderers[i].gameObject.animation.clip = anim;
////			renderers[i].gameObject.animation.AddClip(anim,"oultine");
//			renderers[i].gameObject.animation.Play();
//			renderers[i].gameObject.AddComponent<Animator>();
//			Animator controller = renderers[i].gameObject.GetComponent<Animator>();
//			controller.runtimeAnimatorController = animControl;
			//renderers[i].gameObject.
			//renderers[i].gameObject.animation.AddClip//
			/////
			/// 
			Material mat = new Material(Shader.Find("VertexLit Funky"));
			mat.SetTexture("_MainTex",renderers[i].material.GetTexture("_MainTex"));
			renderers[i].material = mat;
			/////
		}
		width = Display.main.systemWidth;
		height = Display.main.systemHeight;
		//ratio = width/height;
		HideMenu();
		r = Radius * Display.main.systemHeight;
		
		iconsize = Mathf.Min (Mathf.CeilToInt(height*0.118f), 128);
	}

	void Update () {

	}

	public void ShowMenu (Vector3 screenPos) {
		posy = screenPos.y;
		posx = screenPos.x;
		float angle = 225 / (Actions.Length + 1);
		for(int i=1;i<=Actions.Length;i++) {
			float x = r*Mathf.Cos((202.5f-(angle*i))*Mathf.Deg2Rad);
			float y = r*Mathf.Sin((202.5f-(angle*i))*Mathf.Deg2Rad);

			Actions[i-1].transform.position = new Vector3(posx / width,posy / height,0);
			Actions[i-1].guiTexture.pixelInset = new Rect(x - iconsize/2,y - iconsize/2,iconsize,iconsize);
			Actions[i-1].SetActive(true);
		}
	}

	public void HideMenu () {
		for(int i=0;i<Actions.Length;i++)
		{
			Actions[i].SetActive(false);
		}
	}

	public Vector3 GetScreenPosition() {
		return new Vector3(posx,posy,0);
	}
	
	public bool comprobarInteraccio(GameObject origen){
		DialogCameraScript DCScript = GameObject.Find ("DialogLayout").GetComponent<DialogCameraScript>();

		Debug.Log ("Origen de combinacio: " + origen);

		ConversationTreeClass nodeALlencar = defaultNode;
		
		if(objecteBo==origen)return true;
		else{
			foreach(erronis erroni in objectesErronis){
				foreach(GameObject objecte in erroni.objectes){
					if(objecte == origen){
						nodeALlencar = erroni.frase;
						//DCScript.SetRootNodes(nodeALlencar.rootNodes);
						//DCScript.Init();
						DCScript.Init(nodeALlencar);
						DCScript.enabled = true;
						return false;
					}
				}
			}
			//DCScript.SetRootNodes(nodeALlencar.rootNodes);
			//DCScript.Init();
			DCScript.Init(nodeALlencar);
			DCScript.enabled = true;
			return false;
		}
	}

}
