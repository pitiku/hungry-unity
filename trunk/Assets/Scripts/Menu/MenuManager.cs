using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuManager : Menu 
{
	private static MenuManager s_Instance;
	
	public static MenuManager Instance
	{
		get
		{
			if (!s_Instance)
			{
				s_Instance = GameObject.FindObjectOfType<MenuManager>();
			}
			return s_Instance;
		}
	}

	public MenuPage mainPage;
	public MenuPage optionsPage;
	public MenuPage shopPage;
	public MenuPage museumPage;
	public MenuPage babiesPage;
	public MenuPage upgradesPage;
	public MenuPage powerupsPage;
	public MenuPage statsPage;

	public override void Start () 
	{
		base.Start();

		if(FadeManager.Instance)
		{
			FadeManager.Instance.FadeIn(0.5f);
		}

		SetPage(mainPage);
	}
	
	public override void Update () 
	{
		base.Update();
	}
}
