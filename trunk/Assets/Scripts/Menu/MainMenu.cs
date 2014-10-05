using UnityEngine;
using System.Collections;

public class MainMenu : MenuPage 
{
	public MenuItem playButton;
	public MenuItem optionsButton;
	public MenuItem shopButton;
	public MenuItem pedestalButton;

	MenuManager menuManager;

	public override void Start () 
	{
		base.Start();
	
		menuManager = FindObjectOfType<MenuManager>();
	}
	
	public override void Update () 
	{
		base.Update();
		
		if (Input.GetKeyDown(KeyCode.Escape)) 
		{
			Application.Quit(); 
		}

		if(playButton.IsJustPressed())
		{
			Application.LoadLevel("Level");
		}
		else if(optionsButton.IsJustPressed())
		{
			menuManager.SetPage(menuManager.optionsPage);
		}
		else if(shopButton.IsJustPressed())
		{
			menuManager.SetPage(menuManager.shopPage);
		}
		else if(pedestalButton.IsJustPressed())
		{
			menuManager.SetPage(menuManager.museumPage);
		}
	}
}
