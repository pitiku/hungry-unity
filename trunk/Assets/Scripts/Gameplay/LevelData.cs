using UnityEngine;
using System.Collections;

public class LevelData : MonoBehaviour 
{
	public int level = 0;
	public int numBabies = 2;
	public bool allowDiscard = false;
	public bool allowRepeat = false;

	public int GetLevel()
	{
		return level;
	}
}
