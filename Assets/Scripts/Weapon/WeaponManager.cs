using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoSingleton<WeaponManager> 
{
	public MonoObjectPool bulletList;

	public IWeapon GetWeapon(Defines.WeaponType weaponType, bool isPlayer = false)
	{
		// todo
		return null;
	}



}
