using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Weapon
{
    MonoObjectPool bullet;

    public Weapon(MonoObjectPool bullet)
    {
        this.bullet = bullet;
    }

    public void SetUpWeapon(List<Transform> weaponPositionList)
    {

    }

    public float GetFireRate()
    {
        return 0f;
    }

    public void Fire()
    {

    }
}

