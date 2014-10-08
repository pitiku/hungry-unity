using UnityEngine;
using System.Collections;

public class Splash : MonoBehaviour {

	enum SplashState
	{
		INIT,
		FADEIN,
		SHOW,
		FADEOUT
	};

	SplashState state = SplashState.INIT;
	float m_fStateTimeStart;

	void SetState(SplashState _state)
	{
		state = _state;
		m_fStateTimeStart = Time.time;
	}

	void Update () 
	{
		switch (state) 
		{
		case SplashState.INIT:
			if((Time.time - m_fStateTimeStart) > 0.2f)
			{
				FadeManager.Instance.FadeIn(0.5f);
				SetState(SplashState.FADEIN);
			}
			break;

		case SplashState.FADEIN:
			if(FadeManager.Instance.IsFinished())
			{
				SetState(SplashState.SHOW);
			}
			break;
		
		case SplashState.SHOW:
			if((Time.time - m_fStateTimeStart) > 2.0f || Input.anyKey)
			{
				FadeManager.Instance.FadeOut(0.5f);
				SetState(SplashState.FADEOUT);
			}
			break;

		case SplashState.FADEOUT:
			if(FadeManager.Instance.IsFinished())
			{
				Application.LoadLevel("MainMenu");
			}
			break;
		}
	}
}
