using UnityEngine;
using System.Collections;

public class Rainbow : MonoBehaviour {

	#region Singleton
	private static Rainbow _Instance;
	
	public static Rainbow Instance
	{
		get
		{
			if(!_Instance)
			{
				_Instance = FindObjectOfType<Rainbow>();
			}
			return _Instance;
		}
	}
	#endregion

	public float currentValue = 1.0f;

	void Awake()
	{
		renderer.material.SetFloat("_Width", 0.0f);
	}

	void Start () 
	{
		AnimateIn();
	}
	
	void Update () 
	{
		currentValue -= (Time.deltaTime * 0.2f);
		//renderer.material.SetFloat("_Width", currentValue);
	}

	public void AnimateIn()
	{
	}

	public void AnimationFinished()
	{
	}
}
