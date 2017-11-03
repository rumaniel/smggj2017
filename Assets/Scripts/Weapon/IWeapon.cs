using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public interface IWeapon
{
    void SetUpWeapon(List<Transform> weaponPositionList);
    float GetFireRate();
    void Fire();
}

