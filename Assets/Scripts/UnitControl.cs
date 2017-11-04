using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class UnitControl : PlayerBaseControl 
{
	public SpriteRenderer sprite;
	public Transform spriteBase;
    public bool isMoving;
	public PatternInfo pattern;
    
	float timeToFire = 0f;

	public void SetUnitInfo(PatternInfo pattern, ShipInfo ship, bool isEnemy)
	{
		if (isEnemy)
		{
			spriteBase.rotation = Quaternion.Euler(0, 0, 180f);
			gameObject.tag = "EnemyShip";
		}
		else
		{
			gameObject.tag = "Unit";
		}
		this.pattern = pattern;
		this.shipInfo = ship;
		sprite.sprite = shipInfo.sprite;
		unitData.SetHealth(this.shipInfo.health);
	}

	public override void Init()
	{
		base.Init();
		isMoving = false;
		StartCoroutine("DoUnitPattern");
	}

	void Update () 
	{
		if (GameManager.Instance.isPause) return;
		if (!isMoving)
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

	protected override void OnTriggerEnter2D(Collider2D col)
	{
		if ((col.tag == "PlayerBullet"))
		{
			BulletBase bullet = col.transform.GetComponent<BulletBase>();
			if (bullet != null)
			{
				unitData.AddHealth(-bullet.weaponInfo.damage);
				if (unitData.IsDie())
				{
					StopCoroutine("DoUnitPattern");
					isMoving = true;
					StartCoroutine("Die");
				}
			}
		}
	}

	IEnumerator DoUnitPattern()
	{
		if (pattern == null) yield break;

		isMoving = true;
		// appear Process
		switch (pattern.appearPattern)
		{
			case Defines.EnemyAppearPattern.Custom:
				transform.position = pattern.customAppearList[0];
				for (int i = 1; i < pattern.customAppearList.Count; ++i)
				{
					transform.DOMove(pattern.customAppearList[i], 1f);
					yield return new WaitForSeconds(1f);
					// while (!Mathf.Approximately(pattern.customAppearList[i].x, transform.position.x) && !Mathf.Approximately(pattern.customAppearList[i].y, transform.position.y))
					// {
					// 	MoveTo(pattern.customAppearList[i]);
					// 	yield return null;
					// }
				}
			break;
		}

		isMoving = false;
		// moving Process
		switch (pattern.movingPattern)
		{
			case Defines.EnemyMovingPattern.HorizontalMiddle:
			case Defines.EnemyMovingPattern.Custom:
			case Defines.EnemyMovingPattern.IdleAndRotate:
				float rotateStayTime = 0f;
				while (rotateStayTime < pattern.stayTime || pattern.leavePattern == Defines.EnemyLeavePattern.Stay)
				{
					GameObject go = GameObject.FindGameObjectWithTag ("MovingTarget");
					Vector2 dir = go.transform.position - spriteBase.position;
					dir.Normalize ();
					
					float zAngle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg - 90;
					
					Quaternion desiredRot = Quaternion.Euler (0, 0, zAngle);
					
					spriteBase.rotation = Quaternion.RotateTowards (spriteBase.rotation, desiredRot, shipInfo.rotateSpeed * Time.deltaTime);
					yield return null;					
					rotateStayTime += Time.deltaTime;
					
				}
				break;
			case Defines.EnemyMovingPattern.IdleAndFacePlayer:
				float accumulatedTime = 0f;	
				while (accumulatedTime < pattern.stayTime || pattern.leavePattern == Defines.EnemyLeavePattern.Stay)
				{
					Vector2 dir = GameManager.Instance.playerShip.transform.position - spriteBase.position;
					dir.Normalize ();
					
					float zAngle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg - 90;
					
					Quaternion desiredRot = Quaternion.Euler (0, 0, zAngle);
					
					spriteBase.rotation = Quaternion.RotateTowards (spriteBase.rotation, desiredRot, shipInfo.rotateSpeed * Time.deltaTime);
					yield return null;
					accumulatedTime += Time.deltaTime;
				}
			break;
			case Defines.EnemyMovingPattern.Idle:
			default:
				yield return new WaitForSeconds(pattern.stayTime);
				break;
		}
		
		switch (pattern.leavePattern)
		{
			case Defines.EnemyLeavePattern.Custom:
				isMoving = true;
				for (int i = 0; i < pattern.leaveDirectionList.Count; ++i)
				{
					transform.DOMove(pattern.leaveDirectionList[i], 1f);
					yield return new WaitForSeconds(1f);
				}
				break;			
			case Defines.EnemyLeavePattern.Stay:
			default:
				while (true)
					yield return null;
				break;
		}

		GetComponent<MonoPooledObject>().ReturnToPool();
		yield return null;
	}

	// todo
	public void MoveTo(Vector2 toPosition)
	{
		float deltax = toPosition.x - transform.position.x > 0 ? 1 : -1;
		float deltay = toPosition.y - transform.position.y > 0 ? -1 : 1;

		transform.position = new Vector3(transform.position.x + deltax * shipInfo.moveSpeed * Time.deltaTime, 
										 transform.position.y + deltay * shipInfo.moveSpeed * Time.deltaTime, 
										 transform.position.z);
	}

}