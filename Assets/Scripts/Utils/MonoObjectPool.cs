using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonoObjectPool : MonoBehaviour
{
	public GameObject prefab;
	public int poolSize;

	[HideInInspector]
	public List<MonoPooledObject> availiableObjects = new List<MonoPooledObject>();

	private void Awake()
	{
		CreatePool();
	}

	[ContextMenu ("CreatePool")]
	public virtual void CreatePool()
	{
		if (prefab == null)
			return;
			
		while (availiableObjects.Count < poolSize)
		{
			AddObject( CreateObject() );
		}
	}

	[ContextMenu ("ClearPool")]
	public virtual void ClearPool()
	{
		for (int i = 0; i < availiableObjects.Count; ++i)
		{
			GameObject.DestroyImmediate(availiableObjects[i].gameObject);
		}
		availiableObjects.Clear();
	}

	public virtual MonoPooledObject GetObject(bool isActive = true)
	{
		MonoPooledObject po;
		int lastAvailiableIndex = availiableObjects.Count - 1;
		if (lastAvailiableIndex >= 0)
		{
			po = availiableObjects[lastAvailiableIndex];
			availiableObjects.RemoveAt(lastAvailiableIndex);
		}
		else 
		{
			po = CreateObject();
		}

		if(isActive)
		{
			po.gameObject.SetActive(isActive);
		}
		return po;
	}

	public virtual void AddObject(MonoPooledObject po)
	{
		po.transform.SetParent(transform, false);
		po.gameObject.SetActive(false);
		availiableObjects.Add(po);
	}

	protected MonoPooledObject CreateObject()
	{
		var go = Instantiate(prefab) as GameObject;
		go.name = prefab.name;
		var po = go.GetComponent<MonoPooledObject>();
		po.pool = this;
		return po;
	}
}
