using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour 
{
	public Gameplay gameplay;

	public AnimatedObject Items;
	public MenuItem ChainBoost;
	public MenuItem DoubleCoins;
	public MenuItem PrizeSeason;
	public MenuItem MegaChainBoost;
	public MenuItem BoletTime;
	public MenuItem FeederGloves;
	public MenuItem ExtraRainbow;

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
		Items.StartAnimation("In");
		
		ChainBoost.enabled = PlayerData.Instance.powerup_chainBoost > 0;
		MegaChainBoost.enabled = PlayerData.Instance.powerup_megaChainBoost > 0;
		DoubleCoins.enabled = PlayerData.Instance.powerup_doubleCoins > 0;
		PrizeSeason.enabled = PlayerData.Instance.powerup_prizeSeason > 0;
	}

	void Update_POWERUPS_INITIAL()
	{
		if(ChainBoost.IsJustPressed())
		{
			PlayerData.Instance.powerup_chainBoost -= 1;
			Score.Instance.ChainBoost(10);
		}
		if(MegaChainBoost.IsJustPressed())
		{
			PlayerData.Instance.powerup_megaChainBoost -= 1;
			Score.Instance.ChainBoost(30);
		}
		if(DoubleCoins.IsJustPressed())
		{
			PlayerData.Instance.powerup_doubleCoins -= 1;
			Score.Instance.ChainBoost(10);
		}
		if(PrizeSeason.IsJustPressed())
		{
			PlayerData.Instance.powerup_prizeSeason -= 1;
			Score.Instance.ChainBoost(10);
		}

		if(GetStateTime() > 2.0f)
		{
			Items.StartAnimation("Out");
			SetState(LevelState.READY);
		}
	}

	void End_POWERUPS_INITIAL()
	{
		PlayerData.Instance.Save();
	}
	#endregion

	#region READY
	void Enter_READY()
	{
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
		gameplay.StartGameplay();
	}

	void Update_GAMEPLAY()
	{
		if(gameplay.IsFinished())
		{
			SetState(LevelState.POWERUPS_FINAL);
		}
	}
	
	void Exit_()
	{
	}
	#endregion

	#region POWERUPS_FINAL
	void Update_POWERUPS_FINAL()
	{
		SetState(LevelState.RESULTS);
	}
	#endregion

	#region RESULTS
	void Update_RESULTS()
	{
		SetState(LevelState.FINISHED);
	}
	#endregion

	#region FINISHED
	void Update_FINISHED()
	{
	}
	#endregion
	
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
