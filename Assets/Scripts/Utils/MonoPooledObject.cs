using UnityEngine;
using System.Collections;

public class MonoPooledObject : MonoBehaviour
{
	public MonoObjectPool pool;

	public virtual void ReturnToPool()
	{
		if (pool)
		{
			pool.AddObject(this);
		}
		else
		{
			Debug.LogWarning("PooledObject has not pool.");
			Destroy(gameObject);
		}
	}
}
