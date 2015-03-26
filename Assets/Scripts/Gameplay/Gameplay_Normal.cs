using UnityEngine;
using System.Collections;

public class Gameplay_Normal : SingletonMonoBehaviour<Gameplay_Normal> 
{
	const int MAX_BABIES = 3;

	[Range(1,3)]
	public int[] NumBabies;
	public int[] scoreToNextLevel;
	public bool[] AllowDiscard;
	public bool[] AllowRepeat;

	public float gameplayTime = 10.0f;
	public float gameplayTime_rainbowplus = 15.0f;
	public float gameplayTime_rainbowplusplus = 20.0f;
	public float successTimeIncrement = 2.5f;

	public Transform Pos_LeftOut;
	public Transform Pos_LeftIn;
	public Transform Pos_RightOut;
	public Transform Pos_RightIn;
	public Transform Pos_CenterOut;
	public Transform Pos_CenterIn;
	public Transform Pos_EatIn;
	public Transform Pos_EatOut;

	public Transform Pos_FoodInit;
	public Transform Pos_FoodIn;
	public Transform Pos_FoodOut;

	public float Time_FoodMoveEat = 0.25f;

	BabyData[] babyData;

	int currentLevel = 0;
	int currentLevelScore = 0;

	bool PrizeSeasonActive = false;

	Baby[] currentBabies;
	CloudForBaby[] currentClouds;
	CloudForBaby cloudEat;
	Food currentFood;
	int fedBaby;

	float timeLeft;
	float totalTime;

	public enum eState
	{
		IDLE,
		CLOUDS_IN,
		WAIT_INPUT,
		LAUNCH_FOOD,
		DISCARD_FOOD,
		FEED_BABY,
		CLOUD_OUT,
		FINISHING,
		FINISHED
	};

	eState state;
	float stateTime;

	bool inPlay = false;

	bool babyFed;
	Vector3 foodDest;
	Vector3 foodSrc;

	void Start() 
	{
		SetState(eState.IDLE);

		currentBabies = new Baby[MAX_BABIES];
		currentClouds = new CloudForBaby[MAX_BABIES];

		babyData = GetComponents<BabyData>();
	}

	void Update()
	{
		if(PauseManager.Instance.IsPaused())
		{
			return;
		}

		//Remove this at some point
		if (Input.GetKeyDown(KeyCode.Escape)) 
		{
			Application.Quit(); 
		}

		//Update time left
		if(inPlay)
		{
			timeLeft -= Time.deltaTime;
			if(timeLeft < 0) timeLeft = 0;
			Rainbow.Instance.SetValue(timeLeft / totalTime);
		}

		//Update state
		stateTime += Time.deltaTime;
		SendMessage("Update_" + state);
	}

	#region IDLE
	void Update_IDLE()
	{
	}
	#endregion
	
	#region CLOUDS_IN
	void Enter_CLOUDS_IN()
	{
		//Decide babies and food
		SetBabiesAndFood();
	}

	void Update_CLOUDS_IN()
	{
		//Check for clouds and food to be in place
		bool bFinished = true;
		foreach(CloudForBaby cloud in currentClouds)
		{
			if(cloud)
			{
				bFinished = bFinished && !cloud.IsMoving();
			}
		}
		
		bFinished = bFinished && !currentFood.IsMoving();

		if(bFinished)
		{
			SetState(eState.WAIT_INPUT);
		}
	}
	#endregion

	#region WAIT_INPUT
	void Update_WAIT_INPUT()
	{
		//Check if we run out of time
		if(timeLeft <= 0.0f)
		{
			SetState(eState.FINISHING);
			return;
		}

		//Any baby pressed
		for(int i=0; i<NumBabies[currentLevel]; ++i)
		{
			if(currentBabies[i].GetComponent<Pushable>().IsJustPressed())
			{
				fedBaby = i;
				babyFed = false;
				foodSrc = currentFood.transform.position;
				foodDest = Pos_EatIn.position + (currentBabies[fedBaby].mouth.transform.position - currentClouds[GetCloudLinkIndex(fedBaby)].transform.position);
				SetState(eState.LAUNCH_FOOD);

				if(currentFood.GetComponent<Living>())
				{
					currentFood.GetComponent<Living>().enabled = false;
				}

				currentClouds[GetCloudLinkIndex(fedBaby)].MoveTo(Pos_EatIn.position, 0.2f);
				
				AudioManager.Instance.PlayLaunch();

				return;
			}
		}

		/*
		//Food pressed
		if(currentFood.GetComponent<Pushable>().IsJustPressed())
		{
			foodLink.StartAnimation("Discard");
			SetState(eState.DISCARD_FOOD);
		}
		*/
	}
	#endregion

