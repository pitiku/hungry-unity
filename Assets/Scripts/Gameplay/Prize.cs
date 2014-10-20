using UnityEngine;
using System.Collections;

public class Prize : MonoBehaviour 
{
	public GameConstants.eBabies foodType;
	public int coins = 1;

	public float speed = 0.1f;
	Vector3 src;
	Vector3 dest = new Vector3(0.0f, -0.5f, -3.0f);

	Animator animator;
	MenuItem menuItem;
	Living living;

	enum eState
	{
		OUT,
		DROPPED,
		IDLE,
		VACUUMED,
		COLLECTED
	};

	eState state;
	float stateTimeStart;

	void Awake()
	{
		menuItem = GetComponent<MenuItem>();
		living = GetComponent<Living>();
		enabled = false;
		menuItem.enabled = false;
		if(living)
		{
			living.enabled = false;
		}

		SetState(eState.OUT);
	}

	void Update () 
	{
		switch(state)
		{
		case eState.OUT:
			break;

		case eState.DROPPED:
		{
			float fPerc = GetStateTime() / 0.3f;

			transform.position = src + (dest - src) * fPerc;
			float scale = (0.6f - Mathf.Sin(fPerc * Mathf.PI) * 0.4f);
			transform.localScale = Vector3.one * scale;

			if(fPerc >= 1.0f)
			{
				if(living)
				{
					living.Reset();
					living.enabled = true;
				}
				SetState(eState.IDLE);
				menuItem.enabled = true;
			}

			break;
		}
		case eState.IDLE:
			if(menuItem.IsJustPressed())
			{
				Collected();
			}
			break;
		
		case eState.VACUUMED:
			break;
		
		case eState.COLLECTED:
		{
			float fPerc = GetStateTime() / 0.3f;
			
			transform.position = src + (dest - src) * fPerc;
			transform.localScale = Vector3.one * (1.0f - fPerc);
			
			if(fPerc >= 1.0f)
			{
				Score.Instance.PrizeCollected(coins);
				ToPool();
			}
			break;
		}
		}
	}
	
	public void Dropped(Vector3 _pos)
	{
		SetState(eState.DROPPED);

		transform.parent = null;
		transform.position = _pos;
		transform.localScale = 0.5f * Vector3.one;
		src = transform.position;

		enabled = true;

		Vacuum.Instance.AddPrize(this);
	}
	
	public void Collected()
	{
		SetState(eState.COLLECTED);

		src = transform.position;
		dest = Score.Instance.GetCoinsDest();

		menuItem.enabled = false;

		Vacuum.Instance.RemovePrize(this);
	}
	
	public void Vacuummed()
	{
		SetState(eState.VACUUMED);
		menuItem.enabled = false;
	}

	public void ToPool()
	{
		SetState(eState.OUT);

		BabiesPool.Instance.AddObject(transform);
		transform.localPosition = Vector3.zero;
	}

	void SetState(eState _state)
	{
		gameObject.SendMessage("Exit_" + state.ToString(), SendMessageOptions.DontRequireReceiver);
		state = _state;
		stateTimeStart = Time.time;
		gameObject.SendMessage("Enter_" + state.ToString(), SendMessageOptions.DontRequireReceiver);
	}

	float GetStateTime()
	{
		return Time.time - stateTimeStart;
	}
}
