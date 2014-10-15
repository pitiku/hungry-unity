using UnityEngine;
using System.Collections;

public class AnimatedObject : MonoBehaviour {

	protected Animator animator;
	bool animating = false;

	void Awake()
	{
		animator = GetComponent<Animator>();
	}

	public void StartAnimation(string _animation)
	{
		if(animator)
		{
			animator.enabled = true;
			animating = true;
			animator.SetTrigger(_animation);
		}
	}

	public void StopAnimator()
	{
		animator.enabled = false;
	}

	void AnimationFinished()
	{
		animating = false;
	}

	public bool IsFinished()
	{
		return !animating;
	}
}
