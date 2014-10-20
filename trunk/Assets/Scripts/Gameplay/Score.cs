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

	int m_coins = 0;
	int m_babiesFed = 0;
	int m_chain = 0;
	bool chainVisible = false;

	bool ChainBoostActive = false;
	bool MegaChainBoostActive = false;
	bool DoubleCoinsActive = false;

	public int GetCoins() { return m_coins; }
	public int GetBabiesFed() { return m_babiesFed; }

	public int Chain
	{ 
		get
		{
			return m_chain;
		}
		set
		{
			m_chain = value;
			if(m_chain > 1 && !chainVisible)
			{
				chainVisible = true;
				animator.SetTrigger("Chain_In");
			}
			else if(m_chain <= 1 && chainVisible)
			{
				chainVisible = false;
				animator.SetTrigger("Chain_Out");
			}
		}
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
		if(Chain > 1)
		{
			m_coins += _coins * Chain * (DoubleCoinsActive ? 2 : 1);
		}
		else
		{
			m_coins += _coins * (DoubleCoinsActive ? 2 : 1);
		}
		m_babiesFed++;
		if(MegaChainBoostActive)
		{
			Chain += 3;
		}
		else if(ChainBoostActive)
		{
			Chain += 2;
		}
		else
		{
			Chain++;
		}

		UpdateCoins();
		UpdateChain();
		UpdateBabiesFed();

		//animator.SetTrigger("BabyFed");
	}
	
	public void PrizeCollected(int _coins)
	{
		m_coins += _coins * Chain;

		UpdateCoins();

		//animator.SetTrigger("IncCoins");
	}
	
	public void Fail()
	{
		Chain = 0;

		UpdateChain();

		//animator.SetTrigger("ResetChain");
	}

	void UpdateCoins()
	{
		CoinsText.text = "" + m_coins;
		CoinsText_shadow.text = "" + m_coins;
	}

	void UpdateBabiesFed()
	{
		BabiesFedText.text = "" + m_babiesFed;
		BabiesFedText_shadow.text = "" + m_babiesFed;
	}

	void UpdateChain()
	{
		ChainText.text = "x" + Chain;
		ChainText_shadow.text = "x" + Chain;
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
