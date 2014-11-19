using UnityEngine;
using System.Collections;

public class LevelManager : SingletonMonoBehaviour<LevelManager> 
{
	public AnimatedObject Clouds;

	bool bExtraRainbowUsed = false;

	public AnimatedObject TextReady;
	public AnimatedObject TextFeed;

	enum LevelState
	{
		INIT,
		TUTORIAL,
		POWERUPS_INITIAL,
		READY,
		FEED,
		GAMEPLAY,
		AFTER_GAMEPLAY,
		POWERUPS_FINAL,
		RESULTS,
		FINISHED
	};

	LevelState state;
	float stateTimeStart;

	void Start()
	{
		SetState(LevelState.INIT);
		Clouds.StartAnimation("In");
	}
	
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape)) 
		{
			Application.Quit(); 
		}

		//Update current state
		SendMessage("Update_" + state.ToString());
	}

	#region INIT
	void Enter_INIT()
	{
		Rainbow.Instance.SetValue(1.0f);
	}

	void Update_INIT()
	{
		if(GetStateTime() > 0.5f)
		{
			SetState(LevelState.TUTORIAL);
		}
	}
	#endregion

	#region TUTORIAL
	void Enter_TUTORIAL()
	{
	}

	void Update_TUTORIAL()
	{
		SetState(LevelState.POWERUPS_INITIAL);
	}
	#endregion

	#region POWERUPS_INITIAL
	void Enter_POWERUPS_INITIAL()
	{
		Score.Instance.AnimateIn();

		Items.Instance.ShowInitial();
	}

	void Update_POWERUPS_INITIAL()
	{
		Items.Instance.CheckInitialInput();

		if(GetStateTime() > 2.0f)
		{
			SetState(LevelState.READY);
		}
	}

	void Exit_POWERUPS_INITIAL()
	{
		Items.Instance.HideInitial();

		PlayerData.Instance.Save();
	}
	#endregion

	#region READY
	void Enter_READY()
	{
		Rainbow.Instance.SetValue(1.0f);

		TextReady.StartAnimation("In");
	}

	void Update_READY()
	{
		if(TextReady.IsFinished())
		{
			SetState(LevelState.FEED);
		}
	}
	#endregion

	#region FEED
	void Enter_FEED()
	{
		TextFeed.StartAnimation("In");
	}

	void Update_FEED()
	{
		if(TextFeed.IsFinished())
		{
			SetState(LevelState.GAMEPLAY);
		}
	}
	#endregion

	#region GAMEPLAY
	void Enter_GAMEPLAY()
	{
		Items.Instance.ShowGameplay();

		Gameplay.Instance.StartGameplay();
	}

	void Update_GAMEPLAY()
	{
		Items.Instance.CheckGameplayInput();

		if(Gameplay.Instance.IsFinished())
		{
			SetState(LevelState.AFTER_GAMEPLAY);
		}
	}

	void Exit_GAMEPLAY()
	{
		Items.Instance.HideGameplay();
		Score.Instance.Fail();
	}
	#endregion

	#region AFTER_GAMEPLAY
	void Update_AFTER_GAMEPLAY()
	{
		if(Items.Instance.IsFinished())
		{
			if(!bExtraRainbowUsed && PlayerData.Instance.powerup_extraRainbow > 0)
			{
				SetState(LevelState.POWERUPS_FINAL);
			}
			else
			{
				SetState(LevelState.RESULTS);
			}
		}
	}
	#endregion

	#region POWERUPS_FINAL
	void Enter_POWERUPS_FINAL()
	{
		Items.Instance.ShowFinal();
	}

	void Update_POWERUPS_FINAL()
	{
		if(Items.Instance.CheckFinalInput())
		{
			bExtraRainbowUsed = true;

			SetState(LevelState.READY);
		}
		else if(GetStateTime() > 2.0f)
		{
			SetState(LevelState.RESULTS);
		}
	}

	void Exit_POWERUPS_FINAL()
	{
		Items.Instance.HideFinal();
	}

	#endregion

	#region RESULTS
	void Enter_RESULTS()
	{
		Score.Instance.AnimateOut();
		ResultsScreen.Instance.Show();
	}

	void Update_RESULTS()
	{
		if(Input.anyKeyDown)
		{
			SetState(LevelState.FINISHED);
		}
	}

	void Exit_RESULTS()
	{
		ResultsScreen.Instance.Hide();
	}
	#endregion

	#region FINISHED
	void Enter_FINISHED()
	{
		Clouds.StartAnimation("Out");
	}

	void Update_FINISHED()
	{
		if(Clouds.IsFinished())
		{
			Application.LoadLevel("MainMenu");
		}
	}
	#endregion

	public void ExitFromPause()
	{
		Score.Instance.AnimateOut();
		SetState(LevelState.FINISHED);
	}

	void SetState(LevelState _state)
	{
		//Exit current state
		gameObject.SendMessage("Exit_" + state.ToString(), SendMessageOptions.DontRequireReceiver);
		
		state = _state;
		stateTimeStart = Time.time;
		
		//Enter new state
		gameObject.SendMessage("Enter_" + state.ToString(), SendMessageOptions.DontRequireReceiver);
	}
	
	float GetStateTime()
	{
		return Time.time - stateTimeStart;
	}
}
