using UnityEngine;
using System.Collections;

public class ScriptableObjectSingleton<T> : ScriptableObject where T : ScriptableObject
{
	private static T _instance;
	private static object _mutex = new object();

	public static T Instance
	{
		get
		{
			lock(_mutex)
			{
				if (_instance == null)
				{
					_instance = Resources.Load(typeof(T).Name) as T;
				}

				return _instance;
			}
		}
	}
}