using UnityEngine;
using System.Collections;

public class FPSCounter : MonoBehaviour {

	TextMesh text;

	float time = 0;
	int frameCount = 0;

	void Awake()
	{
		text = GetComponent<TextMesh>();
	}

	void Start () 
	{
		time = Time.time;
	}
	
	void Update () 
	{
		frameCount ++;
		if(Time.time - time >= 1)
		{
			time = Time.time;
			text.text = "" + frameCount;
			frameCount = 0;
		}
	}
}
