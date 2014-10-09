using UnityEngine;
using System.Collections;

public class MenuItem : MonoBehaviour 
{
	//MenuPage page;
	bool m_bJustPressed = false;
	BoxCollider2D box;

	void Awake()
	{
		box = GetComponent<BoxCollider2D>();
	}

	public void SetPage(MenuPage _page)
	{
		//page = _page;
	}

	void Update()
	{
		if(InputManager.Instance.IsTouch)
		{
			Vector3 touch = InputManager.Instance.TouchPosition;
			touch.z = box.bounds.center.z;
			if(box.bounds.Contains(touch))
			{
				m_bJustPressed = true;
			}
		}
	}

	//void OnMouseDown()
	//{
	//	m_bJustPressed = true;
	//	OnJustPressed();
	//}

	public bool IsJustPressed()
	{
		bool bJustPressed = m_bJustPressed;
		m_bJustPressed = false;
		return bJustPressed;
	}

	void OnJustPressed()
	{
	}

	public void ResetPressed()
	{
		if(m_bJustPressed)
		{
			//m_bJustPressed = false;
		}
	}
}
