using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoSingleton<WeaponManager> 
{
	public List<MonoObjectPool> bulletList; 

	public Weapon GetWeapon(Defines.WeaponType weaponType, bool isPlayer = false)
	{
		Weapon weapon = new Weapon(bulletList[(int)weaponType]);

		switch (weaponType)
		{
			case Defines.WeaponType.Normal:
				// todo?
				break;
			default:
				break;
				
		}
		return weapon;
	}


}
