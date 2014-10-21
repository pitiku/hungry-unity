using UnityEngine;
using System.Collections;

public class Rainbow : MonoBehaviour 
{
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

	public ParticleSystem stars;

	public Transform left;
	public Transform center;
	public Transform right;

	float wantedValue;
	float currentValue = 0.0f;
	public float speed = 1.0f;

	void Start () 
	{
		transform.localScale *= (Screen.width * 480.0f) / (Screen.height * 320.0f);
		SetValue(currentValue, true);
	}
	
	void Update () 
	{
		if(Mathf.Abs(wantedValue - currentValue) > 0.001f)
		{
			float inc = Mathf.Min(speed * Time.deltaTime, Mathf.Abs(wantedValue - currentValue));
			currentValue = currentValue + (wantedValue > currentValue ? 1 : -1) * inc;

			UpdateRainbow();
		}
	}

	void UpdateRainbow()
	{
		renderer.material.SetFloat("_Width", currentValue);
		
		if(currentValue > 0.5f)
		{
			stars.transform.position = center.position + (left.position - center.position) * (currentValue - 0.5f) * 2;
		}
		else
		{
			stars.transform.position = right.position + (center.position - right.position) * currentValue * 2;
		}
	}

	public void EnableStars(bool _bValue = true)
	{
		if(_bValue)
		{
			stars.Play();
		}
		else
		{
			stars.Stop();
			stars.Clear();
		}
	}

	public void SetValue(float _value, bool _instant = false)
	{
		wantedValue = _value;

		if(_instant)
		{
			currentValue = _value;
			UpdateRainbow();
		}
	}
}
