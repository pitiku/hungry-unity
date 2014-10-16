using UnityEngine;
using System.Collections;

public class Baby : MonoBehaviour 
{
	public GameConstants.eBabies baby;
	public Transform mouth;
	public Animator childAnimator;

	Animator animator;
	bool bEating = false;

	void Awake() 
	{
		animator = GetComponent<Animator>();
	}
	
	void Update () 
	{
	}

	public void Idle()
	{
		bEating = false;
		animator.SetTrigger("Idle");
		if(childAnimator)
		{
			childAnimator.SetTrigger("Idle");
		}
	}

	public void Eat(bool _bSuccess)
	{
		bEating = true;
		if(_bSuccess)
		{
			animator.SetTrigger("Eat_Success");
			if(childAnimator)
			{
				childAnimator.SetTrigger("Eat_Success");
			}
		}
		else
		{
			animator.SetTrigger("Eat_Fail");
			if(childAnimator)
			{
				childAnimator.SetTrigger("Eat_Fail");
			}
		}
	}

	public void FinishEating()
	{
		bEating = false;
	}

	public bool IsEating()
	{
		return bEating;
	}

	public bool IsUnlocked()
	{
		return PlayerData.Instance.babies[(int)baby];
	}
}
