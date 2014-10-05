using UnityEngine;
using System.Collections;

public class lifeController : MonoBehaviour {

	public float totalLife = 10.0f;
	public float life;
	private bool regenerating = false;
	private bool imDead = false;

	public ParticleSystem pinyuParticles;

	private Animation anims;
	private GameObject mainChar;
	private mouseControl mc;
	private CharacterController chc;
	private animationControl animcon;

	private saturationController satC; 

	void Start () {
		mainChar = GameObject.Find ("Player");
		mc = mainChar.GetComponent<mouseControl>();
		chc = mainChar.GetComponent<CharacterController>();
		animcon = mainChar.GetComponent<animationControl> ();
		anims = mainChar.GetComponent<Animation> ();
		satC = this.GetComponent<saturationController> ();
		life = totalLife;
	}
	

	void Update () {
	}

	public void takeOutLife(float lifeToTake){
		if (imDead)return;
		CancelInvoke ("startRegen");
		regenerating = false;
		life -= lifeToTake;
		pinyuParticles.Play ();
		transform.FindChild("audioPinyu").audio.Play();

		if (life <= 0) {
			characterDie ();
		} else {
			satC.changeSaturation (-lifeToTake/10);
			Invoke ("startRegen", 3.0f);
		}
	}

	private void startRegen(){
		regenerating = true;
		regenerateLife ();
	}

	private void regenerateLife(){
		if (imDead)return;
		if (regenerating) {
			life += 0.5f;
			satC.changeSaturation (0.05f);

			if(life >= totalLife){
				life = totalLife;
				regenerating = false;
				pinyuParticles.Stop();
			}
			Invoke("regenerateLife", 0.5f);
		}
	}

	public void characterDie(){
		imDead = true;
		animcon.enabled = false;
		mc.enabled = false;
		chc.enabled = false;
		anims.Play ("Die");
		Invoke ("resetScene", 3.0f);
	}

	private void resetScene(){
		Application.LoadLevel ("restart");
	}
}
