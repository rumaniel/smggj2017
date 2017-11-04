using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UnitControl : PlayerBaseControl 
{
    public bool isEnemy;
    public List<ShipInfo> shipInfoList;
	public List<Sprite> spriteList;
    public List<WeaponInfo> weaponList;
    
	float timeToFire = 0f;

	public override void Init()
	{
		base.Init();
	}

}