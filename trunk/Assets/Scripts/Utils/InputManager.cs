using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

	#region Singleton
	private static InputManager _Instance;

	public static InputManager Instance
	{
		get
		{
			if(!_Instance)
			{
				_Instance = FindObjectOfType<InputManager>();
			}
			return _Instance;
		}
	}
	#endregion

	public bool IsTouch = false;
	public Vector3 TouchPosition;

	void Awake()
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
