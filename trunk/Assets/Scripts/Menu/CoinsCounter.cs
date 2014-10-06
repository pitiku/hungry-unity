using UnityEngine;
using System.Collections;

public class CoinsCounter : SingletonMonoBehaviour<CoinsCounter> {

	public TextMesh text;

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
	}
}
