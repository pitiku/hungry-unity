using UnityEngine;
using System.Collections;

public class BabiesPage : MenuPage 
{
	public MenuItem backButton;

	public BabyInShop[] babies;
	BabyInShop currentBaby = null;
	
	public override void OnStart () 
	{
	}

	public override void OnSetPage()
	{
		CoinsCounter.Instance.AnimateIn();

		Select(null);

		foreach(BabyInShop b in babies)
		{
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
		}
		currentBaby = _baby;
		if(currentBaby)
		{
			currentBaby.SetSelected(true);
		}
	}

	public override void OnUpdate () 
	{
		foreach(BabyInShop baby in babies)
		{
			if(baby.GetMenuItem().IsJustPressed())
			{
				Select(baby);
			}
		}

		if(backButton.IsJustPressed() || Input.GetKeyDown(KeyCode.Escape))
		{
			CoinsCounter.Instance.AnimateOut();
			MenuManager.Instance.SetPage(MenuManager.Instance.shopPage);
		}
	}
}
