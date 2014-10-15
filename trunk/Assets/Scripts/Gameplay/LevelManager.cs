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
		//Exit state
		switch(state)
		{
		case LevelState.INIT:
			break;
		case LevelState.TUTORIAL:
			break;
		case LevelState.POWERUPS_INITIAL:
			break;
		case LevelState.READY:
			break;
		case LevelState.GAMEPLAY:
			break;
		case LevelState.POWERUPS_FINAL:
			break;
		case LevelState.RESULTS:
			break;
		}

		state = _state;
		stateTimeStart = Time.time;

		//Enter state
		switch(state)
		{
		case LevelState.INIT:
			break;
		case LevelState.TUTORIAL:
			break;
		case LevelState.POWERUPS_INITIAL:
			break;
		case LevelState.READY:
			TextReady.StartAnimation("In");
			break;
		case LevelState.GAMEPLAY:
			TextFeed.StartAnimation("In");
			gameplay.StartGameplay();
			break;
		case LevelState.POWERUPS_FINAL:
			break;
		case LevelState.RESULTS:
			break;
		}
	}

	float GetStateTime()
	{
		return Time.time - stateTimeStart;
	}
}
