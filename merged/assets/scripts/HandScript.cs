using UnityEngine;
using System.Collections;

public class HandScript : MonoBehaviour {


	//public Texture ImgLook, ImgTake, ImgTalk, ImgUse;
	//public float left1, top1, size1;
	//public float left2, top2;
	//public float left3, top3;
	//public float left4, top4;


	//public float vx;
	//public float vy;

	//public GUIStyle style;

	float buttonleft, buttontop;


	float screenratio;




	//
	Camera cam;
	interactuable _interactuable;
	public float Radius = 0.2f;
	float r;
	int iconsize;




	// Use this for initialization
	void Start () {

		screenratio = (float)(Screen.width) / (float)(Screen.height);

		Debug.Log (Screen.width + ", " + Screen.height);


		//
		cam=Camera.main;

		r = Radius * Display.main.systemHeight;

		iconsize = Mathf.Min (Mathf.CeilToInt((float)(Screen.height)*0.118f), 128);

	}
	


		
	void Update() {

		/*
		if (lookEnabled){
			GUI.Button (new Rect (left1 * Screen.width, top1 * Screen.height, size1 * Screen.height, size1 * Screen.height), new GUIContent ("Look", ImgLook), style);
		}

		if (iconsEnabled){
			GUI.Button (new Rect (left2 * Screen.width, top2 * Screen.height, size1 * Screen.height, size1 * Screen.height), new GUIContent ("Take", ImgTake), style);
			GUI.Button (new Rect (left3 * Screen.width, top3 * Screen.height, size1 * Screen.height, size1 * Screen.height), new GUIContent ("Talk", ImgTalk), style);
			GUI.Button (new Rect (left4 * Screen.width, top4 * Screen.height, size1 * Screen.height, size1 * Screen.height), new GUIContent ("Use", ImgUse), style);
		}
		*/


		//
		ReadInput ();

	}



	void ReadInput () {

		if(Input.GetMouseButtonDown(0))
		{
			Ray ray = cam.ScreenPointToRay(Input.mousePosition);
			int count = Input.touchCount;	
			if( count == 1 )
			{
				Touch touch = Input.GetTouch(0);
				ray = cam.ScreenPointToRay( new Vector3( touch.position.x, touch.position.y ) );
			}

			RaycastHit hit;
			if( Physics.Raycast(ray, out hit))
			{
				Debug.Log(hit.collider.gameObject);
				Debug.Log(hit.collider.gameObject.GetComponent<interactuable>());
				if(hit.collider.gameObject.GetComponent<interactuable>())
				{
					Debug.Log("Interactuable collide!!!");
					Vector3 screenPos = cam.WorldToScreenPoint(hit.collider.gameObject.transform.position);
					ShowMenu(screenPos);

				}

			}
		}
		else if(Input.GetMouseButtonUp(0))
		{

			if(_interactuable!=null) {
				float cx= Input.mousePosition.x;
				float cy= Input.mousePosition.y;
				Vector3 cPos = Input.mousePosition - _interactuable.GetScreenPosition();
				for(int i=0;i<_interactuable.Actions.Length;i++) {
					//Comprovar distancia del puntero al las opciones
					Vector3 aPos = new Vector3(_interactuable.Actions[i].guiTexture.pixelInset.x+32,
					                           _interactuable.Actions[i].guiTexture.pixelInset.y+32,0);
					float aDist = Vector3.Distance(aPos,cPos);
					//Debug.Log("Distance:"+aDist);
					if(aDist<32)
					{
						_interactuable.Actions[i].GetComponent<Action>().Do();
					}
				} 
				_interactuable = null;
			}
			//Debug.Log("ButtonUp");
			
		}
		else if(Input.GetMouseButton(0))
		{

			if(_interactuable!=null) {
				Ray rray = cam.ScreenPointToRay(Input.mousePosition);
				RaycastHit rhit;
				Physics.Raycast(rray,out rhit);

				Vector3 cPos = Input.mousePosition - _interactuable.GetScreenPosition();
				for(int i=0;i<_interactuable.Actions.Length;i++) {
					Vector3 aPos = new Vector3(_interactuable.Actions[i].guiTexture.pixelInset.x+32,
					                           _interactuable.Actions[i].guiTexture.pixelInset.y+32,0);
					float aDist = Vector3.Distance(aPos,cPos);
					if(aDist<32)
					{
						_interactuable.Actions[i].guiTexture.color = new Color(0.7f,0.7f,0.7f,0.5f);
					}
					else
					{
						_interactuable.Actions[i].guiTexture.color = new Color(0.5f,0.5f,0.5f,0.5f);
					}
				}
			}
		}

	}

	public void ShowMenu (Vector3 screenPos) {
		float posy = screenPos.y;
		float posx = screenPos.x;
		float angle = 225 / 4;
		for(int i=1;i<=4;i++) {
			float x = r*Mathf.Cos((202.5f-(angle*i))*Mathf.Deg2Rad);
			float y = r*Mathf.Sin((202.5f-(angle*i))*Mathf.Deg2Rad);
			
			_interactuable.Actions[i-1].transform.position = new Vector3(posx / Screen.width,posy / Screen.height,0);
			_interactuable.Actions[i-1].guiTexture.pixelInset = new Rect(x - iconsize/2,y - iconsize/2,iconsize,iconsize);
			_interactuable.Actions[i-1].SetActive(true);
		}
	}


}
