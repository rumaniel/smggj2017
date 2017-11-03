using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour {

	public GameObject playButton;
	public GameObject playerShip;
	public GameObject hud;
	public GameObject timerText;
	public GameObject enemySpawner;
	public GameObject hazardSpawner;
	public GameObject powerUpSpawner;
	public GameObject GameOver;

	public enum GameManagerState
	{
		Opening,
		Gameplay,
		GameOver,
	}

	GameManagerState GMState;
	
	void Start () 
	{
		// Set the default state to 'Opening'.
		GMState = GameManagerState.Opening;
		UpdateGameManagerState ();
	}

	// Function to switch the current game state.
	void UpdateGameManagerState()
	{
		switch (GMState) 
		{
		case GameManagerState.Opening:
			// Show the cursor.
			Cursor.visible = true;
			// Hide the game over text.
			GameOver.SetActive(false);
			// Hide the main HUD text.
			hud.SetActive(false);
			// Display the game title.
			// GameTitle.SetActive(true);
			// Show the 'Play' button.
			playButton.SetActive(true);
				break;
		case GameManagerState.Gameplay:
			// Hide the cursor.
			Cursor.visible = false;
			// Hide the game titles.
			// GameTitle.SetActive(false);
			// Hide the "Play" button during gameplay.
			playButton.SetActive(false);
			// Show the main HUD text.
			hud.SetActive(true);
			// Set the player ship to be visible during gameplay.
			playerShip.GetComponent<PlayerControl>().Init();
			// Start the enemy spawner.
			enemySpawner.GetComponent<EnemySpawner>().ScheduleEnemySpawner();
			// Start the hazard spawner.
			hazardSpawner.GetComponent<HazardSpawner>().ScheduleHazardSpawner();
			// Start the power up spawner.
			powerUpSpawner.GetComponent<PowerUpSpawner>().SchedulePowerUpSpawner();
				break;
		case GameManagerState.GameOver:
			// Stop the timer.
			timerText.GetComponent<TimeCounter>().StopTimeCounter();
			// Stop the enemy spawner.
			enemySpawner.GetComponent<EnemySpawner>().UnscheduleEnemySpawner();
			// Stop the hazard spawner.
			hazardSpawner.GetComponent<HazardSpawner>().UnscheduleHazardSpawner();
			// Stop the power up spawner.
			powerUpSpawner.GetComponent<PowerUpSpawner>().UnschedulePowerUpSpawner();
			// Display the game over text.
			GameOver.SetActive (true);
			// Change the game state to 'Opening' after 5 seconds.
			Invoke("RestartGamePlay", 5f);
			break;
		}
	}
	// This function sets the game state and updates it.
	public void SetGameManagerState(GameManagerState state)
	{
		GMState = state;
		UpdateGameManagerState ();
	}
	// Call this function when the "Play" button is pressed.
	public void StartGamePlay()
	{
		GMState = GameManagerState.Gameplay;
		UpdateGameManagerState ();
	}
	// This will reload the main scene.
	public void RestartGamePlay()
	{
		// Reset the scene.
		SceneManager.LoadScene("Demo");
	}
}