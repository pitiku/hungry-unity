using UnityEngine;
using System.Collections;

public class ResultsScreen : SingletonMonoBehaviour<ResultsScreen>
{
	void Start()
	{
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
