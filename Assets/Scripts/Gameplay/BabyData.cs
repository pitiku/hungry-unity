using UnityEngine;
using System.Collections;

public class BabyData : MonoBehaviour 
{
	public GameConstants.eBabies BabyType;
	public float probability = 1.0f;
	public float prizeProbability = 0.1f;

	public bool IsAvailable()
	{
		return PlayerData.Instance.babies[(int)BabyType];
	}
	
	public float GetProbability()
	{
		return probability;
	}
	
	public float GetPrizeProbability()
	{
		return prizeProbability;
	}
}
