using UnityEngine;
using System.Collections;

public class EnemyWeapon : MonoBehaviour {
	
	public bool isHomingWeapon; // Check this if you want to use the HomingBullet ammo prefabs.
	public GameObject enemyBullet; // Homing or Standard ammo prefab goes here.

	// This is the enemy attack rate.
	public float attackRate = 0.50f;

	// Unlike the player, for the enemy ships I've opted to use offsets to create the firing points for the enemy bullets just as an extra example. You can
	// of course adapt the player firing method (using the firePoint transform) if you prefer.
	public int bulletCount = 1;
	public Vector3 bulletOffset = new Vector3(0,0.25f,0);
	public Vector3 bullet2Offset = new Vector3(0,0.25f,0);
	
	float cooldownTimer = 0;
	
	void Update()
	{
		cooldownTimer -= Time.deltaTime;
		
		if (cooldownTimer <= 0) 
		{
			// Fire!
			cooldownTimer = attackRate;
			if (isHomingWeapon == true)
			{
				HomingFire ();
			}
			else
				StandardFire();
		}
	}
	
	void StandardFire ()
	{
		if (bulletCount == 1)
		{
			// Get the offset.
			Vector3 offset = transform.rotation * bulletOffset;
			// Instantiate the projecticle.
			GameObject bulletGO = (GameObject)Instantiate(enemyBullet, transform.position + offset, transform.rotation);
			bulletGO.layer = gameObject.layer;
		}
		else if (bulletCount == 2)
		{
			// Get the offset.
			Vector3 offset = transform.rotation * bulletOffset;
			Vector3 offset2 = transform.rotation * bullet2Offset;
			// Instantiate the projecticle.
			GameObject bulletGO = (GameObject)Instantiate(enemyBullet, transform.position + offset, transform.rotation);
			bulletGO.layer = gameObject.layer;
			// Instantiate the projecticle.
			GameObject bullet2GO = (GameObject)Instantiate(enemyBullet, transform.position + offset2, transform.rotation);
			bullet2GO.layer = gameObject.layer;
		}
	}

	void HomingFire ()
	{
		// Get a reference to the player ship.
		GameObject playerShip = GameObject.FindWithTag ("PlayerShip");
		
		// If the player is NOT dead.
		if (playerShip != null) 
		{
			// Get the offset.
			Vector3 offset = transform.rotation * bulletOffset;
			// Instantiate the projecticle.
			GameObject bullet01 = (GameObject)Instantiate(enemyBullet, transform.position + offset, transform.rotation);
			bullet01.layer = gameObject.layer;
			// Compute the bullets direction.
			Vector2 direction = playerShip.transform.position - bullet01.transform.position;
			// Set the bullets direction.
			bullet01.GetComponent<EnemyHomingBullet>().SetDirection (direction);
		}
	}
}