﻿using UnityEngine;
using System.Collections;

public class BabyInShop : MonoBehaviour 
{
	public Baby baby;
	public SpriteRenderer halo;
	public TextMesh priceText;
	public TextMesh priceText_shadow;

	public void ShowPrice(int _price)
	{
		priceText.gameObject.SetActive(true);
		priceText.text = "" + _price;
		priceText_shadow.text = "" + _price;
	}

	public void SetShadowed(bool _bValue)
	{

	}

	public void SetSelected(bool _bValue)
	{
		halo.gameObject.SetActive(_bValue);
	}
}
