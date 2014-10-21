using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Vacuum : MonoBehaviour 
{
	#region Singleton
	private static Vacuum _Instance;
	
	public static Vacuum Instance
	{
		get
		{
			if(!_Instance)
			{
				_Instance = FindObjectOfType<Vacuum>();
			}
			return _Instance;
		}
	}
	#endregion

	public Transform HiddenPosition;
	public Transform VacuumingPosition;
	public Transform Hole;

	float speed = 1.0f;

	List<Prize> prizes = new List<Prize>();

	enum eState
	{
		HIDDEN,
		COMING_OUT,
		VACUUMING,
		GOING_IN
	};

	eState state;

	void Start () 
	{
		state = eState.HIDDEN;
		transform.position = HiddenPosition.transform.position;
	}
	
	void Update () 
	{
		switch(state)
		{
		case eState.HIDDEN:
			if(prizes.Count > 0)
			{
				state = eState.COMING_OUT;
			}
			break;

		case eState.COMING_OUT:
			if(prizes.Count <= 0)
			{
				state = eState.GOING_IN;
			}

			transform.position = Vector3.MoveTowards(transform.position, VacuumingPosition.transform.position, speed * Time.deltaTime);
			if((transform.position - VacuumingPosition.transform.position).magnitude < 0.01f)
			{
				state = eState.VACUUMING;
			}

			break;

		case eState.VACUUMING:
			transform.position = VacuumingPosition.position + 0.01f * Vector3.up * Mathf.Sin(Time.time * 40);

			foreach(Prize p in prizes)
			{
				p.Vacuummed(Hole.position);
			}
			prizes.Clear();

			if(prizes.Count <= 0)
			{
				state = eState.GOING_IN;
			}
			break;

		case eState.GOING_IN:
			if(prizes.Count > 0)
			{
				state = eState.COMING_OUT;
			}

			transform.position = Vector3.MoveTowards(transform.position, HiddenPosition.transform.position, speed * Time.deltaTime);
			if((transform.position - HiddenPosition.transform.position).magnitude < 0.01f)
			{
				state = eState.HIDDEN;
			}

			break;
		}
	}

	public void AddPrize(Prize _prize)
	{
		prizes.Add(_prize);
	}

	public void RemovePrize(Prize _prize)
	{
		prizes.Remove(_prize);
	}
}
