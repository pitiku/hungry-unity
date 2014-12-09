using UnityEngine;
using System.Collections;

public class BabyData : MonoBehaviour 
{
	public GameConstants.eBabies BabyType;
	public float probability = 1.0f;
	public float prizeProbability = 0.1f;
	public int startLevel = 0;

	public bool IsBought()
	{
		return PlayerData.Instance.babyBought[(int)BabyType];
	}
	
	public bool IsUnlocked()
	{
		return PlayerData.Instance.babyUnlocked[(int)BabyType];
	}
	
	public float GetProbability()
	{
		return probability;
	}
	
	public float GetPrizeProbability()
	{
		return prizeProbability;
	}

	public int GetStartLevel()
	{
		return startLevel;
	}
}
