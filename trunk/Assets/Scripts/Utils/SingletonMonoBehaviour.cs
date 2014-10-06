using UnityEngine;
using System.Collections;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
	private static T s_Instance;
	
	// -------------------------------------------------------------------------------
	// -------------------------------------------------------------------------------
	public static T Instance
	{
		get
		{
			if (!s_Instance)
			{
				s_Instance = (T)GameObject.FindObjectOfType(typeof(T));
			}
			
			return s_Instance;
		}
	}
}
