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
	public SpriteRenderer crownTick;
	public SpriteRenderer rainbowPlusTick;
	public SpriteRenderer rainbowPlusPlusTick;
	public SpriteRenderer vacuumTick;
	public SpriteRenderer glovesTick;

	public SpriteRenderer accelerometerHalo;
	public SpriteRenderer crownHalo;
	public SpriteRenderer rainbowPlusHalo;
	public SpriteRenderer rainbowPlusPlusHalo;
	public SpriteRenderer vacuumHalo;
	public SpriteRenderer glovesHalo;

	public TextMesh Message;
	public TextMesh MessageBuy;

	public string AccelerometerText;
	public string CrownText;
	public string RainbowPlusText;
	public string RainbowPlusPlusText;
	public string VacuumText;
	public string GlovesText;

	public int accelerometerPrice;
	public int crownPrice;
	public int rainbowPlusPrice;
	public int rainbowPlusPlusPrice;
	public int vacuumPrice;
	public int glovesPrice;

	public string ClickToBuyText;
	public string OwnedText;
	public string BuyRainbowText;

	MenuItem selected = null;
	SpriteRenderer halo = null;

	public override void OnStart () 
	{
		accelerometerTick.enabled = PlayerData.Instance.upgrade_accelerometer;
		crownTick.enabled = PlayerData.Instance.upgrade_crown;
		rainbowPlusTick.enabled = PlayerData.Instance.upgrade_rainbowplus;
		rainbowPlusPlusTick.enabled = PlayerData.Instance.upgrade_rainbowplusplus;
		vacuumTick.enabled = PlayerData.Instance.upgrade_vacuum;
		glovesTick.enabled = PlayerData.Instance.upgrade_gloves;

		Color gray = Color.gray;
		gray.a = 0.5f;
		rainbowPlusPlus.GetComponent<SpriteRenderer>().color = PlayerData.Instance.upgrade_rainbowplus ? Color.white : gray;
	}

	void SelectItem(MenuItem _item, SpriteRenderer _halo, string _text, string _textBuy)
	{
		selected = _item;
		if(halo)
		{
			halo.gameObject.SetActive(false);
		}
		halo = _halo;
		halo.gameObject.SetActive(true);
		Message.text = _text;
		MessageBuy.text = _textBuy;
	}

	public override void OnUpdate () 
	{
		if(accelerometer.IsJustPressed())
		{
			if(selected == accelerometer)
			{
				if(!PlayerData.Instance.upgrade_accelerometer)
				{
					if(PlayerData.Instance.Coins >= accelerometerPrice)
					{
						accelerometerTick.enabled = true;
						PlayerData.Instance.upgrade_accelerometer = true;
						SelectItem(accelerometer, accelerometerHalo, AccelerometerText, OwnedText);

						PlayerData.Instance.Coins -= accelerometerPrice;

						//PlayerData.Instance.Save();
					}
					else
					{
						//Show coins popup
						//CoinsPopUp.Instance.Show();
					}
				}
			}
			else
			{
				SelectItem(accelerometer, accelerometerHalo, AccelerometerText, PlayerData.Instance.upgrade_accelerometer ? OwnedText : ClickToBuyText);
			}
		}
		else if(crown.IsJustPressed())
		{
			if(selected == crown)
			{
				if(!PlayerData.Instance.upgrade_crown)
				{
					crownTick.enabled = true;
					PlayerData.Instance.upgrade_crown = true;
					SelectItem(crown, crownHalo, CrownText, OwnedText);
					//PlayerData.Instance.Save();
				}
			}
			else
			{
				SelectItem(crown, crownHalo, CrownText, PlayerData.Instance.upgrade_crown ? OwnedText : ClickToBuyText);
			}
		}
		else if(vacuum.IsJustPressed())
		{
			if(selected == vacuum)
			{
				if(!PlayerData.Instance.upgrade_vacuum)
				{
					vacuumTick.enabled = true;
					PlayerData.Instance.upgrade_vacuum = true;
					SelectItem(vacuum, vacuumHalo, VacuumText, OwnedText);
					//PlayerData.Instance.Save();
				}
			}
			else
			{
				SelectItem(vacuum, vacuumHalo, VacuumText, PlayerData.Instance.upgrade_vacuum ? OwnedText : ClickToBuyText);
			}
		}
		else if(gloves.IsJustPressed())
		{
			if(selected == gloves)
			{
				if(!PlayerData.Instance.upgrade_gloves)
				{
					glovesTick.enabled = true;
					PlayerData.Instance.upgrade_gloves = true;
					SelectItem(gloves, glovesHalo, GlovesText, OwnedText);
					//PlayerData.Instance.Save();
				}
			}
			else
			{
				SelectItem(gloves, glovesHalo, GlovesText, PlayerData.Instance.upgrade_gloves ? OwnedText : ClickToBuyText);
			}
		}
		else if(rainbowPlus.IsJustPressed())
		{
			if(selected == rainbowPlus)
			{
				if(!PlayerData.Instance.upgrade_rainbowplus)
				{
					rainbowPlusTick.enabled = true;
					PlayerData.Instance.upgrade_rainbowplus = true;
					SelectItem(rainbowPlus, rainbowPlusHalo, RainbowPlusText, OwnedText);
					//PlayerData.Instance.Save();

					rainbowPlusPlus.GetComponent<SpriteRenderer>().color = Color.white;
				}
			}
			else
			{
				SelectItem(rainbowPlus, rainbowPlusHalo, RainbowPlusText, PlayerData.Instance.upgrade_rainbowplus ? OwnedText : ClickToBuyText);
			}
		}
		else if(rainbowPlusPlus.IsJustPressed())
		{
			if(selected == rainbowPlusPlus)
			{
				if(!PlayerData.Instance.upgrade_rainbowplusplus && PlayerData.Instance.upgrade_rainbowplus)
				{
					rainbowPlusPlusTick.enabled = true;
					PlayerData.Instance.upgrade_rainbowplusplus = true;
					SelectItem(accelerometer, accelerometerHalo, AccelerometerText, OwnedText);
					//PlayerData.Instance.Save();
				}
			}
			else if(!PlayerData.Instance.upgrade_rainbowplus)
			{
				SelectItem(rainbowPlusPlus, rainbowPlusPlusHalo, RainbowPlusPlusText, BuyRainbowText);
			}
			else
			{
				SelectItem(rainbowPlusPlus, rainbowPlusPlusHalo, RainbowPlusPlusText, PlayerData.Instance.upgrade_rainbowplusplus ? OwnedText : ClickToBuyText);
			}
		}
		else if(backButton.IsJustPressed() || Input.GetKeyDown(KeyCode.Escape))
		{
			MenuManager.Instance.SetPage(MenuManager.Instance.shopPage);
		}
	}
}
