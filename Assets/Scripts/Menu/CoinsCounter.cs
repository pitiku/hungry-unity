using UnityEngine;
using System.Collections;

public class CoinsCounter : SingletonMonoBehaviour<CoinsCounter> {

	public TextMesh text;
	public TextMesh text_shadow;
	Animator animator;

	void Awake()
	{
		animator = GetComponent<Animator>();
	}

	void Start () 
	{
		UpdateCoins();
	}
	
	void Update () 
	{
		if(GetComponent<MenuItem>().IsJustPressed())
		{
			//Open coins popup
		}
	}

	public void UpdateCoins()
	{
		text.text = "" + PlayerData.Instance.Coins;
		text_shadow.text = "" + PlayerData.Instance.Coins;
	}

	public void AnimateIn()
	{
		animator.SetTrigger("In");
	}

	public void AnimateOut()
	{
		animator.SetTrigger("Out");
	}
}
