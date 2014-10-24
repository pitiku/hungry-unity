using UnityEngine;
using System.Collections;

public class AudioManager : SingletonMonoBehaviour<AudioManager> 
{
	public AudioSource eat;
	public AudioSource success;
	public AudioSource fail;
	public AudioSource prize;
	public AudioSource[] launch;

	public void PlaySuccess()
	{
		success.Play();
	}
	
	public void PlayFail()
	{
		fail.Play();
	}
	
	public void PlayPrize()
	{
		prize.Play();
	}
	
	public void PlayEat()
	{
		eat.Play();
	}
	
	public void PlayLaunch()
	{
		launch[Random.Range(0, launch.Length - 1)].Play();
	}
}
