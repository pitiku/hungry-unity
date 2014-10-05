using UnityEngine;
using System.Collections;

public class OptionsMenu : MenuPage 
{
	public MenuItem musicItem;
	public SpriteRenderer musicCheckbox;

	public MenuItem backButton;

	MenuManager menuManager;
	
	public override void Start () 
	{
		base.Start();
	
		menuManager = FindObjectOfType<MenuManager>();

		musicCheckbox.enabled = PlayerData.music;
	}
	
	public override void Update () 
	{
		base.Update();

		if(musicItem.IsJustPressed())
		{
			PlayerData.music = !PlayerData.music;
			musicCheckbox.enabled = PlayerData.music;
		}
		else if(backButton.IsJustPressed() || Input.GetKeyDown(KeyCode.Escape))
		{
			PlayerData.Save();
			menuManager.SetPage(menuManager.mainPage);
		}
	}
}
