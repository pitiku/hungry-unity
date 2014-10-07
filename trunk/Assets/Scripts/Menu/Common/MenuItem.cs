using UnityEngine;
using System.Collections;

public class MenuItem : MonoBehaviour 
{
	//MenuPage page;
	bool m_bJustPressed = false;

	public void SetPage(MenuPage _page)
	{
		//page = _page;
	}

	void OnMouseDown()
	{
		m_bJustPressed = true;
		OnJustPressed();
	}

	public bool IsJustPressed()
	{
		return m_bJustPressed;
	}

	void OnJustPressed()
	{
	}

	public void ResetPressed()
	{
		if(m_bJustPressed)
		{
			m_bJustPressed = false;
		}
	}
}
