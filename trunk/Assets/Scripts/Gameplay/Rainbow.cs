using UnityEngine;
using System.Collections;

public class Rainbow : MonoBehaviour {

	private static Rainbow _Instance;
	
	public static Rainbow Instance
	{
		get
		{
			if(!_Instance)
			{
				_Instance = FindObjectOfType<Rainbow>();
			}
			return _Instance;
		}
	}

	void Start () 
	{
	
	}
	
	void Update () 
	{
	
	}
}
