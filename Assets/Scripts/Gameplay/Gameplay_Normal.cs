using UnityEngine;
using System.Collections;

public class Gameplay_Normal : SingletonMonoBehaviour<Gameplay_Normal> 
{
	const int MAX_BABIES = 3;

	[Range(1,3)]
	public int NumBabies = 2;
	public bool AllowDiscard = false;
	public bool AllowRepeat = false;
	public float gameplayTime = 10.0f;
	public float gameplayTime_rainbowplus = 15.0f;
	public float gameplayTime_rainbowplusplus = 20.0f;
	public float successTimeIncrement = 2.5f;

	public BabyData[] babyData;

	public AnimatedObject[] cloudLinks;
	public AnimatedObject foodLink;

	bool PrizeSeasonActive = false;

	Baby[] currentBabies;
	CloudForBaby[] currentClouds;
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

	public eState state;
	float stateTime;

	bool inPlay = false;

	bool babyFed;
	Vector3 foodDest;
	Vector3 foodSrc;

	void Start() 
	{
		SetState(eState.IDLE);
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
		foreach(AnimatedObject cloud in cloudLinks)
		{
			bFinished = bFinished && cloud.IsFinished();
		}
		
		bFinished = bFinished && foodLink.IsFinished();

		if(bFinished)
		{
			foodLink.StopAnimator();
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
		for(int i=0; i<NumBabies; ++i)
		{
			if(currentBabies[i].GetComponent<Pushable>().IsJustPressed())
			{
				fedBaby = i;
				babyFed = false;
				foodSrc = foodLink.transform.position;
				foodDest = currentBabies[fedBaby].mouth.transform.position;
				SetState(eState.LAUNCH_FOOD);
				
				AudioManager.Instance.PlayLaunch();

				return;
			}
		}

		//Food pressed
		if(currentFood.GetComponent<Pushable>().IsJustPressed())
		{
			foodLink.StartAnimation("Discard");
			SetState(eState.DISCARD_FOOD);
		}
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
		
		float fPerc = Mathf.Min(1.0f, GetStateTime() / 0.3f);

		foodLink.transform.position = foodSrc + (foodDest - foodSrc) * fPerc;
		foodLink.transform.localScale = Vector3.one * Mathf.Pow((1-fPerc), 0.3f);
		
		if(fPerc >= 1.0f)
		{
			BabiesPool.Instance.ReturnToPool(currentFood.transform);
			SetState(eState.FEED_BABY);
		}
	}
	#endregion
	
	#region DISCARD_FOOD
	void Update_DISCARD_FOOD()
	{
		if(foodLink.IsFinished())
		{
			BabiesPool.Instance.ReturnToPool(currentFood.transform);
			SetState(eState.CLOUDS_IN);
		}
	}
	#endregion
	
	#region FEED_BABY
	void Update_FEED_BABY()
	{
		if(!currentBabies[fedBaby].IsEating())
		{
			if(currentBabies[fedBaby].baby != currentFood.foodType)
			{
				Score.Instance.Fail();

				cloudLinks[GetCloudLinkIndex(fedBaby)].StartAnimation("Out");
				SetState(eState.CLOUD_OUT);

				if(timeLeft <= 0.0f)
				{
					SetState(eState.FINISHING);
				}
			}
			else if(currentBabies[fedBaby].hunger <= 0)
			{
				Score.Instance.BabyFed(1);
				timeLeft = Mathf.Min(totalTime, timeLeft + 2.5f);

				//Drop prize
				float fRand = Random.Range(0.0f, 1.0f);
				float fProb = (GetBabyData(currentBabies[fedBaby].baby).GetPrizeProbability() * (PrizeSeasonActive ? 2.0f : 1.0f));
				if(fRand < fProb)
				{
					if(BabiesPool.Instance.GetPrize(currentBabies[fedBaby].baby))
					{
						BabiesPool.Instance.GetPrize(currentBabies[fedBaby].baby).Dropped(currentBabies[fedBaby].transform.position);
					}
				}

				cloudLinks[GetCloudLinkIndex(fedBaby)].StartAnimation("Out");
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
		if(cloudLinks[GetCloudLinkIndex(fedBaby)].IsFinished())
		{
			cloudLinks[GetCloudLinkIndex(fedBaby)].transform.localScale = Vector3.one;
			BabiesPool.Instance.ReturnToPool(currentBabies[fedBaby].transform);
			CloudPool.Instance.AddObject(currentClouds[fedBaby].transform);
			currentBabies[fedBaby] = null;

			//NumBabies++;
			//if(NumBabies > 3) NumBabies = 3;

			SetState(eState.CLOUDS_IN);
		}
	}
	#endregion

	#region FINISHING
	void Enter_FINISHING()
	{
		Rainbow.Instance.EnableStars(false);

		inPlay = false;

		foodLink.StartAnimation("Discard");
		foreach(AnimatedObject cloud  in cloudLinks)
		{
			cloud.StartAnimation("Out");
		}
	}
	
	void Update_FINISHING()
	{
		bool finished = foodLink.IsFinished();

		foreach(AnimatedObject cloud in cloudLinks)
		{
			finished = finished && cloud.IsFinished();
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

		foreach(AnimatedObject cloud in cloudLinks)
		{
			cloud.transform.localScale = Vector3.one;
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
	
	public void StartGameplay()
	{
		currentBabies = new Baby[MAX_BABIES];
		currentClouds = new CloudForBaby[MAX_BABIES];

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
		for(int i=0; i<NumBabies; ++i)
		{
			if(currentBabies[i] == null)
			{
				//Get random baby
				GameConstants.eBabies babyType = GetRandomBabyType();
				currentBabies[i] = BabiesPool.Instance.GetBaby(babyType);
				currentBabies[i].Idle();

				//Link a cloud
				int cloudLinkIndex = GetCloudLinkIndex(i);
				currentClouds[i] = CloudPool.Instance.GetCloud();
				currentClouds[i].transform.parent = cloudLinks[cloudLinkIndex].transform;
				currentClouds[i].transform.localPosition = Vector3.zero;

				//Link baby to cloud
				currentBabies[i].transform.parent = currentClouds[i].babyLink;
				currentBabies[i].transform.localPosition = Vector3.zero;
				if(cloudLinkIndex == 0)
				{
					cloudLinks[cloudLinkIndex].transform.localScale = new Vector3(-1, 1, 1);
				}

				cloudLinks[cloudLinkIndex].StartAnimation("In");
			}
		}

		//Set food
		currentFood = BabiesPool.Instance.GetFood(currentBabies[Random.Range(0, NumBabies)].baby);

		currentFood.transform.parent = foodLink.transform;
		currentFood.transform.localPosition = Vector3.zero;
		currentFood.transform.localScale = Vector3.one;
		currentFood.gameObject.SetActive(true);
		foodLink.StartAnimation("In");
	}

	GameConstants.eBabies GetRandomBabyType()
	{
		float totalProbability = 0.0f;
		foreach(BabyData data in babyData)
		{
			if(data.IsAvailable() && (AllowRepeat || !IsPlaying(data.BabyType)))
			{
				totalProbability += data.GetProbability();
			}
		}

		float fRand = Random.Range(0.0f, totalProbability);
		totalProbability = 0.0f;
		for(int i=0; i<babyData.Length; ++i)
		{
			if(babyData[i].IsAvailable() && (AllowRepeat || !IsPlaying(babyData[i].BabyType)))
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
		return GameConstants.eBabies.ANTEATER;
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
		if(NumBabies == 1)
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
