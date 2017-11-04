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

    public override string ToString()
    {
        return Name + "\n" + follwerType.ToString() + " : " + follwerBuf + "\n\n" + quoteList[0] + "\n";
    }

    public void Init( Sprite sprite, float moveSpeed, float rotateSpeed,
 WeaponInfo weaponInfo, int bulletIndex, float health, List<Sprite> portraitList, Defines.FollwerType follwerType, float follwerBuf, string Name, List<string> quoteList)
    {
        this.sprite = sprite;
        this.moveSpeed = moveSpeed;
        this.rotateSpeed = rotateSpeed;
        this.weaponInfo = weaponInfo;
        this.bulletIndex = bulletIndex;
        this.health = health;
        this.portraitList = portraitList;
        this.follwerType = follwerType;
        this.follwerBuf = follwerBuf;
        this.Name =  Name;
        this.quoteList = quoteList;
    }
}
