using UnityEngine;
using System.Collections;

public class ShopPage : MenuPage 
{
	public Pushable babies;
	public Pushable upgrades;
	public Pushable powerups;
	public Pushable backButton;
	
	public override void OnStart () 
	{
	}

	public override void OnSetPage ()
	{
		CoinsCounter.Instance.AnimateIn();
	}

	public override void OnUpdate () 
	{
		if(babies.IsJustPressed())
		{
			CoinsCounter.Instance.AnimateOut();
			MenuManager.Instance.SetPage(MenuManager.Instance.babiesPage);
		}
		else if(upgrades.IsJustPressed())
		{
			CoinsCounter.Instance.AnimateOut();
			MenuManager.Instance.SetPage(MenuManager.Instance.upgradesPage);
		}
		else if(powerups.IsJustPressed())
		{
			CoinsCounter.Instance.AnimateOut();
			MenuManager.Instance.SetPage(MenuManager.Instance.powerupsPage);
		}
		else if(backButton.IsJustPressed() || Input.GetKeyDown(KeyCode.Escape))
		{
			CoinsCounter.Instance.AnimateOut();
			MenuManager.Instance.SetPage(MenuManager.Instance.mainPage, true, false);
		}
	}
}
