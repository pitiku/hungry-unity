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

	void Awake()
	{
		PlayerData.Instance.Load();
	}

	public override void Start () 
	{
		base.Start();

		if(FadeManager.Instance)
		{
			FadeManager.Instance.FadeIn(0.2f);
		}

		SetPage(mainPage);

		GetComponent<AudioSource>().enabled = PlayerData.Instance.option_music;
	}
	
	public override void Update () 
	{
		base.Update();
	}
}
