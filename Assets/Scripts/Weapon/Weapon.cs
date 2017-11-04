using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Weapon
{
    MonoObjectPool bulletPool;
    
    public List<Transform> firePositionList = new List<Transform>();
    public WeaponInfo weaponInfo;

    public Weapon(MonoObjectPool bulletPool, bool isPlayer = true)
    {
        this.bulletPool = bulletPool;

        MonoPooledObject bullet = bulletPool.GetObject(false);
        this.weaponInfo = bullet.GetComponent<BulletBase>().weaponInfo;
    }

    public virtual void SetUpWeapon(List<Transform> weaponPositionList)
    {
        for (int i = 0; i < weaponPositionList.Count; ++i)
        {
            this.firePositionList.Add(weaponPositionList[i]);
        }
    }

    public virtual float GetFireRate()
    {
        return weaponInfo.fireRate;
    }

    public virtual void Fire()
    {
        MonoPooledObject bullet = bulletPool.GetObject();
        bullet.transform.position = firePositionList[0].position;
    }
}

