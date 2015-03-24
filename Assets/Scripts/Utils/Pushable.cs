using UnityEngine;
using System.Collections;

public class Pushable : MonoBehaviour 
{
	BoxCollider2D box;

	void Awake()
	{
		box = GetComponent<BoxCollider2D>();
	}

	public bool IsJustPressed()
	{
		if(!enabled)
		{
			return false;
		}

		if(InputManager.Instance && InputManager.Instance.IsPress)
		{
			Vector3 touch = InputManager.Instance.PressPosition;
			touch.z = box.bounds.center.z;
			if(box.bounds.Contains(touch))
			{
				return true;
			}
		}
		return false;
	}

	public bool IsJustReleased()
	{
		if(!enabled)
		{
			return false;
		}
		
		if(InputManager.Instance && InputManager.Instance.IsRelease)
		{
			Vector3 touch = InputManager.Instance.ReleasePosition;
			touch.z = box.bounds.center.z;
			if(box.bounds.Contains(touch))
			{
				return true;
			}
		}
		return false;
	}
}
