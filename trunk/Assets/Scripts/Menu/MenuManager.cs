using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuManager : Menu 
{
	#region Singleton
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
	#endregion

	public MenuPage mainPage;
	public MenuPage optionsPage;
	public MenuPage statsPage;
	public MenuPage shopPage;
	public MenuPage babiesPage;
	public MenuPage upgradesPage;
	public MenuPage powerupsPage;
	public MenuPage museumPage;

	bool bMusicFading = false;
	float fMusicFadeSpeed = 2.0f;

	void Awake()
	{
		PlayerData.Instance.Load();
	}

	public override void Start () 
	{
		base.Start();

		if(FadeManager.Instance && FadeManager.Instance.IsFaded())
		{
			FadeManager.Instance.FadeIn(0.2f);
		}

		SetPage(mainPage);

		GetComponent<AudioSource>().enabled = PlayerData.Instance.option_music;
	}
	
	public override void Update () 
	{
		base.Update();

		if(bMusicFading)
		{
			GetComponent<AudioSource>().volume -= Time.deltaTime * fMusicFadeSpeed;
		}
	}

	public void MusicFadeOut()
	{
		bMusicFading = GetComponent<AudioSource>().enabled;
	}
}
