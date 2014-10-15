using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {

	#region Singleton
	private static Score _Instance;

	public static Score Instance
	{
		get
		{
			if(!_Instance)
			{
				_Instance = FindObjectOfType<Score>();
			}
			return _Instance;
		}
	}
	#endregion

	public TextMesh BabiesFedText;
	public TextMesh BabiesFedText_shadow;
	public TextMesh CoinsText;
	public TextMesh CoinsText_shadow;
	public TextMesh ChainText;
	public TextMesh ChainText_shadow;

	Animator animator;

	int coins = 0;
	int babiesFed = 0;
	int chain = 1;

	public int GetCoins() { return coins; }
	public int GetBabiesFed() { return babiesFed; }
	public int GetChain() { return coins; }

	void Awake()
	{
		animator = GetComponent<Animator>();
	}

	void Start () 
	{
		BabiesFedText.text = "0";
		BabiesFedText_shadow.text = "0";
		CoinsText.text = "0";
		CoinsText_shadow.text = "0";
		ChainText.text = "x1";
		ChainText_shadow.text = "x1";
	}
	
	void Update () 
	{
	
	}

	void BabyFed(int coins)
	{
		coins += coins * chain;
		babiesFed++;
		chain++;
		
		animator.SetTrigger("BabyFed");
	}
	
	void PrizeCollected(int coins)
	{
		coins += coins * chain;

		animator.SetTrigger("IncCoins");
	}
	
	void Fail()
	{
		animator.SetTrigger("ResetChain");
		chain = 1;
	}

	public void UpdateCoins()
	{
		CoinsText.text = "" + coins;
		CoinsText_shadow.text = "" + coins;
	}

	public void UpdateBabiesFed()
	{
		BabiesFedText.text = "" + babiesFed;
		BabiesFedText_shadow.text = "" + babiesFed;
	}

	public void UpdateChain()
	{
		ChainText.text = "x" + chain;
		ChainText_shadow.text = "x" + chain;
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
