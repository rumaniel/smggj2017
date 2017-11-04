using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoSingleton<UnitManager> 
{
	public MonoObjectPool unitPool; 

	public MonoPooledObject GetUnit()
	{
		return unitPool.GetObject();
	}
}
