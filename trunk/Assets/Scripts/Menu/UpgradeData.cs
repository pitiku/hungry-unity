using UnityEngine;
using System.Collections;

public class UpgradeData : MonoBehaviour {

	public GameConstants.eUpgrades upgrade;
	public int price;

	public MenuItem item;
	public SpriteRenderer tick;
	public TextMesh priceText;
	public TextMesh priceText_shadow;
	public SpriteRenderer halo;
	public MeshRenderer textMesh;

	public UpgradeData prerequisite = null;

	public void UpdateVisual()
	{
		tick.enabled = IsBought();
		if(IsBought())
		{
			priceText.gameObject.SetActive(false);
		}
		else
		{
			priceText.text = "" + price;
			priceText_shadow.text = "" + price;
		}
		halo.enabled = false;

		if(!prerequisite || prerequisite.IsBought())
		{
			GetComponent<SpriteRenderer>().color = Color.white;
		}
		else
		{
			Color gray = Color.gray;
			gray.a = 0.5f;
			GetComponent<SpriteRenderer>().color = gray;
		}
	}

	public void Select()
	{
		halo.enabled = true;
		textMesh.enabled = true;
	}

	public void UnSelect()
	{
		halo.enabled = false;
		textMesh.enabled = false;
	}

	public bool CanBuy()
	{
		return !IsBought() && (!prerequisite || prerequisite.IsBought());
	}

	public bool IsBought()
	{
		switch(upgrade)
		{
		case GameConstants.eUpgrades.ACCELEROMETER:
			return PlayerData.Instance.upgrade_accelerometer;
		case GameConstants.eUpgrades.CROWN:
			return PlayerData.Instance.upgrade_crown;
		case GameConstants.eUpgrades.SUPER_RAINBOW:
			return PlayerData.Instance.upgrade_rainbowplus;
		case GameConstants.eUpgrades.VACUUM:
			return PlayerData.Instance.upgrade_vacuum;
		case GameConstants.eUpgrades.GLOVES:
			return PlayerData.Instance.upgrade_gloves;
		case GameConstants.eUpgrades.MEGA_RAINBOW:
			return PlayerData.Instance.upgrade_rainbowplusplus;
		}
		return false;
	}

	public void Buy()
	{
		switch(upgrade)
		{
		case GameConstants.eUpgrades.ACCELEROMETER:
			PlayerData.Instance.upgrade_accelerometer = true;
			break;
		case GameConstants.eUpgrades.CROWN:
			PlayerData.Instance.upgrade_crown = true;
			break;
		case GameConstants.eUpgrades.SUPER_RAINBOW:
			PlayerData.Instance.upgrade_rainbowplus = true;
			break;
		case GameConstants.eUpgrades.VACUUM:
			PlayerData.Instance.upgrade_vacuum = true;
			break;
		case GameConstants.eUpgrades.GLOVES:
			PlayerData.Instance.upgrade_gloves = true;
			break;
		case GameConstants.eUpgrades.MEGA_RAINBOW:
			PlayerData.Instance.upgrade_rainbowplusplus = true;
			break;
		}

		tick.enabled = true;
		priceText.gameObject.SetActive(false);
	}
}
