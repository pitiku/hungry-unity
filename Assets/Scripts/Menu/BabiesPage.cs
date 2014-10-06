using UnityEngine;
using System.Collections;

public class BabiesPage : MenuPage 
{
	public MenuItem backButton;
	
	public override void OnStart () 
	{
	}
	
	public override void OnUpdate () 
	{
		if(backButton.IsJustPressed() || Input.GetKeyDown(KeyCode.Escape))
		{
			MenuManager.Instance.SetPage(MenuManager.Instance.shopPage);
		}
	}
}
