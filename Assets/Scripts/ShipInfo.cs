using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="ShipInfo", menuName="ScriptableObject/ShipInfo")]
public class ShipInfo : ScriptableObject
{
    public float moveSpeed;
    public WeaponInfo weaponInfo;
    public int bulletIndex;
}
