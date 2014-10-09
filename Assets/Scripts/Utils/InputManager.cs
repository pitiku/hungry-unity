using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

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

	public bool IsTouch = false;
	public Vector3 TouchPosition;
	public TextMesh text;

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
				//text.text = TouchPosition.x + ", " + TouchPosition.y;
				break;
			}
		}

		if(Input.GetMouseButtonDown(0))
		{
			IsTouch = true;
			TouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			//text.text = TouchPosition.x + ", " + TouchPosition.y;
		}
	}
}
