using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class FollowerControl : PlayerBaseControl 
{
	public SpriteRenderer sprite;
	public Transform spriteBase;

	float timeToFire = 0f;

    public void UpdateFollower(ShipInfo ship)
    {
		this.shipInfo = ship;
		sprite.sprite = shipInfo.sprite;
		unitData.SetHealth(this.shipInfo.health);
		Init();
    }

	public override void Init()
	{
		base.Init();
	}

	protected override void OnTriggerEnter2D(Collider2D col)
	{
		if ((col.tag == "EnemyBullet"))
		{
			BulletBase bullet = col.transform.GetComponent<BulletBase>();
			if (bullet != null)
			{
				unitData.AddHealth(-bullet.weaponInfo.damage);
				if (unitData.IsDie())
				{
					StartCoroutine("DieFollower");
				}
				// PlayerData.Instance.AddHealth(-bullet.weaponInfo.damage);
			}
		}
	}	

	IEnumerator DieFollower()
	{
        explosionObject.SetActive(true);
		yield return new WaitForSeconds(0.3f);
		explosionObject.SetActive(false);
		gameObject.SetActive(false);

    }	

}