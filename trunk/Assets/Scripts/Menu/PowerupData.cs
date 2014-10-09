using UnityEngine;
using System.Collections;

public class PowerupData : MonoBehaviour 
{
	
	public GameConstants.ePowerups powerup;
	public int price;
	public string text;
	
	public MenuItem item;
	public TextMesh countText;
	public TextMesh priceText;
	public TextMesh priceText_shadow;
	public SpriteRenderer halo;
	
	public void UpdateVisual()
	{
		countText.text = "" + GetCount();
		priceText.text = "" + price;
		priceText_shadow.text = "" + price;
		halo.gameObject.SetActive(false);
	}
	
	public void Select(TextMesh _message)
	{
		halo.gameObject.SetActive(true);
		_message.text = text;
	}
	
	public void UnSelect()
	{
		halo.gameObject.SetActive(false);
	}
	
	public int GetCount()
	{
		switch(powerup)
		{
		case GameConstants.ePowerups.BOLET_TIME:
			return PlayerData.Instance.powerup_boletTime;
		case GameConstants.ePowerups.DOUBLE_COINS:
			return PlayerData.Instance.powerup_doubleCoins;
		case GameConstants.ePowerups.EXTRA_RAINBOW:
			return PlayerData.Instance.powerup_extraRainbow;
		case GameConstants.ePowerups.PRIZE_SEASON:
			return PlayerData.Instance.powerup_prizeSeason;
		case GameConstants.ePowerups.CHAIN_BOOST:
			return PlayerData.Instance.powerup_chainBoost;
		case GameConstants.ePowerups.MEGA_CHAIN_BOOST:
			return PlayerData.Instance.powerup_megaChainBoost;
		}
		return 0;
	}
	
	public void Buy()
	{
		switch(powerup)
		{
		case GameConstants.ePowerups.BOLET_TIME:
			PlayerData.Instance.powerup_boletTime++;
			break;
		case GameConstants.ePowerups.DOUBLE_COINS:
			PlayerData.Instance.powerup_doubleCoins++;
			break;
		case GameConstants.ePowerups.EXTRA_RAINBOW:
			PlayerData.Instance.powerup_extraRainbow++;
			break;
		case GameConstants.ePowerups.PRIZE_SEASON:
			PlayerData.Instance.powerup_prizeSeason++;
			break;
		case GameConstants.ePowerups.CHAIN_BOOST:
			PlayerData.Instance.powerup_chainBoost++;
			break;
		case GameConstants.ePowerups.MEGA_CHAIN_BOOST:
			PlayerData.Instance.powerup_megaChainBoost++;
			break;
		}
	}}
