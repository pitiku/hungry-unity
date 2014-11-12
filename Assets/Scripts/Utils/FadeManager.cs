using UnityEngine;
using System.Collections;

public class FadeManager : SingletonMonoBehaviour<FadeManager>
{
	bool finished = true;
	bool fadeIn = true;
	float duration = 0.2f;
	float startTime;
	SpriteRenderer sprite;

	void Awake()
	{
		DontDestroyOnLoad (transform.gameObject);
		sprite = GetComponent<SpriteRenderer> ();
	}

	void Start () 
	{
	}
	
	void Update () 
	{
		if (!finished) 
		{
			Color color = sprite.color;

			float fPerc = (Time.time - startTime) / duration;
			if(fPerc >= 1.0f)
			{
				finished = true;
				color.a = fadeIn ? 0.0f : 1.0f;

				if(fadeIn)
				{
					sprite.enabled = false;
				}
			}
			else
			{
				color.a = fadeIn ? 1.0f-fPerc : fPerc;
			}

			sprite.color = color;
		}
	}

	public void Fade(Color _color, bool _fadeIn = true, float _duration = 0.2f)
	{
		finished = false;
		fadeIn = _fadeIn;
		duration = _duration;

		sprite.enabled = true;

		if (fadeIn) 
		{
			_color.a = 1.0f;
		}
		else
		{
			_color.a = 0.0f;
		}

		sprite.color = _color;

		startTime = Time.time;
	}

	public void FadeIn(float _duration = 0.2f)
	{
		Fade (Color.black, true, _duration);
	}
	
	public void FadeIn(Color _color, float _duration = 0.2f)
	{
		Fade (_color, true, _duration);
	}
	
	public void FadeOut(float _duration = 0.2f)
	{
		Fade (Color.black, false, _duration);
	}
	
	public void FadeOut(Color _color, float _duration = 0.2f)
	{
		Fade (_color, false, _duration);
	}

	public bool IsFinished()
	{
		return finished;
	}

	public bool IsFaded()
	{
		return !fadeIn;
	}
}
