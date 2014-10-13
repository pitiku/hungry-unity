﻿using UnityEngine;
using System.Collections;

public class AnimatedObject : MonoBehaviour {

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
			animating = true;
			animator.SetTrigger(_animation);
		}
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
