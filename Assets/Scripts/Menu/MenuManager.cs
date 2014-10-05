using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuManager : Menu 
{
	FadeManager fade;

	public MenuPage mainPage;
	public MenuPage optionsPage;
	public MenuPage shopPage;
	public MenuPage museumPage;

	public override void Start () 
	{
		base.Start();

		fade = FindObjectOfType<FadeManager> ();
		if(fade != null)
		{
			fade.FadeIn(0.5f);
		}

		SetPage(mainPage);
	}
	
	public override void Update () 
	{
		base.Update();
	}
}
