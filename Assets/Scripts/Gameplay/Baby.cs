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

	public void StopAnimation()
	{
		animator.enabled = false;
		if(childAnimator)
		{
			childAnimator.enabled = false;
		}
	}

	public void Idle()
	{
		bEating = false;
		animator.enabled = true;
		animator.SetTrigger("Idle");
		if(childAnimator)
		{
			childAnimator.enabled = true;
			childAnimator.SetTrigger("Idle");
		}
	}

	public void JustEat()
	{
		animator.enabled = true;
		animator.SetTrigger("Eat");
		if(childAnimator)
		{
			childAnimator.enabled = true;
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

		animator.enabled = true;
		animator.SetTrigger(trigger);
		if(childAnimator)
		{
			childAnimator.enabled = true;
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
		return PlayerData.Instance.babyUnlocked[(int)baby];
	}
	
	public bool IsBought()
	{
		return PlayerData.Instance.babyBought[(int)baby];
	}
}
