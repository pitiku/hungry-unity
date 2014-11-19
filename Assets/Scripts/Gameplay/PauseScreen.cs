using UnityEngine;
using System.Collections;

public class PauseScreen : SingletonMonoBehaviour<ResultsScreen>
{
	void Start()
	{
		transform.position = Vector3.zero;
		Hide();
	}
	
	void Update()
	{
		
	}
	
	public void Show()
	{
		gameObject.SetActive(true);
	}
	
	public void Hide()
	{
		gameObject.SetActive(false);
	}
}
