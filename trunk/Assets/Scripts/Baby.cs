using UnityEngine;
using System.Collections;

public class Baby : MonoBehaviour {

	public GameConstants.eBabies baby;
	public Transform mouth;

	Animator animator;

	void Start () 
	{
		animator = GetComponent<Animator>();
	}
	
	void Update () 
	{
	}

	public void Idle()
	{
		animator.SetTrigger("Idle");
	}

	public void Eat(bool _bSuccess)
	{
		if(_bSuccess)
		{
			animator.SetTrigger("Eat_Success");
		}
		else
		{
			animator.SetTrigger("Eat_Fail");
		}
	}

	public bool IsUnlocked()
	{
		return PlayerData.Instance.babies[(int)baby];
	}
}
