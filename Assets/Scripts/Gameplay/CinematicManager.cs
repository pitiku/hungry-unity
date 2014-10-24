using UnityEngine;
using System.Collections;

public class CinematicManager : SingletonMonoBehaviour<CinematicManager> 
{
	Animator animator;
	bool finished = true;

	protected override void OnAwake() 
	{
		animator = GetComponent<Animator>();
		animator.enabled = false;
	}

	public void CinematicFinished()
	{
		animator.enabled = false;
		finished = true;
	}

	public bool IsFinished()
	{
		return finished;
	}

	public void StartCinematic(string _cinematic)
	{
		finished = false;
		animator.enabled = true;
		animator.SetTrigger(_cinematic);
	}
}
