using UnityEngine;
using System.Collections;

public class MenuPage : MonoBehaviour 
{
	protected Menu menu;
	MenuItem[] items;

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
		OnUpdate();
	}

	public virtual void OnStart()
	{
	}

	public virtual void OnUpdate()
	{
	}

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
}
