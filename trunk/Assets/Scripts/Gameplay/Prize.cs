using UnityEngine;
using System.Collections;

public class Prize : MonoBehaviour 
{
	public GameConstants.eBabies foodType;
	public int coins = 1;

	public float speed = 0.1f;
	Vector3 dest = Vector3.zero;

	Animator animator;
	MenuItem menuItem;

	void Awake()
	{
		animator = GetComponent<Animator>();
		menuItem = GetComponent<MenuItem>();
		enabled = false;
		menuItem.enabled = false;
	}

	void Update () 
	{
		//Move
		transform.position = Vector3.MoveTowards(transform.position, dest, speed * Time.deltaTime);

		//Check collected
		if(menuItem.IsJustPressed())
		{
			Collected();
		}
	}
	
	public void Dropped(Vector3 _pos)
	{
		transform.position = _pos;

		enabled = true;
		menuItem.enabled = true;
		if(animator)
		{
			animator.SetTrigger("Dropped");
		}

		Vacuum.Instance.AddPrize(this);
	}
	
	public void Collected()
	{
		enabled = false;
		menuItem.enabled = false;
		if(animator)
		{
			animator.SetTrigger("Collected");
		}
		
		Score.Instance.PrizeCollected(coins);
		
		Vacuum.Instance.RemovePrize(this);

		ToPool();
	}
	
	public void Vacuummed()
	{
		enabled = false;
		menuItem.enabled = false;
	}

	public void ToPool()
	{
		BabiesPool.Instance.AddObject(transform);
		transform.localPosition = Vector3.zero;
	}
}
