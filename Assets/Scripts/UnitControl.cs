using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UnitControl : PlayerBaseControl 
{
    public bool isEnemy;
    public List<ShipInfo> shipInfoList;
    
	float timeToFire = 0f;

	public override void Init()
	{
		base.Init();
	}

}