using UnityEngine;
using System.Collections;

public class AnimatedObject : MonoBehaviour 
{
	Animator animator;
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
		if(animator)
		{
			animator.enabled = false;
		}
	}

	void AnimationFinished()
	{
		animating = false;
		StopAnimator();
	}

	public bool IsFinished()
	{
		return !animating;
	}
}
