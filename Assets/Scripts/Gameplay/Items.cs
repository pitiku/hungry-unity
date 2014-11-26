using UnityEngine;
using System.Collections;

public class Items : SingletonMonoBehaviour<Items>
{
	public AnimatedObject ItemsAnimation;
	public PowerUp_Level ChainBoost;
	public PowerUp_Level DoubleCoins;
	public PowerUp_Level PrizeSeason;
	public PowerUp_Level MegaChainBoost;
	
	public PowerUp_Level BoletTime;
	public Pushable FeederGloves;
	
	public PowerUp_Level ExtraRainbow;

	public void ShowInitial()
	{
		ItemsAnimation.StartAnimation("In");
		
		ChainBoost.SetCount(PlayerData.Instance.powerup_chainBoost);
		ChainBoost.SetEnabled(PlayerData.Instance.powerup_chainBoost > 0);
		
		MegaChainBoost.SetCount(PlayerData.Instance.powerup_megaChainBoost);
		MegaChainBoost.SetEnabled(PlayerData.Instance.powerup_megaChainBoost > 0);
		
		DoubleCoins.SetCount(PlayerData.Instance.powerup_doubleCoins);
		DoubleCoins.SetEnabled(PlayerData.Instance.powerup_doubleCoins > 0);
		
		PrizeSeason.SetCount(PlayerData.Instance.powerup_prizeSeason);
		PrizeSeason.SetEnabled(PlayerData.Instance.powerup_prizeSeason > 0);
	}

	public void CheckInitialInput()
	{
		if(ChainBoost.menuItem.IsJustPressed())
		{
			PlayerData.Instance.powerup_chainBoost -= 1;
			PlayerData.Instance.Save();
			ChainBoost.SetCount(PlayerData.Instance.powerup_chainBoost);
			ChainBoost.SetEnabled(false);
			MegaChainBoost.SetEnabled(false);
			Score.Instance.ChainBoost();
		}
		if(MegaChainBoost.menuItem.IsJustPressed())
		{
			PlayerData.Instance.powerup_megaChainBoost -= 1;
			PlayerData.Instance.Save();
			MegaChainBoost.SetCount(PlayerData.Instance.powerup_megaChainBoost);
			MegaChainBoost.SetEnabled(false);
			ChainBoost.SetEnabled(false);
			Score.Instance.MegaChainBoost();
		}
		if(DoubleCoins.menuItem.IsJustPressed())
		{
			PlayerData.Instance.powerup_doubleCoins -= 1;
			PlayerData.Instance.Save();
			DoubleCoins.SetCount(PlayerData.Instance.powerup_doubleCoins);
			DoubleCoins.SetEnabled(false);
			Score.Instance.DoubleCoins();
		}
		if(PrizeSeason.menuItem.IsJustPressed())
		{
			PlayerData.Instance.powerup_prizeSeason -= 1;
			PlayerData.Instance.Save();
			PrizeSeason.SetCount(PlayerData.Instance.powerup_prizeSeason);
			PrizeSeason.SetEnabled(false);
			Gameplay_Normal.Instance.PrizeSeason();
		}
	}

	public void HideInitial()
	{
		ItemsAnimation.StartAnimation("Out");
	}

	public void ShowGameplay()
	{
		BoletTime.gameObject.SetActive(PlayerData.Instance.powerup_boletTime > 0);
		FeederGloves.gameObject.SetActive(PlayerData.Instance.upgrade_gloves);

		ItemsAnimation.StartAnimation("GameplayIn");		
	}

	public void CheckGameplayInput()
	{
		if(FeederGloves.IsJustPressed())
		{
			
		}
		
		if(BoletTime.menuItem.IsJustPressed())
		{
			
		}
	}

	public void HideGameplay()
	{
		ItemsAnimation.StartAnimation("GameplayOut");
	}

	public void ShowFinal()
	{
		ItemsAnimation.StartAnimation("FinalIn");
		
		ExtraRainbow.SetCount(PlayerData.Instance.powerup_extraRainbow);
		ExtraRainbow.SetEnabled(PlayerData.Instance.powerup_extraRainbow > 0);
	}
	
	public bool CheckFinalInput()
	{
		if(ExtraRainbow.menuItem.IsJustPressed())
		{
			PlayerData.Instance.powerup_extraRainbow -= 1;
			PlayerData.Instance.Save();
			ExtraRainbow.SetCount(PlayerData.Instance.powerup_extraRainbow);
			ExtraRainbow.SetEnabled(false);	

			return true;
		}
		return false;
	}
	
	public void HideFinal()
	{
		ItemsAnimation.StartAnimation("FinalOut");
	}

	public bool IsFinished()
	{
		return ItemsAnimation.IsFinished();
	}

	void Update()
	{
	}
}
