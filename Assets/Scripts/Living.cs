using UnityEngine;
using System.Collections;

public class Living : MonoBehaviour 
{
	Vector2 scaleInit;

	public float scaleFactorX = 0.15f;
	public float scaleFactorY = 0.15f;
	public float scaleSpeedX = 2.0f;
	public float scaleSpeedY = 2.0f;
	public float scaleDelay = 0.5f;

	void Start () 
	{
		scaleInit = transform.localScale;
	}
	
	void Update () 
	{
		transform.localScale = scaleInit + new Vector2(scaleFactorX * Mathf.Sin(Time.time * scaleSpeedX), scaleFactorY * Mathf.Sin((Time.time + scaleDelay) * scaleSpeedY));
	}
}
