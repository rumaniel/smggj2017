using UnityEngine;
using System.Collections;

public class PlayerBullet : BulletBase 
{
	protected override void OnTriggerEnter2D(Collider2D col)
	{
		// Detect collision of the player bullet with an enemy ship or world hazard.
		if ((col.tag == "EnemyShip") || (col.tag == "WorldHazard")) 
		{
			PlayHitEffect ();
			ConsumeBullet();
		}
	}
}