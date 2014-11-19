using UnityEngine;
using System.Collections;

public class PowerUp_Level : MonoBehaviour 
{
	public Pushable menuItem;
	public SpriteRenderer sprite;
	public SpriteRenderer textBG;
	public TextMesh text;
	Color DisabledColor = new Color(0.7f, 0.7f, 0.7f, 0.7f);
	Color DisabledTextColor = new Color(0.0f, 0.0f, 0.0f, 0.7f);

	public void SetEnabled(bool _value)
	{
		menuItem.enabled = _value;
		sprite.color = _value ? Color.white : DisabledColor;
		textBG.color = _value ? Color.white : DisabledColor;
		text.color = _value ? Color.black : DisabledTextColor;
	}

	public void SetCount(int count)
	{
		text.text = "" + count;
	}
}
