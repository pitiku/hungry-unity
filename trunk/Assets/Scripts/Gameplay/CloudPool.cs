using UnityEngine;
using System.Collections;

public class CloudPool : MonoBehaviour 
{
	#region Singleton
	private static CloudPool _Instance;
	
	public static CloudPool Instance
	{
		get
		{
			if(!_Instance)
			{
				_Instance = FindObjectOfType<CloudPool>();
			}
			return _Instance;
		}
	}
	#endregion
	
	public CloudForBaby[] clouds;

	void Awake()
	{
		clouds = GetComponentsInChildren<CloudForBaby>();
	}
	
	public CloudForBaby GetCloud()
	{
		foreach(CloudForBaby cloud in clouds)
		{
			if(cloud.transform.parent == transform)
			{
				return cloud;
			}
		}
		return null;
	}
	
	public void AddObject(Transform _transform)
	{
		_transform.parent = transform;
		_transform.localPosition = Vector3.zero;
	}	
}
