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
	Food currentFood;

	enum eState
	{
		IDLE,
		CLOUDS_IN,
		WAIT_INPUT,
		FEED_BABY
	};

	eState state;
	float stateTimeStart;
	
	void Awake() 
	{
		
	}
	
	void Start() 
	{
		state = eState.IDLE;
		StartGameplay();
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
			bool bFinished = true;
			for(int i=0; i<clouds.Length; ++i)
			{
				bFinished = bFinished && clouds[i].IsFinished();
			}

			bFinished = bFinished && foodLink.IsFinished();

			if(bFinished)
			{
				SetState(eState.WAIT_INPUT);
			}
			break;
		case eState.WAIT_INPUT:
			for(int i=0; i<currentBabies.Length; ++i)
			{
				if(currentBabies[i].GetComponent<MenuItem>().IsJustPressed())
				{
					SetState(eState.FEED_BABY);
					break;
				}
			}
			break;
		case eState.FEED_BABY:
			SetState(eState.CLOUDS_IN);
			break;
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
		}
	}
	
	float GetStateTime()
	{
		return Time.time - stateTimeStart;
	}

	public void StartGameplay()
	{
		currentBabies = new Baby[NumBabies];

		SetState(eState.CLOUDS_IN);
	}

	public void SetBabiesAndFood()
	{
		//Set babies
		for(int i=0; i<NumBabies; ++i)
		{
			if(currentBabies[i] == null)
			{
				bool done = false;
				while(!done)
				{
					currentBabies[i] = babies[Random.Range(0, babies.Length-1)];
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
			}
		}

		//Set food
		currentFood = GetFood(currentBabies[Random.Range(0, NumBabies)].baby);

		//Link babies and food
		for(int i=0; i<NumBabies; ++i)
		{
			int babyLinkIndex = GetBabyLinkIndex(i);
			currentBabies[i].transform.parent = babyLink[babyLinkIndex];
			currentBabies[i].transform.localPosition = Vector3.zero;
			clouds[babyLinkIndex].StartAnimation("In");
		}

		currentFood.transform.parent = foodLink.transform;
		currentFood.transform.localPosition = Vector3.zero;
		foodLink.StartAnimation("In");
	}

	public Food GetFood(GameConstants.eBabies _foodType)
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

	public int GetBabyLinkIndex(int _index)
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
}
