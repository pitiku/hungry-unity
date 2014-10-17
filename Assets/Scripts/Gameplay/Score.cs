using UnityEngine;
using System.Collections;

public class Score : AnimatedObject {

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

	int coins = 0;
	int babiesFed = 0;
	int chain = 1;

	bool ChainBoostActive = false;
	bool MegaChainBoostActive = false;
	bool DoubleCoinsActive = false;

	public int GetCoins() { return coins; }
	public int GetBabiesFed() { return babiesFed; }
	public int GetChain() { return chain; }

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

	public void ChainBoost()
	{
		ChainBoostActive = true;
		animator.SetTrigger("ChainBoost");
	}
	
	public void MegaChainBoost()
	{
		MegaChainBoostActive = true;
		animator.SetTrigger("MegaChainBoost");
	}
	
	public void DoubleCoins()
	{
		DoubleCoinsActive = true;
		animator.SetTrigger("DoubleCoins");
	}
	
	public void BabyFed(int _coins)
	{
		coins += _coins * chain * (DoubleCoinsActive ? 2 : 1);
		babiesFed++;
		if(MegaChainBoostActive)
		{
			chain += 3;
		}
		else if(ChainBoostActive)
		{
			chain += 2;
		}
		else
		{
			chain++;
		}

		UpdateCoins();
		UpdateChain();
		UpdateBabiesFed();

		//animator.SetTrigger("BabyFed");
	}
	
	public void PrizeCollected(int coins)
	{
		coins += coins * chain;

		UpdateCoins();

		//animator.SetTrigger("IncCoins");
	}
	
	public void Fail()
	{
		chain = 1;

		UpdateChain();

		//animator.SetTrigger("ResetChain");
	}

	void UpdateCoins()
	{
		CoinsText.text = "" + coins;
		CoinsText_shadow.text = "" + coins;
	}

	void UpdateBabiesFed()
	{
		BabiesFedText.text = "" + babiesFed;
		BabiesFedText_shadow.text = "" + babiesFed;
	}

	void UpdateChain()
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
