using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
			spriteBase.rotation *= Quaternion.Euler(0, 180f, 0);
		}
		this.pattern = pattern;
		this.shipInfo = ship;
		// sprite change
	}

	public override void Init()
	{
		base.Init();
		StartCoroutine("DoUnitPattern");
	}

	void Update () 
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

	IEnumerator DoUnitPattern()
	{
		// appear Process

		// moving Process
		switch (pattern.movingPattern)
		{
			case Defines.EnemyMovingPattern.HorizontalMiddle:
			case Defines.EnemyMovingPattern.Custom:
			case Defines.EnemyMovingPattern.IdleAndFacePlayer:
				while (true)
				{
					Vector2 dir = GameManager.Instance.playerShip.transform.position - spriteBase.position;
					dir.Normalize ();
					
					float zAngle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg - 90;
					
					Quaternion desiredRot = Quaternion.Euler (0, 0, zAngle);
					
					spriteBase.rotation = Quaternion.RotateTowards (spriteBase.rotation, desiredRot, shipInfo.moveSpeed * Time.deltaTime);
					yield return null;
				}
			break;
			case Defines.EnemyMovingPattern.Idle:
			default:
				break;
		}

		yield return null;
	}

}