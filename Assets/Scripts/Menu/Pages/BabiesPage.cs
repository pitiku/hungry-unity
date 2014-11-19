using UnityEngine;
using System.Collections;

public class BabiesPage : MenuPage 
{
	public Pushable backButton;

	public BabyInShop[] babies;
	BabyInShop currentBaby = null;

	public MeshRenderer DefaultText;
	public MeshRenderer UnlockInGameText;
	public MeshRenderer ClickToBuyText;
	public MeshRenderer NotEnoughCoinsText;

	public MeshRenderer HiddenMessageText;

	MeshRenderer CurrentText;

	public override void OnStart () 
	{
		CurrentText = DefaultText;
	}

	public override void OnSetPage()
	{
		CoinsCounter.Instance.AnimateIn();

		Select(null);

		foreach(BabyInShop b in babies)
		{
			b.ShowPrice(b.GetBaby().IsUnlocked() && !b.GetBaby().IsBought());
			b.SetShadowed(!b.GetBaby().IsUnlocked() || !b.GetBaby().IsBought());
			if(!b.GetBaby().IsUnlocked())
			{
				b.GetBaby().StopAnimation();
			}
		}
	}

	void Select(BabyInShop _baby)
	{
		if(currentBaby)
		{
			currentBaby.SetSelected(false);
			currentBaby.message.enabled = false;
			HiddenMessageText.enabled = false;
		}
		currentBaby = _baby;
		if(currentBaby)
		{
			currentBaby.SetSelected(true);

			if(currentBaby.GetBaby().IsBought())
			{
				SetMessage(null);
				HiddenMessageText.enabled = false;
				currentBaby.message.enabled = true;
			}
			else if(currentBaby.GetBaby().IsUnlocked())
			{
				HiddenMessageText.enabled = true;
				if(PlayerData.Instance.Coins >= currentBaby.Price)
				{
					SetMessage(ClickToBuyText);
				}
				else
				{
					SetMessage(NotEnoughCoinsText);
				}
			}
			else
			{
				HiddenMessageText.enabled = true;
				SetMessage(UnlockInGameText);
			}
		}
		else
		{
			SetMessage(DefaultText);
		}
	}

	void SetMessage(MeshRenderer _message)
	{
		if(_message != CurrentText)
		{
			if(CurrentText)
			{
				CurrentText.enabled = false;
			}
			CurrentText = _message; 
			if(CurrentText)
			{
				CurrentText.enabled = true;
			}
		}
	}

	public override void OnUpdate () 
	{
		foreach(BabyInShop baby in babies)
		{
			if(baby.GetMenuItem().IsJustPressed())
			{
				if(currentBaby == baby)
				{
					//Buy
					if(!currentBaby.GetBaby().IsBought() && PlayerData.Instance.Coins >= currentBaby.Price)
					{
						PlayerData.Instance.BuyBaby((int)currentBaby.GetBaby().baby, currentBaby.Price);
					}
				}
				else
				{
					Select(baby);
				}
			}
		}

		if(backButton.IsJustPressed() || Input.GetKeyDown(KeyCode.Escape))
		{
			CoinsCounter.Instance.AnimateOut();
			MenuManager.Instance.SetPage(MenuManager.Instance.shopPage);
		}
	}
}
