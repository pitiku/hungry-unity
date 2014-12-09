using UnityEngine;
using System.Collections;

public class ProgressBar : SingletonMonoBehaviour<ProgressBar> 
{
	public Transform Bar;

	public float speed = 1.0f;
	public float completeShowTime = 0.5f;

	float currentProgress = 0.0f;
	float targetProgress;
	bool animating = false;
	bool completing = false;
	float completingTime;

	void Start()
	{
		currentProgress = 0;
		SetProgress(currentProgress, false);
	}
	
	void Update()
	{
		if(animating)
		{
			float delta = Mathf.Sign(targetProgress - currentProgress) * Time.deltaTime * speed;
			if(Mathf.Abs(delta) >= Mathf.Abs(targetProgress - currentProgress))
			{
				currentProgress = targetProgress;
				animating = false;
			}
			else
			{
				currentProgress = currentProgress + delta;
			}
			Bar.localScale = new Vector3(currentProgress, 1.0f, 1.0f);
		}
		else if(completing)
		{
			completingTime -= Time.deltaTime;
			if(completingTime <= 0)
			{
				SetProgress(0.0f, false);
				completing = false;
			}
		}

	}

	public void SetProgress(float _value, bool _animate = true)
	{
		if(_animate)
		{
			targetProgress = _value;
			animating = true;
		}
		else
		{
			targetProgress = _value;
			currentProgress = _value;
			Bar.localScale = new Vector3(currentProgress, 1.0f, 1.0f);
		}
	}

	public void CompleteBar()
	{
		completing = true;
		completingTime = completeShowTime;
		SetProgress(1.0f);
	}
}
