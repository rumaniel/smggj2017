using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="ShipInfo", menuName="ScriptableObject/ShipInfo")]
public class ShipInfo : ScriptableObject
{
    public Sprite sprite;
    public float moveSpeed;
    public float rotateSpeed = 5f;
    public WeaponInfo weaponInfo;
    public int bulletIndex;
    public float health;

    public List<Sprite> portraitList;
    public Defines.FollwerType follwerType;
    public float follwerBuf;
    public string Name;
    public List<string> quoteList;
}
