using UnityEngine;
using System.Collections;

public class MainMenu : MenuPage 
{
	public MenuItem playButton;
	public MenuItem optionsButton;
	public MenuItem shopButton;
	public MenuItem pedestalButton;

	public override void OnStart () 
	{
		CoinsCounter.Instance.gameObject.SetActive(false);
	}
	
	public override void OnUpdate () 
	{
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
			MenuManager.Instance.SetPage(MenuManager.Instance.optionsPage);
		}
		else if(shopButton.IsJustPressed())
		{
			CoinsCounter.Instance.gameObject.SetActive(true);
			MenuManager.Instance.SetPage(MenuManager.Instance.shopPage);
		}
		else if(pedestalButton.IsJustPressed())
		{
			MenuManager.Instance.SetPage(MenuManager.Instance.museumPage);
		}
	}
}
