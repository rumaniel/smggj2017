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
    }

    public virtual void SetUpWeapon(WeaponInfo weaponInfo, List<Transform> weaponPositionList)
    {
        this.weaponInfo = weaponInfo;

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
        for (int i = 0; i < weaponInfo.firePositionList.Count; ++i)
        {
            if (weaponInfo.firePositionList[i])
            {
                MonoPooledObject bullet = bulletPool.GetObject();
                bullet.transform.parent = GameManager.Instance.unitRootObject;
                bullet.GetComponent<BulletBase>().Initialize(this.weaponInfo);
                bullet.transform.position = firePositionList[i].position;
                bullet.transform.rotation = firePositionList[i].rotation;
            }
        }
    }
}

