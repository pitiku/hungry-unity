﻿using UnityEngine;
using System.Collections;

public class StatsPage : MenuPage 
{
	public MenuItem backButton;

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

	public 
}
