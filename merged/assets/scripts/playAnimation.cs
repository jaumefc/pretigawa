using UnityEngine;
using System.Collections;

public class playAnimation : MonoBehaviour {

	public GameObject characterToPlayIt;
	public GameObject otherCharSpeaking;
	public AnimationClip animacio;

	public playAnimation previousAnim;
	public AnimationClip goBackToAnimation;

	private Animation anims;

	void Start () {
	}

	public void playIt(){
		if (characterToPlayIt != null && animacio != null) {
			if(previousAnim!=null)previousAnim.StopAllCoroutines();
			anims = characterToPlayIt.GetComponent<Animation>();
			anims.Play(animacio.name);
			if(otherCharSpeaking)otherCharSpeaking.GetComponent<Animation>().Play("Idle");
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
