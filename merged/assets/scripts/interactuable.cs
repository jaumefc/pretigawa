using UnityEngine;
using System.Collections;

//#pragma strict

public class interactuable : MonoBehaviour {

	public GameObject[] Actions;
	public float r = 150;

	private float width;
	private float height;
	private float posx;
	private float posy;

	void Start () {
		width = Display.main.systemWidth;
		height = Display.main.systemHeight;
		HideMenu();
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
			Actions[i-1].guiTexture.pixelInset = new Rect(x - 32,y - 32,Actions[i-1].guiTexture.pixelInset.width,Actions[i-1].guiTexture.pixelInset.height);
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
}