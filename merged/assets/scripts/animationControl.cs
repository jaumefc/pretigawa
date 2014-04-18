using UnityEngine;
using System.Collections;

public class animationControl : MonoBehaviour
{
	
	public AnimationClip idleAnimation;
	public AnimationClip walkAnimation;
	public AnimationClip runAnimation;
	public AnimationClip jumpPoseAnimation;

	
	private NavMeshAgent navi;

	CharacterState _characterState;

	
	private Animation _animation;

	
	enum CharacterState {
		Idle = 0,
		Walking = 1,
		Trotting = 2,
		Running = 3,
		Jumping = 4,
	}
		// Use this for initialization
		void Start ()
	{
		_characterState = CharacterState.Idle;
		navi = GetComponent<NavMeshAgent>();
		_animation = GetComponent<Animation>();
	
		}
	
		// Update is called once per frame
		void Update ()
	{
		UpdateState ();
		PlayAnimation ();
	}

	private void PlayAnimation(){
		if(_animation) {
			switch(_characterState){
			case CharacterState.Idle:
				_animation.CrossFade(idleAnimation.name);
				break;
			case CharacterState.Walking:
				_animation[walkAnimation.name].speed = Mathf.Clamp(navi.velocity.magnitude, 0.0f, 1.8f);
				_animation.CrossFade(walkAnimation.name);	
				break;
			case CharacterState.Running:
				_animation[runAnimation.name].speed = Mathf.Clamp(navi.velocity.magnitude, 0.0f, 1.0f);
				_animation.CrossFade(runAnimation.name);	
				break;
			}
		}
	}

	private void UpdateState(){
		if( navi.velocity.sqrMagnitude < 0.1 )
		{
			_characterState = CharacterState.Idle;
		}
		else
		{
			_characterState = CharacterState.Walking;
		}
	}
}

