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

	protected override void OnTriggerEnter2D(Collider2D col)
	{
		// if ((col.tag == "EnemyBullet"))
		// {
		// 	BulletBase bullet = col.transform.GetComponent<BulletBase>();
		// 	if (bullet != null)
		// 	{
		// 		PlayerData.Instance.AddHealth(-bullet.weaponInfo.damage);
		// 	}
		// }
	}	

}