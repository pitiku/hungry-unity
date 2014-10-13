using UnityEngine;
using System.Collections;

public class Gameplay : MonoBehaviour 
{
	[Range(1,3)]
	public int NumBabies = 2;
	public bool DiscardAllowed = false;

	public GameObject[] babies;
	public GameObject[] food;
	public GameObject[] prizes;
	
	public GameObject[] clouds;
	public GameObject[] babyLink;
	public GameObject foodLink;

	public LevelManager levelManager;

	Baby[] currentBabies;
	Food currentFood;

	enum eState
	{
		CLOUDS_IN,
		WAIT_INPUT,
		FEED_BABY
	};

	eState state;

	void Awake() 
	{
		
	}
	
	void Start() 
	{
		
	}
	
	void Update() 
	{
	
	}

	public void StartGameplay()
	{
	}
}
