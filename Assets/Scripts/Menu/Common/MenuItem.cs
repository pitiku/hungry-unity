using UnityEngine;
using System.Collections;

public class MenuItem : MonoBehaviour 
{
	//MenuPage page;
	BoxCollider2D box;

	void Awake()
	{
		box = GetComponent<BoxCollider2D>();
	}

	public void SetPage(MenuPage _page)
	{
		//page = _page;
	}

	public bool IsJustPressed()
	{
		if(!enabled)
		{
			return false;
		}

		if(InputManager.Instance && InputManager.Instance.IsTouch)
		{
			Vector3 touch = InputManager.Instance.TouchPosition;
			touch.z = box.bounds.center.z;
			if(box.bounds.Contains(touch))
			{
				return true;
			}
		}
		return false;
	}

	void OnJustPressed()
	{
	}
}
