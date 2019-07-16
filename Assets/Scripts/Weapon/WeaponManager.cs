using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoSingleton<WeaponManager>
{
	public List<MonoObjectPool> bulletList;

	public Weapon GetWeapon(int bulletIndex)
	{
		return new Weapon(bulletList[bulletIndex]);
	}
}
