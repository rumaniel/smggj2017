using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
	
	public GameObject[] enemyShips;

	float maxSpawnRateInSeconds = 5f;
	
	void SpawnRandomEnemy()
	{
		// This is the bottom left most point of the screen.
		Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0,0));
		// This is the bottom right most point of the screen.
		Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1,1));
		
		// Create an enemy as a new gameObject from the available gameObjects in the array.
		GameObject anEnemy = Instantiate (enemyShips [UnityEngine.Random.Range (0, enemyShips.Length)]);
		anEnemy.transform.position = new Vector2 (Random.Range (min.x, max.x), max.y);
		
		// Schedule when to spawn the next enemy.
		ScheduleNextEnemySpawn ();
	}

	void ScheduleNextEnemySpawn()
	{
		float spawnInSeconds;

		if (maxSpawnRateInSeconds > 1f) {
			//Pick a random number between 1 and the max spawn rate.
			spawnInSeconds = Random.Range (1f, maxSpawnRateInSeconds);
		} else
			spawnInSeconds = 1f;
		// Spawn an enemy according the the timer.
		Invoke ("SpawnRandomEnemy", spawnInSeconds);
	}
	// This will increase the spawn rate over time.
	void IncreaseSpawnRate()
	{
		if(maxSpawnRateInSeconds > 1f)
			maxSpawnRateInSeconds--;

		if(maxSpawnRateInSeconds == 1f)
			CancelInvoke("IncreaseSpawnRate");
	}
	// This function starts the enemy spawner.
	public void ScheduleEnemySpawner()
	{
		maxSpawnRateInSeconds = 5f;

		Invoke ("SpawnRandomEnemy", maxSpawnRateInSeconds);

		//Increase spawn rate of ships every 30 Seconds.
		InvokeRepeating ("IncreaseSpawnRate", 0f, 30f);
	}
	// This function stops the enemy spawner.
	public void UnscheduleEnemySpawner()
	{
		CancelInvoke ("SpawnRandomEnemy");
		CancelInvoke ("IncreaseSpawnRate");
	}
}