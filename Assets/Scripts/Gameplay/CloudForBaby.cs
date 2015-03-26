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
}
