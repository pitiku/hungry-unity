using UnityEngine;
using System.Collections;

public class MuseumPage : MenuPage 
{
	public MenuItem backButton;
	
	MenuManager menuManager;
	
	public override void Start () 
	{
		base.Start();
		
		menuManager = FindObjectOfType<MenuManager>();
	}
	
	public override void Update () 
	{
		base.Update();
		
		if(backButton.IsJustPressed() || Input.GetKeyDown(KeyCode.Escape))
		{
			menuManager.SetPage(menuManager.mainPage);
		}
	}}
