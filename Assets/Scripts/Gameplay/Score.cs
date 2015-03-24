using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour 
{
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
	public TextMesh Level_Text;
	public TextMesh Level_Text_shadow;

	public Animator ChainAnimator;
	public Animator CoinsAnimator;
	public Animator DiaperAnimator;
	public Animator TopHUDAnimator;
	public Animator LevelAnimator;

	public Transform ChainTransform;
	public Transform CoinsTransform;

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
		get { return m_chain; }
		set
		{
			m_chain = value;
			if(m_chain > 1 && !chainVisible)
			{
				chainVisible = true;
				ChainAnimator.SetTrigger("In");
			}
			else if(m_chain <= 1 && chainVisible)
			{
				chainVisible = false;
				ChainAnimator.SetTrigger("Out");
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
		ChainAnimator.SetTrigger("Boost");
	}
	
	public void MegaChainBoost()
	{
		MegaChainBoostActive = true;
		ChainAnimator.SetTrigger("MegaBoost");
	}
	
	public void DoubleCoins()
	{
		DoubleCoinsActive = true;
		CoinsAnimator.SetTrigger("DoubleCoins");
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
	}
	
	public void PrizeCollected(int _coins)
	{
		m_coins += _coins * Chain;

		UpdateCoins();
	}
	
	public void Fail()
	{
		Chain = 0;

		UpdateChain();
	}

	void UpdateCoins()
	{
		CoinsAnimator.SetTrigger("IncCoins");
		CoinsText.text = "" + m_coins;
		CoinsText_shadow.text = "" + m_coins;
	}

	void UpdateBabiesFed()
	{
		DiaperAnimator.SetTrigger("BabyFed");
		BabiesFedText.text = "" + m_babiesFed;
		BabiesFedText_shadow.text = "" + m_babiesFed;
	}

	void UpdateChain()
	{
		if(Chain > 2)
		{
			ChainAnimator.SetTrigger("IncChain");
		}
		ChainText.text = "x" + Chain;
		ChainText_shadow.text = "x" + Chain;
	}

	public void SetLevel(int _level)
	{
		Level_Text.text = "Level " + _level;
		Level_Text_shadow.text = "Level " + _level;
	}

	public void AnimateIn()
	{
		TopHUDAnimator.SetTrigger("In");
		LevelAnimator.SetTrigger("In");
	}
	
	public void AnimateOut()
	{
		TopHUDAnimator.SetTrigger("Out");
		LevelAnimator.SetTrigger("Out");
	}

	public Vector3 GetCoinsDest()
	{
		if(Chain > 1)
		{
			return ChainTransform.position;
		}
		else
		{
			return CoinsTransform.position;
		}
	}
}
