using UnityEngine;
using System.Collections;

public class ShopPage : MenuPage 
{
	public MenuItem babies;
	public MenuItem upgrades;
	public MenuItem powerups;
	public MenuItem backButton;
	
	MenuManager menuManager;
	
	public override void Start () 
	{
		base.Start();
		
		menuManager = FindObjectOfType<MenuManager>();
	}
	
	public override void Update () 
	{
		base.Update();
		
		if(babies.IsJustPressed())
		{
		}
		else if(upgrades.IsJustPressed())
		{
		}
		else if(powerups.IsJustPressed())
		{
		}
		else if(backButton.IsJustPressed() || Input.GetKeyDown(KeyCode.Escape))
		{
			menuManager.SetPage(menuManager.mainPage);
		}
	}
}