	#region LAUNCH_FOOD
	void Update_LAUNCH_FOOD()
	{
		if(GetStateTime() > 0.0f && !babyFed)
		{
			babyFed = true;
			currentBabies[fedBaby].Eat(currentBabies[fedBaby].baby == currentFood.foodType);
		}

		float fPerc = Mathf.Min(1.0f, GetStateTime() / Time_FoodMoveEat);

		currentFood.transform.position = Vector3.Lerp(foodSrc, foodDest, fPerc);
		currentFood.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, fPerc); 
		
		if(fPerc >= 1.0f)
		{
			BabiesPool.Instance.ReturnToPool(currentFood.transform);
			if(currentFood.GetComponent<Living>())
			{
				currentFood.GetComponent<Living>().enabled = true;
			}

			SetState(eState.FEED_BABY);
			cloudEat = currentClouds[GetCloudLinkIndex(fedBaby)];
			currentClouds[GetCloudLinkIndex(fedBaby)] = null;
		}
	}
	#endregion
	
	#region DISCARD_FOOD
	void Update_DISCARD_FOOD()
	{
		if(!currentFood.IsMoving())
		{
			BabiesPool.Instance.ReturnToPool(currentFood.transform);
			SetState(eState.CLOUDS_IN);
		}
	}
	#endregion
	
	#region FEED_BABY
	void Update_FEED_BABY()
	{
		//if(!currentBabies[fedBaby].IsEating())
		{
			if(currentBabies[fedBaby].baby != currentFood.foodType)
			{
				Score.Instance.Fail();
				currentLevelScore = 0;
				
				cloudEat.MoveTo(Pos_EatOut.position, 0.2f);
				SetState(eState.CLOUD_OUT);

				if(timeLeft <= 0.0f)
				{
					SetState(eState.FINISHING);
				}
			}
			else if(currentBabies[fedBaby].hunger <= 0)
			{
				Score.Instance.BabyFed(1);
				currentLevelScore++;

				timeLeft = Mathf.Min(totalTime, timeLeft + 2.5f);

				//Drop prize
				float fRand = Random.Range(0.0f, 1.0f);
				float fProb = GetBabyData(currentBabies[fedBaby].baby).GetPrizeProbability() * (PrizeSeasonActive ? 2.0f : 1.0f);
				if(fRand <= fProb)
				{
					if(BabiesPool.Instance.GetPrize(currentBabies[fedBaby].baby))
					{
						BabiesPool.Instance.GetPrize(currentBabies[fedBaby].baby).Dropped(currentBabies[fedBaby].transform.position);
					}
				}

				cloudEat.MoveTo(Pos_EatOut.position, 0.2f);
				SetState(eState.CLOUD_OUT);
			}
			else
			{
				if(timeLeft <= 0.0f)
				{
					SetState(eState.FINISHING);
				}
				else
				{
					//Baby is still hungry
					SetState(eState.CLOUDS_IN);
				}
			}
		}
	}
	#endregion

	#region CLOUD_OUT
	void Update_CLOUD_OUT()
	{
		//if(!cloudEat.IsMoving())
		{
			cloudEat.transform.localScale = Vector3.one;
			BabiesPool.Instance.ReturnToPool(currentBabies[fedBaby].transform);
			CloudPool.Instance.AddObject(cloudEat.transform);
			currentBabies[fedBaby] = null;

			//Check level
			if(currentLevel == 0 || currentLevelScore >= scoreToNextLevel[currentLevel])
			{
				currentLevel++;
				currentLevelScore = 0;
				Score.Instance.SetLevel(currentLevel + 1);

				//Unlock babies
				foreach(BabyData data in babyData)
				{
					if(!data.IsUnlocked() && data.GetStartLevel() <= currentLevel)
					{
						PlayerData.Instance.UnlockBaby((int)data.BabyType);
					}
				}

				if(currentLevel >= NumBabies.Length)
				{
					//Game completed!
				}
				ProgressBar.Instance.CompleteBar();
			}
			else
			{
				ProgressBar.Instance.SetProgress((float)currentLevelScore / (float)scoreToNextLevel[currentLevel]);
			}

			SetState(eState.CLOUDS_IN);
		}
	}
	#endregion

	#region FINISHING
	void Enter_FINISHING()
	{
		Rainbow.Instance.EnableStars(false);

		inPlay = false;

		currentFood.MoveTo(Pos_FoodOut, 0.2f);
		foreach(CloudForBaby cloud in currentClouds)
		{
			if(cloud)
			{
				cloud.MoveTo(Pos_CenterOut.position, 0.2f);
			}
		}
	}
	
	void Update_FINISHING()
	{
		bool finished = !currentFood.IsMoving();

		foreach(CloudForBaby cloud in currentClouds)
		{
			if(cloud)
			{
				finished = finished && !cloud.IsMoving();
			}
		}

		if(finished)
		{
			SetState(eState.FINISHED);
		}
	}
	#endregion
	
	#region FINISHED
	void Enter_FINISHED()
	{
		if(currentFood)
		{
			BabiesPool.Instance.ReturnToPool(currentFood.transform);
		}

		foreach(CloudForBaby cloud in currentClouds)
		{
			if(cloud)
			{
				cloud.transform.localScale = Vector3.one;
			}
		}

		foreach(Baby baby in currentBabies)
		{
			if(baby)
			{
				BabiesPool.Instance.ReturnToPool(baby.transform);
			}
		}

		foreach(CloudForBaby cloud in currentClouds)
		{
			if(cloud)
			{
				CloudPool.Instance.AddObject(cloud.transform);
			}
		}

		for(int i=0; i<currentBabies.Length; ++i)
		{
			currentBabies[i] = null;
		}
	}
	
	void Update_FINISHED()
	{
	}
	#endregion

	public void ResetGameplay()
	{
		currentLevel = 9;
		Score.Instance.SetLevel(currentLevel + 1);
	}

	public void StartGameplay()
	{
		//Calculate total time
		if(PlayerData.Instance.upgrade_rainbowplusplus)
		{
			totalTime = gameplayTime_rainbowplusplus;
		}
		else if(PlayerData.Instance.upgrade_rainbowplus)
		{
			totalTime = gameplayTime_rainbowplus;
		}
		else
		{
			totalTime = gameplayTime;
		}
		timeLeft = totalTime;
		inPlay = true;

		Rainbow.Instance.EnableStars();

		SetState(eState.CLOUDS_IN);
	}
	
	void SetBabiesAndFood()
	{
		//Set babies
		for(int i=0; i<NumBabies[currentLevel]; ++i)
		{
			if(currentBabies[i] == null)
			{
				//Get random baby
				GameConstants.eBabies babyType = GetRandomBabyType();
				currentBabies[i] = BabiesPool.Instance.GetBaby(babyType);
				currentBabies[i].Idle();

				//Link a cloud
				int cloudLinkIndex = GetCloudLinkIndex(i);

				currentClouds[cloudLinkIndex] = CloudPool.Instance.GetCloud();
				currentClouds[cloudLinkIndex].transform.parent = null;

				//Link baby to cloud
				currentBabies[i].transform.parent = currentClouds[cloudLinkIndex].babyLink;
				currentBabies[i].transform.localPosition = Vector3.zero;
				if(cloudLinkIndex == 0)
				{
					currentClouds[cloudLinkIndex].transform.localScale = new Vector3(-1, 1, 1);
				}

				currentClouds[cloudLinkIndex].SetPos(cloudLinkIndex == 0 ? Pos_LeftOut : cloudLinkIndex == 1 ? Pos_CenterOut : Pos_RightOut);
				currentClouds[cloudLinkIndex].MoveTo(cloudLinkIndex == 0 ? Pos_LeftIn : cloudLinkIndex == 1 ? Pos_CenterIn : Pos_RightIn, 0.2f);
			}
		}

		//Set food
		GameConstants.eBabies foodType;
		if(NumBabies[currentLevel] <= 1)
		{
			foodType = currentBabies[0].baby;
		}
		else
		{
			foodType = GetRandomFoodType();
		}
		currentFood = BabiesPool.Instance.GetFood(foodType);

		currentFood.transform.parent = null;
		currentFood.transform.localScale = Vector3.one;
		currentFood.gameObject.SetActive(true);

		currentFood.SetPos(Pos_FoodInit);
		currentFood.MoveTo(Pos_FoodIn, 0.2f);
	}

	GameConstants.eBabies GetRandomBabyType()
	{
		float totalProbability = 0.0f;
		foreach(BabyData data in babyData)
		{
			if(data.IsBought() && data.GetStartLevel() <= currentLevel && (AllowRepeat[currentLevel] || !IsPlaying(data.BabyType)))
			{
				totalProbability += data.GetProbability();
			}
		}

		float fRand = Random.Range(0.0f, totalProbability);
		totalProbability = 0.0f;
		for(int i=0; i<babyData.Length; ++i)
		{
			if(babyData[i].IsBought() && babyData[i].GetStartLevel() <= currentLevel && (AllowRepeat[currentLevel] || !IsPlaying(babyData[i].BabyType)))
			{
				if(fRand <= totalProbability + babyData[i].GetProbability())
				{
					return babyData[i].BabyType;
				}
				totalProbability += babyData[i].GetProbability();
			}
		}

		//Error
		Debug.LogError("ERROR: GetRandomBabyType");
		return GameConstants.eBabies.HUMAN;
	}

	GameConstants.eBabies GetRandomFoodType()
	{
		float totalProbability = 0.0f;
		foreach(BabyData data in babyData)
		{
			if(IsPlaying(data.BabyType) || (!data.IsBought() && data.GetStartLevel() <= currentLevel))
			{
				totalProbability += data.GetProbability();
			}
		}
		
		float fRand = Random.Range(0.0f, totalProbability);
		totalProbability = 0.0f;
		for(int i=0; i<babyData.Length; ++i)
		{
			if(IsPlaying(babyData[i].BabyType) || (!babyData[i].IsBought() && babyData[i].GetStartLevel() <= currentLevel))
			{
				if(fRand <= totalProbability + babyData[i].GetProbability())
				{
					return babyData[i].BabyType;
				}
				totalProbability += babyData[i].GetProbability();
			}
		}
		
		//Error
		Debug.LogError("ERROR: GetRandomFoodType");
		return GameConstants.eBabies.HUMAN;
	}

	public BabyData GetBabyData(GameConstants.eBabies _babyType)
	{
		foreach(BabyData data in babyData)
		{
			if(data.BabyType == _babyType)
			{
				return data;
			}
		}

		//Error
		Debug.LogError("ERROR: GetBabyData");
		return null;
	}

	bool IsPlaying(GameConstants.eBabies _babyType)
	{
		foreach(Baby baby in currentBabies)
		{
			if(baby && baby.baby == _babyType)
			{
				return true;
			}
		}
		return false;
	}

	int GetCloudLinkIndex(int _index)
	{
		if(NumBabies[currentLevel] == 1)
		{
			return 1;
		}
		else
		{
			switch(_index)
			{
			case 0:
				return 0;
			case 1:
				return 2;
			case 2:
				return 1;
			default:
				return _index;
			}
		}
	}

	public void PrizeSeason()
	{
		PrizeSeasonActive = true;
	}

	public bool IsFinished()
	{
		return state == eState.FINISHED;
	}

	public void ExitFromPause()
	{
		SetState(eState.FINISHING);
	}

	void SetState(eState _state)
	{
		gameObject.SendMessage("Exit_" + state.ToString(), SendMessageOptions.DontRequireReceiver);
		state = _state;
		stateTime = 0;
		gameObject.SendMessage("Enter_" + state.ToString(), SendMessageOptions.DontRequireReceiver);
	}
	
	float GetStateTime()
	{
		return stateTime;
	}
}
