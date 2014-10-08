﻿using UnityEngine;
using System.Collections;

public class MainMenu : MenuPage 
{
	public MenuItem playButton;
	public MenuItem optionsButton;
	public MenuItem shopButton;
	public MenuItem pedestalButton;

	bool PlayPressed = false;

	public override void OnStart () 
	{
		CoinsCounter.Instance.gameObject.SetActive(false);
	}
	
	public override void OnUpdate () 
	{
		if(PlayPressed)
		{
			if(IsAnimationFinished())
			{
				Application.LoadLevel("Level");
			}
			return;
		}

		if (Input.GetKeyDown(KeyCode.Escape)) 
		{
			Application.Quit(); 
		}

		if(playButton.IsJustPressed())
		{
			Animate("StartGame");
			PlayPressed = true;
		}
		else if(optionsButton.IsJustPressed())
		{
			MenuManager.Instance.SetPage(MenuManager.Instance.optionsPage);
		}
		else if(shopButton.IsJustPressed())
		{
			CoinsCounter.Instance.gameObject.SetActive(true);
			CoinsCounter.Instance.AnimateIn();
			MenuManager.Instance.SetPage(MenuManager.Instance.shopPage);
		}
		else if(pedestalButton.IsJustPressed())
		{
			MenuManager.Instance.SetPage(MenuManager.Instance.museumPage);
		}
	}
}
