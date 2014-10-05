using UnityEngine;
using System.Collections;

public class Baby : MonoBehaviour {

	Animator animator;

	public Transform mouth;

	void Start () 
	{
		animator = GetComponent<Animator>();
	}
	
	void Update () 
	{
	}

	public void Eat(bool _bSuccess)
	{
		if(_bSuccess)
		{
			animator.SetBool("Eat_Success", true);
		}
		else
		{
			animator.SetBool("Eat_Fail", true);
		}
	}
}
