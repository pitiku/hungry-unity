using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour 
{
	MenuPage[] pages;
	MenuPage currentPage = null;

	public virtual void Start () 
	{
		pages = FindObjectsOfType<MenuPage>();
		foreach(MenuPage page in pages)
		{
			page.SetMenu(this);
			page.gameObject.SetActive(false);
		}
	}
	
	public virtual void Update () 
	{
	
	}

	public void SetPage(MenuPage _page)
	{
		if(currentPage != null)
		{
			currentPage.gameObject.SetActive(false);
		}
		currentPage = _page;
		currentPage.gameObject.SetActive(true);
	}
}
