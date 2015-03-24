using UnityEngine;
using System.Collections;

public class InputManager : SingletonMonoBehaviour<InputManager>
{
	private bool m_IsPress = false;
	private bool m_IsRelease = false;
	private Vector3 m_PressPosition;
	private Vector3 m_ReleasePosition;

	public bool IsPress
	{
		get { return m_IsPress; }
		set { m_IsPress = value; }
	}
	
	public bool IsRelease
	{
		get { return m_IsRelease; }
		set { m_IsRelease = value; }
	}
	
	public Vector3 PressPosition
	{
		get { return m_PressPosition; }
		set { m_PressPosition = value; }
	}
	
	public Vector3 ReleasePosition
	{
		get { return m_ReleasePosition; }
		set { m_ReleasePosition = value; }
	}
	
	protected override void OnAwake()
	{
		DontDestroyOnLoad(gameObject);
	}

	void Update () 
	{
		//Reset the values each frame
		IsPress = false;
		IsRelease = false;

		//Detect mobile touch
		foreach(Touch touch in Input.touches)
		{
			if(touch.phase == TouchPhase.Began)
			{
				IsPress = true;
				PressPosition = Camera.main.ScreenToWorldPoint(touch.position);
				break;
			}

			if(touch.phase == TouchPhase.Ended)
			{
				IsRelease = true;
				ReleasePosition = Camera.main.ScreenToWorldPoint(touch.position);
				break;
			}
		}

		//Detect mouse press
		if(Input.GetMouseButtonDown(0))
		{
			IsPress = true;
			PressPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		}

		if(Input.GetMouseButtonUp(0))
		{
			IsRelease = true;
			ReleasePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		}
	}
}
