using UnityEngine;
using System.Collections;

public class GlobusRandom : MonoBehaviour {

	private Vector3 initialScale;
	private float randomOffset = 0.2f;

	void Start () {
		initialScale = transform.localScale;

		float randomNewScale = Random.Range ((-1*randomOffset), randomOffset);
		int rndColor = Random.Range (0, 6);
		gameObject.renderer.material.color = chooseColor (rndColor);


		Vector3 newScale = new Vector3(initialScale.x+randomNewScale, initialScale.y+randomNewScale, initialScale.z+randomNewScale); 

		transform.localScale = newScale;
	}

	private Color chooseColor(int rndColor){
		switch (rndColor) {
			case 0:
				return Color.yellow;
			break;
			case 1:
				return Color.blue;
			break;
			case 2:
				return Color.cyan;
			break;
			case 3:
				return Color.magenta;
			break;
			case 4:
				return Color.green;
			break;
			case 5:
				return Color.red;
			break;
			default:
				return Color.red;
			break;
		}
	}

	void Update () {
	}
}
