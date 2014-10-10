using UnityEngine;
using System.Collections;

public class UpgradesPage : MenuPage 
{
	public MenuItem backButton;

	public UpgradeData[] upgrades;

	public MeshRenderer Message;
	public MeshRenderer ClickToBuyText;
	public MeshRenderer NotEnoughCoinsText;
	public MeshRenderer OwnedText;
	public MeshRenderer BuyRainbowText;

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

		Message.enabled = true;

		ClickToBuyText.enabled = false;
		NotEnoughCoinsText.enabled = false;
		OwnedText.enabled = false;
		BuyRainbowText.enabled = false;
	}

	void UpdateMessage()
	{
		ClickToBuyText.enabled = false;
		NotEnoughCoinsText.enabled = false;
		OwnedText.enabled = false;
		BuyRainbowText.enabled = false;

		if(selected.IsBought())
		{
			OwnedText.enabled = true;
		}
		else if(selected.prerequisite && !selected.prerequisite.IsBought())
		{
			BuyRainbowText.enabled = true;
		}
		else if(selected.price > PlayerData.Instance.Coins)
		{
			NotEnoughCoinsText.enabled = true;
		}
		else
		{
			ClickToBuyText.enabled = true;
		}
	}

	public void Select(UpgradeData _upgrade)
	{
		if(selected)
		{
			selected.UnSelect();
		}
		selected = _upgrade;
		selected.Select();

		Message.enabled = false;

		UpdateMessage();
	}

	public override void OnUpdate () 
	{
		foreach(UpgradeData upgrade in upgrades)
		{
			if(upgrade.item.IsJustPressed())
			{
				if(selected == upgrade && selected.CanBuy())
				{
					if(PlayerData.Instance.Coins >= selected.price)
					{
						selected.Buy();
						UpdateMessage();

						//Update all the visuals (because of prerequisites)
						foreach(UpgradeData upgradeVis in upgrades)
						{
							if(!upgradeVis.IsBought() && upgradeVis.prerequisite)
							{
								upgradeVis.UpdateVisual();
							}
						}

						PlayerData.Instance.Coins -= selected.price;
						PlayerData.Instance.Save();
					}
				}
				else
				{
					Select(upgrade);
				}
				return;
			}
		}

		/*
		if(InputManager.Instance.IsTouch)
		{
			Select(upgrades[0]);
		}
		*/

		if(backButton.IsJustPressed() || Input.GetKeyDown(KeyCode.Escape))
		{
			CoinsCounter.Instance.AnimateOut();
			MenuManager.Instance.SetPage(MenuManager.Instance.shopPage);
		}
	}
}
