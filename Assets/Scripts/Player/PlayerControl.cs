using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerControl : PlayerBaseControl 
{
	public List<GameObject> followerList;
	float timeToFire = 0f;

	public override void Init()
	{
		base.Init();
		
		transform.position = new Vector2 (0, -2.5f);
	}
	
	void Update () 
	{
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

	void MovePlayer()
	{
		float horizontalRatio = Input.GetAxis("Horizontal") * shipInfo.moveSpeed * Time.deltaTime;
		float verticalRatio   = Input.GetAxis("Vertical") * shipInfo.moveSpeed * Time.deltaTime;

		transform.position = new Vector3(transform.position.x + horizontalRatio, transform.position.y + verticalRatio, transform.position.z);
	}

	protected override void OnTriggerEnter2D(Collider2D col)
	{
		// Detect collision of the player ship with the enemy ship or player bullet.
		if ((col.tag == "EnemyShip") || (col.tag == "EnemyBullet")) 
		{
			// This is the current amount you get hurt when hit by an enemy ship or bullet.
			GetComponent<PlayerHealth>().TakeDamage(10f);

			if(GetComponent<PlayerHealth>().curHealth <= 0) // If the player is out of life, destroy player.
			{
				// Explode if the player is out of health.
				PlayExplosion();
				// Change the gamestate to 'Game Over'.
				gameManger.SetGameManagerState(GameManager.GameManagerState.GameOver);
				// Hide the player ship.
				gameObject.SetActive(false);
			}
		}
		// Detect collision of the player ship with a world hazard, and reduce current health to 0.
		else if ((col.tag == "WorldHazard"))
		{
			// Currently hitting a world hazard will reduce your health to 0 and destroy you.
			GetComponent<PlayerHealth>().TakeDamage(100f);

			// Explode if the player is out of health.
			PlayExplosion();
			// Change the gamestate to 'Game Over'.
			gameManger.SetGameManagerState(GameManager.GameManagerState.GameOver);
			// Hide the player ship.
			gameObject.SetActive(false);
		}
	}
}