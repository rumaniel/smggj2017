using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoSingleton<WeaponManager> 
{
	public MonoObjectPool bulletList;

	public Weapon GetWeapon(Defines.WeaponType weaponType, bool isPlayer = false)
	{
		switch (weaponType)
		{
			// case Defines.WeaponType.Normal:
			// 	bulletList[0]
		}
		// todo
		return null;
	}


}
