using UnityEngine;
using System.Collections;

public class BabyInShop : MonoBehaviour 
{
	Baby baby;
	MenuItem menuItem;

	public SpriteRenderer halo;
	public GameObject priceVisual;
	public int Price;
	public MeshRenderer message;

	void Awake()
	{
		baby = GetComponentInChildren<Baby>();
		menuItem = GetComponentInChildren<MenuItem>();
		SetSelected(false);
	}

	public Baby GetBaby()
	{
		return baby;
	}
	
	public MenuItem GetMenuItem()
	{
		return menuItem;
	}
	
	public void ShowPrice(bool _bValue)
	{
		priceVisual.SetActive(_bValue);
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
