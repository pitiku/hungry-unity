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

	UpgradeData selected = null;

	public override void OnStart () 
	{
	}

	public override void OnSetPage()
	{
		CoinsCounter.Instance.AnimateIn();

		selected = null;

		foreach(UpgradeData upgrade in upgrades)
		{
			upgrade.UnSelect();
			upgrade.UpdateVisual();
		}

		Message.text = DefaultText;
		MessageBuy.text = "";
	}
	
	public void Select(UpgradeData _upgrade)
	{
		if(selected)
		{
			selected.UnSelect();
		}
		selected = _upgrade;
		selected.Select(Message);

		if(selected.IsBought())
		{
			MessageBuy.text = OwnedText;
		}
		else if(selected.prerequisite && !selected.prerequisite.IsBought())
		{
			MessageBuy.text = BuyRainbowText;
		}
		else if(selected.price > PlayerData.Instance.Coins)
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
				if(selected == upgrade)
				{
					if(PlayerData.Instance.Coins >= upgrade.price)
					{
						upgrade.Buy();
						PlayerData.Instance.Coins -= upgrade.price;

						//Update all the visuals (because of prerequisites)
						foreach(UpgradeData upgradeVis in upgrades)
						{
							upgradeVis.UpdateVisual();
						}
						
						PlayerData.Instance.Save();
					}
				}
				else
				{
					Select(upgrade);
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
