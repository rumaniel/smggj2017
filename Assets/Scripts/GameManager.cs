using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoSingleton<GameManager> 
{
	public GameObject unitBase;
	public List<StageInfo> stageList;
	public GameObject playButton;
	public GameObject playerShip;
	public GameObject hud;
	public GameObject timerText;
	//public GameObject enemySpawner;
	//public GameObject hazardSpawner;
	//public GameObject powerUpSpawner;
	public GameObject GameOver;

	public enum GameManagerState
	{
		Opening,
		InGame,
		GameOver,
		InterMission
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
			//hud.SetActive(false);
			// Display the game title.
			// GameTitle.SetActive(true);
			// Show the 'Play' button.
			playButton.SetActive(true);
				break;
		case GameManagerState.InGame:
			// Hide the cursor.
			Cursor.visible = false;
			playButton.SetActive(false);
			playerShip.GetComponent<PlayerControl>().Init();

			StageManager.Instance.StartStage(stageList[0]);

				break;
		case GameManagerState.GameOver:
			// Stop the timer.
			//timerText.GetComponent<TimeCounter>().StopTimeCounter();
			// Stop the enemy spawner.
			//enemySpawner.GetComponent<EnemySpawner>().UnscheduleEnemySpawner();
			// Stop the hazard spawner.
			//hazardSpawner.GetComponent<HazardSpawner>().UnscheduleHazardSpawner();
			// Stop the power up spawner.
			//powerUpSpawner.GetComponent<PowerUpSpawner>().UnschedulePowerUpSpawner();
			// Display the game over text.
			GameOver.SetActive (true);
			// Change the game state to 'Opening' after 5 seconds.
			Invoke("RestartInGame", 5f);
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
	public void StartInGame()
	{
		GMState = GameManagerState.InGame;
		UpdateGameManagerState ();
	}
	// This will reload the main scene.
	public void RestartInGame()
	{
		// Reset the scene.
		SceneManager.LoadScene("Game");
	}
}