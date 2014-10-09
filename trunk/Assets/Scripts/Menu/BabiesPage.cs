using UnityEngine;
using System.Collections;

public class BabiesPage : MenuPage 
{
	public MenuItem backButton;

	public Baby[] babies;
	
	public override void OnStart () 
	{
	}

	public override void OnSetPage()
	{
		CoinsCounter.Instance.AnimateIn();
	}
	
	public override void OnUpdate () 
	{
		if(backButton.IsJustPressed() || Input.GetKeyDown(KeyCode.Escape))
		{
			CoinsCounter.Instance.AnimateOut();
			MenuManager.Instance.SetPage(MenuManager.Instance.shopPage);
		}
	}
}
