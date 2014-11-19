using UnityEngine;
using System.Collections;

public class InputManager : SingletonMonoBehaviour<InputManager>
{
	public bool IsTouch = false;
	public Vector3 TouchPosition;

	protected override void OnAwake()
	{
		DontDestroyOnLoad(gameObject);
	}

	void Update () 
	{
		IsTouch = false;
		foreach(Touch touch in Input.touches)
		{
			if(touch.phase == TouchPhase.Began)
			{
				IsTouch = true;
				TouchPosition = Camera.main.ScreenToWorldPoint(touch.position);
				break;
			}
		}

		if(Input.GetMouseButtonDown(0))
		{
			IsTouch = true;
			TouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		}
	}
}
