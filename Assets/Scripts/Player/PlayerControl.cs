using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerControl : PlayerBaseControl 
{
	public List<FollowerControl> followerList;
	float timeToFire = 0f;

	public override void Init()
	{
		base.Init();
		PlayerData.Instance.SetPlayerControl(this);
		unitData.SetHealth(this.shipInfo.health);
		transform.position = new Vector2 (0, -2.5f);
	}
	
	public void PatchFollwerInfo()
	{
		
	}

	void Update () 
	{
		if (GameManager.Instance.isPause) return;
		MovePlayer();

		if (isInitialize && Input.GetButton("Fire"))
		{
			if (currentWeapon.GetFireRate() == 0f) 
			{
				FireWeapon ();
			} 
			else if (Time.time > timeToFire)  
			{
				timeToFire = Time.time + 1f / currentWeapon.GetFireRate();
				FireWeapon ();
			}
		}
	}

	public override void FireWeapon()
	{
		base.FireWeapon();
        for (int i = 0 ; i < followerList.Count; ++i)
		{
			if (followerList[i].gameObject.activeInHierarchy)
			{
				followerList[i].FireWeapon();
			}
		}
	}
	

	void MovePlayer()
	{
		float horizontalRatio = Input.GetAxis("Horizontal") * shipInfo.moveSpeed * Time.deltaTime;
		float verticalRatio   = Input.GetAxis("Vertical") * shipInfo.moveSpeed * Time.deltaTime;

		transform.position = new Vector3(transform.position.x + horizontalRatio, transform.position.y + verticalRatio, transform.position.z);
	}

	protected override void OnTriggerEnter2D(Collider2D col)
	{
		if ((col.tag == "EnemyBullet"))
		{
			BulletBase bullet = col.transform.GetComponent<BulletBase>();
			if (bullet != null)
			{
				PlayerData.Instance.AddHealth(-bullet.weaponInfo.damage);
			}
		}
	}
}