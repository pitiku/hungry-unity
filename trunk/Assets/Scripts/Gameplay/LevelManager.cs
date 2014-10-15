using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour 
{
	public Gameplay gameplay;

	public SpriteRenderer[] BeforeItems;
	public SpriteRenderer[] DuringItems;
	public SpriteRenderer ExtraRainbow;

	public AnimatedObject TextReady;
	public AnimatedObject TextFeed;

	enum LevelState
	{
		INIT,
		TUTORIAL,
		POWERUPS_INITIAL,
		READY,
		GAMEPLAY,
		POWERUPS_FINAL,
		RESULTS,
		FINISHED
	};

	LevelState state;
	float stateTimeStart;

	void Start()
	{
		SetState(LevelState.INIT);
	}
	
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape)) 
		{
			Application.Quit(); 
		}

		//Update state
		switch(state)
		{
		case LevelState.INIT:
			SetState(LevelState.TUTORIAL);
			break;
		case LevelState.TUTORIAL:
			SetState(LevelState.POWERUPS_INITIAL);
			break;
		case LevelState.POWERUPS_INITIAL:
			SetState(LevelState.READY);
			break;
		case LevelState.READY:
			if(TextReady.IsFinished())
			{
				SetState(LevelState.GAMEPLAY);
			}
			break;
		case LevelState.GAMEPLAY:
			if(gameplay.IsFinished())
			{
				SetState(LevelState.POWERUPS_FINAL);
			}
			break;
		case LevelState.POWERUPS_FINAL:
			SetState(LevelState.RESULTS);
			break;
		case LevelState.RESULTS:
			SetState(LevelState.FINISHED);
			break;
		case LevelState.FINISHED:
			break;
		}
	}

	void SetState(LevelState _state)
	{
		gameObject.SendMessage("Exit_" + state.ToString(), SendMessageOptions.DontRequireReceiver);

		state = _state;
		stateTimeStart = Time.time;

		gameObject.SendMessage("Enter_" + state.ToString(), SendMessageOptions.DontRequireReceiver);
	}

	void Enter_READY()
	{
		TextReady.StartAnimation("In");
		Rainbow.Instance.SetValue(1.0f);
		Score.Instance.AnimateIn();
	}
	
	void Enter_GAMEPLAY()
	{
		TextFeed.StartAnimation("In");
		gameplay.StartGameplay();
	}
	
	float GetStateTime()
	{
		return Time.time - stateTimeStart;
	}
}
