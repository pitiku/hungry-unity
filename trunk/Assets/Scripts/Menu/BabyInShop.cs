using UnityEngine;
using System.Collections;

public class BabyInShop : MonoBehaviour 
{
	public Baby baby;
	public SpriteRenderer halo;
	public TextMesh priceText;
	public TextMesh priceText_shadow;
	public MenuItem menuItem;
	public int Price;

	public void ShowPrice(int _price)
	{
		priceText.gameObject.SetActive(true);
		priceText.text = "" + _price;
		priceText_shadow.text = "" + _price;
	}

	public void SetShadowed(bool _bValue)
	{
		baby.GetComponent<SpriteRenderer>().color = _bValue ? Color.black : Color.white;
	}

	public void SetSelected(bool _bValue)
	{
		halo.gameObject.SetActive(_bValue);
	}
}
