using UnityEngine;
using System.Collections;

public class Gameplay : MonoBehaviour 
{
	[Range(1,3)]
	public int NumBabies = 2;
	public bool DiscardAllowed = false;
	public bool AllowRepeated = true;

	public Baby[] babies;
	public Food[] food;
	public Prize[] prizes;
	
	public GameObject[] clouds;
	public GameObject[] babyLink;
	public GameObject foodLink;

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
			SetState(eState.WAIT_INPUT);
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
				currentBabies[i] = babies[Random.Range(0, NumBabies-1)];
			}
		}

		//Set food
		currentFood = GetFood(currentBabies[Random.Range(0, NumBabies)].baby);

		//Link babies and food
		for(int i=0; i<NumBabies; ++i)
		{
			currentBabies[i].transform.parent = GetBabyLink(i);
			currentBabies[i].transform.localPosition = Vector3.zero;
		}

		currentFood.transform.parent = foodLink.transform;
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

	public Transform GetBabyLink(int _index)
	{
		switch(NumBabies)
		{
		case 1:
			return babyLink[1].transform;
		case 2:
			if(_index == 0)
				return babyLink[0].transform;
			else
				return babyLink[2].transform;
		case 3:
			return babyLink[_index].transform;
		}
		return null;
	}
}
