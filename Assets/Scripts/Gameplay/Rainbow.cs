﻿using UnityEngine;
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

	public ParticleSystem stars;

	public Transform left;
	public Transform center;
	public Transform right;

	public float wantedValue;
	public float currentValue = 0.0f;
	public float speed = 1.0f;

	void Start () 
	{
		transform.localScale *= (Screen.width * 480.0f) / (Screen.height * 320.0f);

		SetValue(0.0f, true);
		SetValue(1.0f);

		stars.Play();
	}
	
	void Update () 
	{
		if((wantedValue - currentValue) > 0.01f)
		{
			currentValue = currentValue + (wantedValue > currentValue ? 1 : -1) * speed * Time.deltaTime;

			UpdateRainbow();
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
}
