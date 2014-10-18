using UnityEngine;
using System.Collections;

public class Gameplay : MonoBehaviour 
{
	[Range(1,3)]
	public int NumBabies = 2;
	public bool AllowDiscard = false;
	public bool AllowRepeat = false;

	public BabyData[] babyData;

	public AnimatedObject[] cloudLinks;
	public AnimatedObject foodLink;

	public LevelManager levelManager;

	bool PrizeSeasonActive = false;

	Baby[] currentBabies;
	CloudForBaby[] currentClouds;
	Food currentFood;
	int fedBaby;

	public float timeLeft;

	enum eState
	{
		IDLE,
		CLOUDS_IN,
		WAIT_INPUT,
		LAUNCH_FOOD,
		DISCARD_FOOD,
		FEED_BABY,
		CLOUD_OUT,
		FINISHED
	};

	eState state;
	float stateTimeStart;

	bool babyFed;
	Vector3 foodDest;
	Vector3 foodSrc;

	void Start() 
	{
		SetState(eState.IDLE);
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape)) 
		{
			Application.Quit(); 
		}

		//Update state
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
		SetBabiesAndFood();
	}

	void Update_CLOUDS_IN()
	{
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
		//Any baby pressed
		for(int i=0; i<currentBabies.Length; ++i)
		{
			if(currentBabies[i].GetComponent<MenuItem>().IsJustPressed())
			{
				fedBaby = i;
				babyFed = false;
				foodSrc = foodLink.transform.position;
				foodDest = currentBabies[fedBaby].mouth.transform.position;
				SetState(eState.LAUNCH_FOOD);
				return;
			}
		}

		//Food pressed
		if(currentFood.GetComponent<MenuItem>().IsJustPressed())
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
			BabiesPool.Instance.AddObject(currentFood.transform);
			SetState(eState.FEED_BABY);
		}
	}
	#endregion
	
	#region DISCARD_FOOD
	void Update_DISCARD_FOOD()
	{
		if(foodLink.IsFinished())
		{
			BabiesPool.Instance.AddObject(currentFood.transform);
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
			}
			else if(currentBabies[fedBaby].hunger <= 0)
			{
				Score.Instance.BabyFed(1);

				float fRand = Random.Range(0.0f, 1.0f);
				float fProb = (GetBabyData(currentBabies[fedBaby].baby).GetPrizeProbability() * (PrizeSeasonActive ? 2.0f : 1.0f));
				if(fRand < fProb || true)
				{
					//Prize
					BabiesPool.Instance.GetPrize(currentBabies[fedBaby].baby).Dropped(currentBabies[fedBaby].transform.position * 0.9f);
				}

				cloudLinks[GetCloudLinkIndex(fedBaby)].StartAnimation("Out");
				SetState(eState.CLOUD_OUT);
			}
			else
			{
				//Baby is still hungry
				SetState(eState.CLOUDS_IN);
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
			BabiesPool.Instance.AddObject(currentBabies[fedBaby].transform);
			CloudPool.Instance.AddObject(currentClouds[fedBaby].transform);
			currentBabies[fedBaby] = null;
			SetState(eState.CLOUDS_IN);
		}
	}
	#endregion

	#region FINISHED
	void Update_FINISHED()
	{
	}
	#endregion
	
	public void StartGameplay()
	{
		currentBabies = new Baby[NumBabies];
		currentClouds = new CloudForBaby[NumBabies];
		
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
		switch(NumBabies)
		{
		case 1:
			return 1;
		case 2:
			if(_index == 0)
				return 0;
			else
				return 2;
		case 3:
		default:
			return _index;
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

	void SetState(eState _state)
	{
		gameObject.SendMessage("Exit_" + state.ToString(), SendMessageOptions.DontRequireReceiver);
		state = _state;
		stateTimeStart = Time.time;
		gameObject.SendMessage("Enter_" + state.ToString(), SendMessageOptions.DontRequireReceiver);
	}
	
	float GetStateTime()
	{
		return Time.time - stateTimeStart;
	}
}
