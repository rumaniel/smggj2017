using UnityEngine;
using System.Collections;

public class EnemyOrbWeapon : MonoBehaviour {

	public Transform enemyFirePoint01;
	public Transform enemyFirePoint02;
	public Transform enemyFirePoint03;
	public Transform enemyFirePoint04;
	public Transform enemyFirePoint05;
	public Transform enemyFirePoint06;

	// Burst Ammo Prefab goes here.
	public GameObject enemyBullet;

	// We have a single firing sound for our orb (chosen randomly from an array), as we don't want to fire off a sound per projectile like other enemies.
	public AudioSource[] firingSounds;

	// This is the enemy attack rate.
	public float fireDelay = 0.50f;

	float cooldownTimer = 0;

	void Update()
	{
		cooldownTimer -= Time.deltaTime;

		if (cooldownTimer <= 0) 
		{
			// Fire!
			cooldownTimer = fireDelay;

			FireOrbWeapon();
		}
	}

	// Fire all 6 bullets at once.
	void FireOrbWeapon()
	{
		// Get a reference to the player ship.
		GameObject playerShip = GameObject.FindWithTag ("PlayerShip");

		// Check the player isn't dead.
		if (playerShip != null) 
		{
			// Spawn bullet objects at each firing position/rotation.
			Instantiate(enemyBullet, enemyFirePoint01.position, enemyFirePoint01.rotation);
			Instantiate(enemyBullet, enemyFirePoint02.position, enemyFirePoint02.rotation);
			Instantiate(enemyBullet, enemyFirePoint03.position, enemyFirePoint03.rotation);
			Instantiate(enemyBullet, enemyFirePoint04.position, enemyFirePoint04.rotation);
			Instantiate(enemyBullet, enemyFirePoint05.position, enemyFirePoint05.rotation);
			Instantiate(enemyBullet, enemyFirePoint06.position, enemyFirePoint06.rotation);

			// Select a sound from the array (we are using only one in this example) and play it.
			firingSounds [UnityEngine.Random.Range (0, firingSounds.Length)].Play ();
		}
	}
}