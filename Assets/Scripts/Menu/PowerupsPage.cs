using UnityEngine;
using System.Collections;

public class PowerupsPage : MenuPage 
{
	public MenuItem backButton;
	
	public PowerupData[] powerups;
	
	public TextMesh Message;
	public TextMesh MessageBuy;
	
	public string DefaultText;
	public string ClickToBuyText;
	public string NotEnoughCoinsText;

	PowerupData selected = null;
	
	public override void OnSetPage()
	{
		CoinsCounter.Instance.AnimateIn();
		
		foreach(PowerupData powerup in powerups)
		{
			powerup.UnSelect();
			powerup.UpdateVisual();
		}
		
		Message.text = DefaultText;
		MessageBuy.text = "";
	}
	
	public void Select(PowerupData _powerup)
	{
		if(selected)
		{
			selected.UnSelect();
		}
		selected = _powerup;
		selected.Select(Message);
		
		if(selected.price > PlayerData.Instance.Coins)
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
		foreach(PowerupData powerup in powerups)
		{
			if(powerup.item.IsJustPressed())
			{
				if(selected == powerup)
				{
					if(PlayerData.Instance.Coins >= powerup.price)
					{
						powerup.Buy();
						PlayerData.Instance.Coins -= powerup.price;
						powerup.UpdateVisual();

						PlayerData.Instance.Save();
					}
				}
				else
				{
					Select(powerup);
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
