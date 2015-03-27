using UnityEngine;
using System.Collections;

public class CloudForBaby : MonoBehaviour 
{
	public Transform babyLink;

	private bool m_bMoving = false;
	private float m_duration;
	private Vector3 m_dest;

	private float m_timeStart;
	private Vector3 m_initPos;

	private Baby m_linkedBaby = null;
	private bool m_success;

	private bool m_bBabyAte = false;

	void Update()
	{
		if(m_bMoving)
		{
			float fPerc = Mathf.Min (1.0f, (Time.time - m_timeStart) / m_duration);
			transform.position = Vector3.Lerp(m_initPos, m_dest, fPerc);
			if(fPerc >= 1.0f)
			{
				m_bMoving = false;
			}
		}

		if(m_linkedBaby != null)
		{
			if(!m_bBabyAte)
			{
				if(!m_linkedBaby.IsEating()) //Baby just ate
				{
					m_bBabyAte = true;
					MoveTo(Gameplay_Normal.Instance.GetPos_EatOut(), 0.2f);

					if(m_success)
					{
						//Drop prize
						float fRand = Random.Range(0.0f, 1.0f);
						float fProb = Gameplay_Normal.Instance.GetBabyData(m_linkedBaby.baby).GetPrizeProbability() * (Gameplay_Normal.Instance.IsPrizeSeasonActive() ? 2.0f : 1.0f);
						if(fRand <= fProb)
						{
							if(BabiesPool.Instance.GetPrize(m_linkedBaby.baby))
							{
								BabiesPool.Instance.GetPrize(m_linkedBaby.baby).Dropped(m_linkedBaby.transform.position);
							}
						}
						
					}
				}
			}
			else
			{
				if(!IsMoving()) //Going Out
				{
					transform.localScale = Vector3.one;
					BabiesPool.Instance.ReturnToPool(m_linkedBaby.transform);
					CloudPool.Instance.AddObject(transform);
					m_linkedBaby = null;
				}
			}
		}
	}

	public void SetPos(Vector3 _pos)
	{
		transform.position = _pos;
	}
	
	public void SetPos(Transform _pos)
	{
		transform.position = _pos.position;
	}
	
	public void MoveTo(Transform _dest, float _duration)
	{
		MoveTo(_dest.position, _duration);
	}
	
	public void MoveTo(Vector3 _dest, float _duration)
	{
		m_dest = _dest;
		m_duration = _duration;

		m_timeStart = Time.time;
		m_initPos = transform.position;

		m_bMoving = true;
	}

	public bool IsMoving()
	{
		return m_bMoving;
	}

	public void LinkBaby(Baby _baby, bool _bSuccess)
	{
		m_linkedBaby = _baby;
		m_success = _bSuccess;
		m_bBabyAte = false;
	}

	public Baby GetLinkedBaby()
	{
		return m_linkedBaby;
	}

	private void UnlinkBaby()
	{

	}
}
