using UnityEngine;
using System.Collections;

public class MuseumPage : MenuPage 
{
	public MenuItem backButton;
	
	public override void OnStart () 
	{
	}
	
	public override void OnUpdate () 
	{
		if(backButton.IsJustPressed() || Input.GetKeyDown(KeyCode.Escape))
		{
			MenuManager.Instance.SetPage(MenuManager.Instance.mainPage, true, false);
		}
	}
}
