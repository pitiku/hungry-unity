using UnityEngine;
using System.Collections;

public class UpgradesPage : MenuPage 
{
	public MenuItem backButton;

	public MenuItem accelerometer;
	public MenuItem crown;
	public MenuItem rainbowPlus;
	public MenuItem rainbowPlusPlus;
	public MenuItem vacuum;
	public MenuItem gloves;

	public SpriteRenderer accelerometerTick;
	public SpriteRenderer crowmTick;
	public SpriteRenderer rainbowPlusTick;
	public SpriteRenderer rainbowPlusPlusTick;
	public SpriteRenderer vacuumTick;
	public SpriteRenderer glovesTick;

	public override void OnStart () 
	{
		accelerometerTick.enabled = PlayerData.Instance.upgrade_accelerometer;
		crowmTick.enabled = PlayerData.Instance.upgrade_crown;
		rainbowPlusTick.enabled = PlayerData.Instance.upgrade_rainbowplus;
		rainbowPlusPlusTick.enabled = PlayerData.Instance.upgrade_rainbowplusplus;
		vacuumTick.enabled = PlayerData.Instance.upgrade_vacuum;
		glovesTick.enabled = PlayerData.Instance.upgrade_gloves;

		rainbowPlusPlus.enabled = PlayerData.Instance.upgrade_rainbowplus;
		rainbowPlusPlus.GetComponent<SpriteRenderer>().color = PlayerData.Instance.upgrade_rainbowplus ? Color.white : Color.gray;
	}
	
	public override void OnUpdate () 
	{
		if(!PlayerData.Instance.upgrade_accelerometer && accelerometer.IsJustPressed())
		{
			accelerometerTick.enabled = true;

			PlayerData.Instance.upgrade_accelerometer = true;
			PlayerData.Instance.Save();
		}
		else if(backButton.IsJustPressed() || Input.GetKeyDown(KeyCode.Escape))
		{
			MenuManager.Instance.SetPage(MenuManager.Instance.shopPage);
		}
	}
}
