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

	public enum eState
	{
	};

	public float currentValue = 1.0f;
	public Transform stars;

	public Transform left;
	public Transform center;
	public Transform right;

	bool bStarted = false;

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
		currentValue = Mathf.Clamp(currentValue, 0, 1);
		renderer.material.SetFloat("_Width", currentValue);

		if(currentValue > 0.5f)
		{
			stars.position = center.position + (left.position - center.position) * (currentValue - 0.5f) * 2;
		}
		else
		{
			stars.position = right.position + (center.position - right.position) * currentValue * 2;
		}
	}

	public void AnimateIn()
	{
	}

	public void AnimationFinished()
	{
	}
}
