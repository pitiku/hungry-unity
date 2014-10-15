using UnityEngine;
using System.Collections;

public class Gameplay : MonoBehaviour 
{
	[Range(1,3)]
	public int NumBabies = 2;
	public bool DiscardAllowed = false;

	public Baby[] babies;
	public Food[] food;
	public Prize[] prizes;
	
	public AnimatedObject[] clouds;
	public Transform[] babyLink;
	public AnimatedObject foodLink;

	public LevelManager levelManager;

	Baby[] currentBabies;
	AnimatedObject[] currentClouds;
	Food currentFood;
	int fedBaby;

	public float timeLeft;

	enum eState
	{
		IDLE,
		CLOUDS_IN,
		WAIT_INPUT,
		LAUNCH_FOOD,
		FEED_BABY,
		CLOUD_OUT,
		FINISHED
	};

	eState state;
	float stateTimeStart;

	bool babyFed;
	Vector3 foodDest;
	Vector3 foodSrc;

	void Awake() 
	{
		
	}
	
	void Start() 
	{
		state = eState.IDLE;
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
		case eState.CLOUDS_IN:
		{
			bool bFinished = true;
			for(int i=0; i<clouds.Length; ++i)
			{
				bFinished = bFinished && clouds[i].IsFinished();
			}

			bFinished = bFinished && foodLink.IsFinished();

			if(bFinished)
			{
				foodLink.StopAnimator();
				SetState(eState.WAIT_INPUT);
			}
			break;
		}

		case eState.WAIT_INPUT:
			for(int i=0; i<currentBabies.Length; ++i)
			{
				if(currentBabies[i].GetComponent<MenuItem>().IsJustPressed())
				{
					fedBaby = i;
					babyFed = false;
					foodSrc = foodLink.transform.position;
					foodDest = currentBabies[fedBaby].mouth.transform.position;
					SetState(eState.LAUNCH_FOOD);
					break;
				}
			}
			break;
			
		case eState.LAUNCH_FOOD:
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
				currentFood.transform.parent = null;
				currentFood.gameObject.SetActive(false);
				SetState(eState.FEED_BABY);
			}
			break;
		}

		case eState.FEED_BABY:
		{
			if(!currentBabies[fedBaby].IsEating())
			{
				currentClouds[fedBaby].StartAnimation("Out");
				SetState(eState.CLOUD_OUT);
			}
			break;
		}

		case eState.CLOUD_OUT:
		{
			if(currentClouds[fedBaby].IsFinished())
			{
				currentBabies[fedBaby].transform.parent = null;
				currentBabies[fedBaby] = null;
				SetState(eState.CLOUDS_IN);
			}
			break;
		}
		}
	}
	
	void SetState(eState _state)
	{
		//Exit state
		switch(state)
		{
		case eState.CLOUDS_IN:
			break;
		case eState.WAIT_INPUT:
			break;
		case eState.FEED_BABY:
			break;
		case eState.CLOUD_OUT:
			break;
		}
		
		state = _state;
		stateTimeStart = Time.time;
		
		//Enter state
		switch(state)
		{
		case eState.CLOUDS_IN:
			SetBabiesAndFood();
			break;
		case eState.WAIT_INPUT:
			break;
		case eState.FEED_BABY:
			break;
		case eState.CLOUD_OUT:
			break;
		}
	}
	
	float GetStateTime()
	{
		return Time.time - stateTimeStart;
	}

	void SetBabiesAndFood()
	{
		//Set babies
		for(int i=0; i<NumBabies; ++i)
		{
			if(currentBabies[i] == null)
			{
				bool done = false;
				while(!done)
				{
					currentBabies[i] = babies[Random.Range(0, babies.Length)];
					done = true;
					for(int j=0; j<NumBabies; ++j)
					{
						if(i != j && currentBabies[j] && currentBabies[j].baby == currentBabies[i].baby)
						{
							done = false;
							break;
						}
					}
				}

				currentBabies[i].Idle();
				
				int babyLinkIndex = GetBabyLinkIndex(i);
				currentBabies[i].transform.parent = babyLink[babyLinkIndex];
				currentBabies[i].transform.localPosition = Vector3.zero;
				if(babyLinkIndex == 0)
				{
					Vector3 scale = currentBabies[i].transform.localScale;
					scale.x = -1;
					currentBabies[i].transform.localScale = scale;
				}
				else
				{
					Vector3 scale = currentBabies[i].transform.localScale;
					scale.x = 1;
					currentBabies[i].transform.localScale = scale;
				}
				
				currentClouds[i] = clouds[babyLinkIndex];
				clouds[babyLinkIndex].StartAnimation("In");
			}
		}

		//Set food
		currentFood = GetFood(currentBabies[Random.Range(0, NumBabies)].baby);

		currentFood.transform.parent = foodLink.transform;
		currentFood.transform.localPosition = Vector3.zero;
		currentFood.transform.localScale = Vector3.one;
		currentFood.gameObject.SetActive(true);
		foodLink.StartAnimation("In");
	}

	Food GetFood(GameConstants.eBabies _foodType)
	{
		for(int i=0; i<food.Length; ++i)
		{
			if(food[i].foodType == _foodType)
			{
				return food[i];
			}
		}
		return null;
	}

	int GetBabyLinkIndex(int _index)
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

	public void StartGameplay()
	{
		currentBabies = new Baby[NumBabies];
		currentClouds = new AnimatedObject[NumBabies];
		
		SetState(eState.CLOUDS_IN);
	}
	
	public bool IsFinished()
	{
		return state == eState.FINISHED;
	}
}
