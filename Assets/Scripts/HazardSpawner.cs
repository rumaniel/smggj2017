using UnityEngine;
using System.Collections;

public class HazardSpawner : MonoBehaviour {
	
	public GameObject[] hazards;
	
	float maxSpawnRateInSeconds = 10f;
	
	void SpawnRandomHazard()
	{
		// This is the bottom left most point of the screen.
		Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0,0));
		// This is the bottom right most point of the screen.
		Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1,1));
		
		// Create a hazard as a new gameObject from the available gameObjects in the array.
		GameObject aHazard = Instantiate (hazards [UnityEngine.Random.Range (0, hazards.Length)]);
		aHazard.transform.position = new Vector2 (Random.Range (min.x, max.x), max.y);
		
		// Schedule when to spawn the next hazard.
		ScheduleNextHazardSpawn ();
	}
	
	void ScheduleNextHazardSpawn()
	{
		float spawnInSeconds;
		
		if (maxSpawnRateInSeconds > 1f) {
			//Pick a random number between 1 and the max spawn rate.
			spawnInSeconds = Random.Range (1f, maxSpawnRateInSeconds);
		} else
			spawnInSeconds = 1f;
		// Spawn a hazard according the the timer.
		Invoke ("SpawnRandomHazard", spawnInSeconds);
	}
	// This will increase the spawn rate over time.
	void IncreaseSpawnRate()
	{
		if(maxSpawnRateInSeconds > 1f)
			maxSpawnRateInSeconds--;
		
		if(maxSpawnRateInSeconds == 1f)
			CancelInvoke("IncreaseSpawnRate");
	}
	// This function starts the hazard spawner.
	public void ScheduleHazardSpawner()
	{
		maxSpawnRateInSeconds = 5f;
		
		Invoke ("SpawnRandomHazard", maxSpawnRateInSeconds);
		
		//Increase spawn rate of ships every 30 Seconds.
		InvokeRepeating ("IncreaseSpawnRate", 0f, 30f);
	}
	// This function stops the hazard spawner.
	public void UnscheduleHazardSpawner()
	{
		CancelInvoke ("SpawnRandomHazard");
		CancelInvoke ("IncreaseSpawnRate");
	}
}