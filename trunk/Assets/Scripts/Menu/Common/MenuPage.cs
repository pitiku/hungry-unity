using UnityEngine;
using System.Collections;

public class MenuPage : MonoBehaviour 
{
	protected Menu menu;
	MenuItem[] items;
	bool animating = false;

	Animator animator;
	public bool HasInAnim = false;
	public bool HasOutAnim = false;

	void Awake()
	{
		animator = GetComponent<Animator>();
		if(animator) animator.enabled = false;
	}

	void Start ()
	{
		items = FindObjectsOfType<MenuItem>();
		foreach(MenuItem item in items)
		{
			item.SetPage(this);
		}

		OnStart();
	}

	void Update()
	{
		if(!animating)
		{
			OnUpdate();
		}
	}

	public virtual void OnStart(){}
	public virtual void OnUpdate(){}
	public virtual void OnSetPage(){}

	public void SetMenu(Menu _menu)
	{
		menu = _menu;
	}

	void LateUpdate()
	{
		foreach(MenuItem item in items)
		{
			item.ResetPressed();
		}
	}

	public void Animate(string _name)
	{
		if(animator)
		{
			animating = true;
			animator.enabled = true;
			animator.SetTrigger(_name);
		}
	}

	public void AnimateIn()
	{
		if(animator && HasInAnim)
		{
			animating = true;
			animator.enabled = true;
			animator.SetTrigger("In");
		}
	}

	public void AnimateOut()
	{
		if(animator && HasOutAnim)
		{
			animating = true;
			animator.enabled = true;
			animator.SetTrigger("Out");
		}
	}
	
	public void FinishAnimation()
	{
		animating = false;
		if(animator) animator.enabled = false;
	}
	
	public bool IsAnimationFinished()
	{
		return !animating;
	}
}
