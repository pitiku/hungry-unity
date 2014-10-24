using UnityEngine;
using System.Collections;

public class CinematicManager : SingletonMonoBehaviour<CinematicManager> 
{
	Animator animator;
	bool finished = true;

	protected override void OnAwake() 
	{
		animator = GetComponent<Animator>();
	}

	public void CinematicFinished()
	{
		finished = true;
	}

	public bool IsFinished()
	{
		return finished;
	}

	public void StartCinematic(string _cinematic)
	{
		finished = false;
		animator.SetTrigger(_cinematic);
	}
}
