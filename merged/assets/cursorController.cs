using UnityEngine;
using System.Collections;

public class cursorController : MonoBehaviour {

	public Texture2D cursorTexture;
	public CursorMode cursorMode = CursorMode.Auto;
	public Vector2 hotSpot = Vector2.zero;

	void Start(){
		Invoke ("changeCursor", 0.1f);
	}

	private void changeCursor () {
		Cursor.SetCursor (cursorTexture, hotSpot, cursorMode);
	}
	
}
