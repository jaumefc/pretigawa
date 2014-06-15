using UnityEngine;
using System.Collections;

public class tirolinaPath : MonoBehaviour {
	
	public Transform[] path;
	
	//-->> 0 inici // 1 final
	public float t = 0;
	
	// Update is called once per frame
	void Update () {
		transform.position = Spline.MoveOnPath(path, transform.position, ref t, 10.0f);
	}
}
