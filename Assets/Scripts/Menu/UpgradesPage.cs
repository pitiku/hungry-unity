using UnityEngine;
using System.Collections;

public class UpgradesPage : MenuPage 
{
	public MenuItem backButton;

	public UpgradeData[] upgrades;

	public TextMesh Message;
	public TextMesh MessageBuy;

	public string DefaultText;
	public string ClickToBuyText;
	public string NotEnoughCoinsText;
	public string OwnedText;
	public string BuyRainbowText;

	UpgradeData selectedUpgrade = null;

	public override void OnStart () 
	{
	}

	public override void OnSetPage()
	{
		CoinsCounter.Instance.AnimateIn();

		foreach(UpgradeData upgrade in upgrades)
		{
			upgrade.UnSelect();
			upgrade.UpdateVisual();
		}

		Message.text = DefaultText;
		MessageBuy.text = "";
	}
	
	public void SelectUpgrade(UpgradeData _upgrade)
	{
		if(selectedUpgrade)
		{
			selectedUpgrade.UnSelect();
		}
		selectedUpgrade = _upgrade;
		selectedUpgrade.Select(Message);

		if(selectedUpgrade.IsBought())
		{
			MessageBuy.text = OwnedText;
		}
		else if(selectedUpgrade.prerequisite && !selectedUpgrade.prerequisite.IsBought())
		{
			MessageBuy.text = BuyRainbowText;
		}
		else if(selectedUpgrade.price > PlayerData.Instance.Coins)
		{
			MessageBuy.text = NotEnoughCoinsText;
		}
		else
		{
			MessageBuy.text = ClickToBuyText;
		}
	}

	public override void OnUpdate () 
	{
		foreach(UpgradeData upgrade in upgrades)
		{
			if(upgrade.item.IsJustPressed())
			{
				if(selectedUpgrade == upgrade)
				{
					if(PlayerData.Instance.Coins >= upgrade.price)
					{
						upgrade.Buy();
						PlayerData.Instance.Coins -= upgrade.price;

						foreach(UpgradeData upgradeVis in upgrades)
						{
							upgradeVis.UpdateVisual();
						}
						
						PlayerData.Instance.Save();
					}
				}
				else
				{
					SelectUpgrade(upgrade);
				}
				break;
			}
		}

		if(backButton.IsJustPressed() || Input.GetKeyDown(KeyCode.Escape))
		{
			CoinsCounter.Instance.AnimateOut();
			MenuManager.Instance.SetPage(MenuManager.Instance.shopPage);
		}
	}
}
