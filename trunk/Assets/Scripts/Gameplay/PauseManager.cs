using UnityEngine;
using System.Collections;

public class PauseManager : SingletonMonoBehaviour<PauseManager>
{
	public GameObject PauseScreen;
	public Pushable ResumeButton;
	public Pushable ExitButton;

	void Start()
	{
		transform.position = Vector3.zero;
		Hide();
	}
	
	void Update()
	{
		if(ResumeButton.IsJustPressed())
		{
			Pause(false);
		}
		else if(ExitButton.IsJustPressed())
		{
			Pause(false);
			Gameplay_Normal.Instance.ExitFromPause();
			LevelManager.Instance.ExitFromPause();
			Rainbow.Instance.SetValue(0);
		}
		else
		{
			return;
		}
	}
	
	public void Show()
	{
		gameObject.SetActive(true);
	}
	
	public void Hide()
	{
		gameObject.SetActive(false);
	}

	public void Pause(bool _bValue)
	{
		Time.timeScale = _bValue ? 0 : 1;
		gameObject.SetActive(_bValue);
	}

	public bool IsPaused()
	{
		return gameObject.activeSelf;
	}
}
