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

	public Transform[] points;

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
		if(IsAnimating())
		{
			currentValue = Mathf.MoveTowards(currentValue, wantedValue, speed * Time.deltaTime);
			UpdateRainbow();
		}
	}

	public bool IsAnimating()
	{
		return Mathf.Abs(wantedValue - currentValue) > 0.001f;
	}

	void UpdateRainbow()
	{
		renderer.material.SetFloat("_Width", currentValue);

		float fValue = currentValue * (points.Length - 1);
		int iFloor = (int)Mathf.Floor(fValue);
		int iCeil = (int)Mathf.Ceil(fValue);
		float fPerc = fValue - iFloor;
		stars.transform.position = points[iFloor].position * (1-fPerc) + points[iCeil].position * fPerc;
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
