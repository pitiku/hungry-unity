using UnityEngine;
using System.Collections;

public class OptionsMenu : MenuPage 
{
	public MenuItem musicItem;
	public SpriteRenderer musicCheckbox;

	public MenuItem backButton;

	public override void OnStart () 
	{
		musicCheckbox.enabled = PlayerData.Instance.option_music;
	}
	
	public override void OnUpdate () 
	{
		if(musicItem.IsJustPressed())
		{
			PlayerData.Instance.option_music = !PlayerData.Instance.option_music;
			musicCheckbox.enabled = PlayerData.Instance.option_music;
		}
		else if(backButton.IsJustPressed() || Input.GetKeyDown(KeyCode.Escape))
		{
			PlayerData.Instance.Save();
			MenuManager.Instance.SetPage(MenuManager.Instance.mainPage);
		}
	}
}
