using UnityEngine;
using System.Collections;

public class OptionsPage : MenuPage 
{
	public Pushable musicItem;
	public SpriteRenderer musicCheckbox;
	public Pushable soundItem;
	public SpriteRenderer soundCheckbox;
	public Pushable accelerometerItem;
	public SpriteRenderer accelerometerCheckbox;
	public Pushable statsItem;

	public TextMesh accelerometerText;
	public TextMesh accelerometerText_shadow;

	public Pushable backButton;

	public override void OnStart () 
	{
		musicCheckbox.enabled = PlayerData.Instance.option_music;
		soundCheckbox.enabled = PlayerData.Instance.option_sound;
		accelerometerCheckbox.enabled = PlayerData.Instance.option_accelerometer;
	}

	public override void OnSetPage ()
	{
		accelerometerItem.enabled = PlayerData.Instance.upgrade_accelerometer;

		Color gray = Color.gray;
		gray.a = 0.5f;
		accelerometerText.color = PlayerData.Instance.upgrade_accelerometer ? Color.white : gray;
		accelerometerText_shadow.color = PlayerData.Instance.upgrade_accelerometer ? Color.black : gray;
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
		else if(accelerometerItem.enabled && accelerometerItem.IsJustPressed())
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
