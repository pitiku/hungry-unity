using UnityEngine;
using System.Collections;

public class StatsPage : MenuPage 
{
	public Pushable backButton;

	public TextMesh maxBabies;
	public TextMesh totalBabies;
	public TextMesh coins;
	public TextMesh totalCoins;
	public TextMesh spentCoind;
	public TextMesh gamesPlayed;
	public TextMesh prizesCollected;
	public TextMesh maxChain;

	public override void OnStart () 
	{

	}

	public override void OnUpdate () 
	{
		if(backButton.IsJustPressed() || Input.GetKeyDown(KeyCode.Escape))
		{
			MenuManager.Instance.SetPage(MenuManager.Instance.optionsPage);
		}
	}

	public void UpdateStats()
	{
		maxBabies.text = "" + PlayerData.Instance.maxBabies;
		totalBabies.text = "" + PlayerData.Instance.totalBabies;
		coins.text = "" + PlayerData.Instance.Coins;
		totalCoins.text = "" + PlayerData.Instance.totalCoins;
		spentCoind.text = "" + PlayerData.Instance.spentCoins;
		gamesPlayed.text = "" + PlayerData.Instance.numGames;
		prizesCollected.text = "" + PlayerData.Instance.numPrizes;
		maxChain.text = "" + PlayerData.Instance.maxCombo;
	}
}
