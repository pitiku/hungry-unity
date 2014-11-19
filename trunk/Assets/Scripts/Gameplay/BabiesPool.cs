using UnityEngine;
using System.Collections;

public class BabiesPool : MonoBehaviour 
{
	#region Singleton
	private static BabiesPool _Instance;
	
	public static BabiesPool Instance
	{
		get
		{
			if(!_Instance)
			{
				_Instance = FindObjectOfType<BabiesPool>();
			}
			return _Instance;
		}
	}
	#endregion

	public Baby[] babies;
	public Food[] food;
	public Prize[] prizes;

	void Awake()
	{
		babies = GetComponentsInChildren<Baby>();
		food = GetComponentsInChildren<Food>();
		prizes = GetComponentsInChildren<Prize>();
	}

	public Baby GetBaby(GameConstants.eBabies _babyType)
	{
		foreach(Baby baby in babies)
		{
			if(baby.baby == _babyType && baby.transform.parent == transform)
			{
				return baby;
			}
		}
		return null;
	}
	
	public Food GetFood(GameConstants.eBabies _babyType)
	{
		foreach(Food f in food)
		{
			if(f.foodType == _babyType && f.transform.parent == transform)
			{
				return f;
			}
		}
		return null;
	}
	
	public Prize GetPrize(GameConstants.eBabies _babyType)
	{
		foreach(Prize prize in prizes)
		{
			if(prize.foodType == _babyType && prize.transform.parent == transform)
			{
				return prize;
			}
		}
		return null;
	}
	
	public void ReturnToPool(Transform _transform)
	{
		_transform.parent = transform;
		_transform.localPosition = Vector3.zero;
	}	
}
