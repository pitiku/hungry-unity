using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour 
{
	public GameObject[] babies;
	public GameObject[] clouds;

	public GameObject foodPosition;

	Baby[] currentBabies;
	Food currentFood;
	Baby selectedBaby;

	enum LevelState
	{
		INIT,
		TUTORIAL,
		POWERUPS_INITIAL,
		READY,
		GAMEPLAY,
		POWERUPS_FINAL,
		RESULTS
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
			break;
		case LevelState.GAMEPLAY:
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
