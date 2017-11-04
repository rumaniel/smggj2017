using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="WeaponInfo", menuName="ScriptableObject/WeaponInfo")]
public class WeaponInfo : ScriptableObject
{
    public float fireRate;
    public float damage;
    public float shotSpeed;
    public List<bool> firePositionList;
}
