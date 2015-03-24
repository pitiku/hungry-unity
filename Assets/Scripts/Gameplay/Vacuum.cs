using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Vacuum : SingletonMonoBehaviour<Vacuum> 
{
	public Transform HiddenPosition;
	public Transform VacuumingPosition;
	public Transform Hole;

	public float MoveSpeed = 5.0f;

	List<Prize> prizes = new List<Prize>();

	enum eState
	{
		HIDDEN,
		COMING_OUT,
		VACUUMING,
		GOING_IN
	};

	eState state;
	float m_fStateEntrTime;

	void Start () 
	{
		state = eState.HIDDEN;
		transform.position = HiddenPosition.transform.position;

		gameObject.SetActive(PlayerData.Instance.upgrade_vacuum);
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

			transform.position = Vector3.MoveTowards(transform.position, VacuumingPosition.transform.position, MoveSpeed * Time.deltaTime);
			if((transform.position - VacuumingPosition.transform.position).magnitude < 0.01f)
			{
				state = eState.VACUUMING;
			}

			break;

		case eState.VACUUMING:
			transform.position = VacuumingPosition.position + 0.01f * Vector3.up * Mathf.Sin(Time.time * 40);

			List<Prize> prizesToRemove = new List<Prize>();
			foreach(Prize p in prizes)
			{
				if(p.IsIdle())
				{
					p.Vacuummed(Hole.position);
				}

				if(p.IsOut())
				{
					prizesToRemove.Add(p);
				}
			}

			foreach(Prize p in prizesToRemove)
			{
				prizes.Remove(p);
			}
			prizesToRemove.Clear();

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

			transform.position = Vector3.MoveTowards(transform.position, HiddenPosition.transform.position, MoveSpeed * Time.deltaTime);
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
