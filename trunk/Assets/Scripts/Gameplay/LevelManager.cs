using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour 
{
	public Gameplay gameplay;

	public AnimatedObject Clouds;

	public AnimatedObject Items;
	public PowerUp_Level ChainBoost;
	public PowerUp_Level DoubleCoins;
	public PowerUp_Level PrizeSeason;
	public PowerUp_Level MegaChainBoost;

	public PowerUp_Level BoletTime;
	public MenuItem FeederGloves;

	public PowerUp_Level ExtraRainbow;
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
		Items.StartAnimation("In");

		ChainBoost.SetCount(PlayerData.Instance.powerup_chainBoost);
		ChainBoost.SetEnabled(PlayerData.Instance.powerup_chainBoost > 0);

		MegaChainBoost.SetCount(PlayerData.Instance.powerup_megaChainBoost);
		MegaChainBoost.SetEnabled(PlayerData.Instance.powerup_megaChainBoost > 0);

		DoubleCoins.SetCount(PlayerData.Instance.powerup_doubleCoins);
		DoubleCoins.SetEnabled(PlayerData.Instance.powerup_doubleCoins > 0);

		PrizeSeason.SetCount(PlayerData.Instance.powerup_prizeSeason);
		PrizeSeason.SetEnabled(PlayerData.Instance.powerup_prizeSeason > 0);
	}

	void Update_POWERUPS_INITIAL()
	{
		if(ChainBoost.menuItem.IsJustPressed())
		{
			PlayerData.Instance.powerup_chainBoost -= 1;
			PlayerData.Instance.Save();
			ChainBoost.SetCount(PlayerData.Instance.powerup_chainBoost);
			ChainBoost.SetEnabled(false);
			MegaChainBoost.SetEnabled(false);
			Score.Instance.ChainBoost();
		}
		if(MegaChainBoost.menuItem.IsJustPressed())
		{
			PlayerData.Instance.powerup_megaChainBoost -= 1;
			PlayerData.Instance.Save();
			MegaChainBoost.SetCount(PlayerData.Instance.powerup_megaChainBoost);
			MegaChainBoost.SetEnabled(false);
			ChainBoost.SetEnabled(false);
			Score.Instance.MegaChainBoost();
		}
		if(DoubleCoins.menuItem.IsJustPressed())
		{
			PlayerData.Instance.powerup_doubleCoins -= 1;
			PlayerData.Instance.Save();
			DoubleCoins.SetCount(PlayerData.Instance.powerup_doubleCoins);
			DoubleCoins.SetEnabled(false);
			Score.Instance.DoubleCoins();
		}
		if(PrizeSeason.menuItem.IsJustPressed())
		{
			PlayerData.Instance.powerup_prizeSeason -= 1;
			PlayerData.Instance.Save();
			PrizeSeason.SetCount(PlayerData.Instance.powerup_prizeSeason);
			PrizeSeason.SetEnabled(false);
			gameplay.PrizeSeason();
		}

		if(GetStateTime() > 2.0f)
		{
			SetState(LevelState.READY);
		}
	}

	void Exit_POWERUPS_INITIAL()
	{
		Items.StartAnimation("Out");
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
		Items.StartAnimation("GameplayIn");

		gameplay.StartGameplay();
	}

	void Update_GAMEPLAY()
	{
		if(FeederGloves.IsJustPressed())
		{

		}

		if(BoletTime.menuItem.IsJustPressed())
		{

		}

		if(gameplay.IsFinished())
		{
			SetState(LevelState.AFTER_GAMEPLAY);
		}
	}

	void Exit_GAMEPLAY()
	{
		Items.StartAnimation("GameplayOut");
		Score.Instance.Fail();
	}
	#endregion

	#region AFTER_GAMEPLAY
	void Update_AFTER_GAMEPLAY()
	{
		if(Items.IsFinished())
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
		Items.StartAnimation("FinalIn");
		
		ExtraRainbow.SetCount(PlayerData.Instance.powerup_extraRainbow);
		ExtraRainbow.SetEnabled(PlayerData.Instance.powerup_extraRainbow > 0);
	}

	void Update_POWERUPS_FINAL()
	{
		if(ExtraRainbow.menuItem.IsJustPressed())
		{
			PlayerData.Instance.powerup_extraRainbow -= 1;
			PlayerData.Instance.Save();
			ExtraRainbow.SetCount(PlayerData.Instance.powerup_extraRainbow);
			ExtraRainbow.SetEnabled(false);

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
		Items.StartAnimation("FinalOut");
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
