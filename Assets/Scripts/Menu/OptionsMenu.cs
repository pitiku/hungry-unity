using UnityEngine;
using System.Collections;

public class OptionsMenu : MenuPage 
{
	public MenuItem musicItem;
	public SpriteRenderer musicCheckbox;
	public MenuItem soundItem;
	public SpriteRenderer soundCheckbox;
	public MenuItem accelerometerItem;
	public SpriteRenderer accelerometerCheckbox;
	public MenuItem statsItem;

	public MenuItem backButton;

	public override void OnStart () 
	{
		musicCheckbox.enabled = PlayerData.Instance.option_music;
		soundCheckbox.enabled = PlayerData.Instance.option_sound;
		accelerometerCheckbox.enabled = PlayerData.Instance.option_accelerometer;
	}
	
	public override void OnUpdate () 
	{
		if(musicItem.IsJustPressed())
		{
			musicCheckbox.enabled = !musicCheckbox.enabled;
			PlayerData.Instance.option_music = musicCheckbox.enabled;
			MenuManager.Instance.GetComponent<AudioSource>().enabled = musicCheckbox.enabled;
		}
		else if(soundItem.IsJustPressed())
		{
			PlayerData.Instance.option_sound = !PlayerData.Instance.option_sound;
			soundCheckbox.enabled = PlayerData.Instance.option_sound;
		}
		else if(accelerometerItem.IsJustPressed())
		{
			PlayerData.Instance.option_accelerometer = !PlayerData.Instance.option_accelerometer;
			accelerometerCheckbox.enabled = PlayerData.Instance.option_accelerometer;
		}
		else if(statsItem.IsJustPressed())
		{
			((StatsPage) MenuManager.Instance.statsPage).UpdateStats();
			MenuManager.Instance.SetPage(MenuManager.Instance.statsPage);
		}
		else if(backButton.IsJustPressed() || Input.GetKeyDown(KeyCode.Escape))
		{
			PlayerData.Instance.Save();
			MenuManager.Instance.SetPage(MenuManager.Instance.mainPage, true, false);
		}
	}
}
