using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour 
{
	MenuPage[] pages;
	MenuPage currentPage = null;
	MenuPage nextPage = null;
	bool InAnim = true;

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
		if(nextPage)
		{
			if(currentPage.IsAnimationFinished())
			{
				currentPage.gameObject.SetActive(false);
				currentPage = nextPage;
				nextPage = null;
				currentPage.gameObject.SetActive(true);
				if(InAnim)
				{
					currentPage.AnimateIn();
				}
			}
		}
	}

	public void SetPage(MenuPage _page, bool _outAnim = true, bool _inAnim = true)
	{
		nextPage = _page;
		InAnim = _inAnim;

		if(currentPage)
		{
			if(_outAnim)
			{
				currentPage.AnimateOut();
			}
		}

		if(!currentPage || currentPage.IsAnimationFinished())
		{
			if(currentPage != null)
			{
				currentPage.gameObject.SetActive(false);
			}

			currentPage = nextPage;
			nextPage = null;
			currentPage.gameObject.SetActive(true);
			if(_inAnim)
			{
				currentPage.AnimateIn();
			}
		}
	}
}
