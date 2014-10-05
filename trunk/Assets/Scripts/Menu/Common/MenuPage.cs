using UnityEngine;
using System.Collections;

public class MenuPage : MonoBehaviour 
{
	protected Menu menu;
	MenuItem[] items;

	public virtual void Start ()
	{
		items = FindObjectsOfType<MenuItem>();
		foreach(MenuItem item in items)
		{
			item.SetPage(this);
		}
	}

	public virtual void Update()
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
