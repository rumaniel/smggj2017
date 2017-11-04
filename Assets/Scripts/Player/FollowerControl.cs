using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class FollowerControl : PlayerBaseControl 
{
	public SpriteRenderer sprite;
	public Transform spriteBase;

	float timeToFire = 0f;

    public void UpdateFollower()
    {

    }

	public override void Init()
	{
		base.Init();
	}

}