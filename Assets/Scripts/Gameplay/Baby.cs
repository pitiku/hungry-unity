using UnityEngine;
using System.Collections;

public class Baby : MonoBehaviour 
{
	public GameConstants.eBabies baby;
	public Transform mouth;
	public Animator childAnimator;

	Animator animator;
	bool bEating = false;

	public int hunger = 1;

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

	public void JustEat()
	{
		animator.SetTrigger("Eat");
		if(childAnimator)
		{
			childAnimator.SetTrigger("Eat");
		}
	}

	public void Eat(bool _bSuccess)
	{
		bEating = true;
		string trigger = "";
		if(_bSuccess)
		{
			hunger--;
			if(hunger <= 0)
			{
				trigger = "Eat_Success";
			}
			else
			{
				trigger = "Eat";
			}
		}
		else
		{
			trigger = "Eat_Fail";
		}

		animator.SetTrigger(trigger);
		if(childAnimator)
		{
			childAnimator.SetTrigger(trigger);
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
