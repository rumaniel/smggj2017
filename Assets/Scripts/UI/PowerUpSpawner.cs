using UnityEngine;
using System.Collections;

public class PowerUpSpawner : MonoBehaviour {
	
	public GameObject[] powerUps;
	
	float maxSpawnRateInSeconds = 30f;
	
	void SpawnRandomPowerUp()
	{
		// This is the bottom left most point of the screen.
		Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0,0));
		// This is the bottom right most point of the screen.
		Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1,1));
		
		// Create a powerUp as a new gameObject from the available gameObjects in the array.
		GameObject aPowerUp = Instantiate (powerUps [UnityEngine.Random.Range (0, powerUps.Length)]);
		aPowerUp.transform.position = new Vector2 (Random.Range (min.x, max.x), max.y);
		
		// Schedule when to spawn the next power up.
		ScheduleNextPowerUpSpawn ();
	}
	
	void ScheduleNextPowerUpSpawn()
	{
		float spawnInSeconds;
		
		if (maxSpawnRateInSeconds > 1f) {
			//Pick a random number between 30 and the max spawn rate.
			spawnInSeconds = Random.Range (15f, maxSpawnRateInSeconds);
		} else
			spawnInSeconds = 1f;
		// Spawn a power up according the the timer.
		Invoke ("SpawnRandomPowerUp", spawnInSeconds);
	}
	// This will increase the spawn rate over time.
	void IncreaseSpawnRate()
	{
		if(maxSpawnRateInSeconds > 1f)
			maxSpawnRateInSeconds--;
		
		if(maxSpawnRateInSeconds == 1f)
			CancelInvoke("IncreaseSpawnRate");
	}
	// This function starts the power up spawner.
	public void SchedulePowerUpSpawner()
	{
		maxSpawnRateInSeconds = 30f;
		
		Invoke ("SpawnRandomPowerUp", maxSpawnRateInSeconds);
		
		//Increase spawn rate of power ups every 30 Seconds.
		InvokeRepeating ("IncreaseSpawnRate", 0f, 30f);
	}
	// This function stops the power up spawner.
	public void UnschedulePowerUpSpawner()
	{
		CancelInvoke ("SpawnRandomPowerUp");
		CancelInvoke ("IncreaseSpawnRate");
	}
}