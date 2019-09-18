using UnityEngine;

public class ScriptableObjectSingleton<T> : ScriptableObject where T : ScriptableObject
{
	private static T instance;
	private static object mutex = new object();

	public static T Instance
	{
		get
		{
			lock(mutex)
			{
				if (instance.IsNull())
				{
					instance = Resources.Load(typeof(T).Name) as T;
				}

				return instance;
			}
		}
	}
}