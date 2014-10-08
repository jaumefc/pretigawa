using UnityEngine;
using System.Collections;

public class playAnimation : MonoBehaviour {

	public GameObject characterToPlayIt;
	public AnimationClip animacio;

	public AnimationClip goBackToAnimation;

	private Animation anims;

	void Start () {
	}

	public void playIt(){
		if (characterToPlayIt != null && animacio != null) {
			anims = characterToPlayIt.GetComponent<Animation>();
			anims.Play(animacio.name);
			StartCoroutine(backToIdle(animacio.length));
		}
	}

	IEnumerator backToIdle(float time)
	{
		yield return new WaitForSeconds (time);
		if (goBackToAnimation == null)
				anims.Play ("Idle");
		else
				anims.Play (goBackToAnimation.name);
	}

	void Update () {	
	}
}
