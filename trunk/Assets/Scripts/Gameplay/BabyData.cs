using UnityEngine;
using System.Collections;

public class BabyData : MonoBehaviour 
{
	public GameConstants.eBabies BabyType;
	public float probability = 1.0f;

	public bool IsAvailable()
	{
		return PlayerData.Instance.babies[(int)BabyType];
	}

	public float GetProbability()
	{
		return probability;
	}
}
