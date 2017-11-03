using UnityEngine;
using System.Collections;

public class EnemyBase : MonoBehaviour {

	GameObject gameManager; // Reference to the gameManager gameObject.
	GameObject scoreUIText; // Reference to the scoreText gameObject.
	
	int maxHealth = 100;
	public int curHealth;
	public int pointsValue;

	public GameObject Explosion; // This is the explosion prefab.

	private SpriteRenderer spriteRenderer; // This is our spriteRenderer.

	// Use this for initialization
	void Start () 
	{
		// Set the current health to be equal to maxHealth on game start.
		curHealth = maxHealth;
		// Find gameObjects with the 'ScoreText' tag.
		scoreUIText = GameObject.FindGameObjectWithTag ("ScoreText");
		// Find gameObjects with the 'GameManager' tag.
		gameManager = GameObject.FindGameObjectWithTag ("GameManager");
		// Find the spriteRenderer component.
		spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		//This is the bottom left most point of the screen.
		Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0,0));

		// If the enemy is outside the screen area, destroy it.
		if (transform.position.y < min.y) 
		{
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		// Detect collision of the enemy ship with the player ship OR player bullet.
		if ((col.tag == "PlayerShip") || (col.tag == "PlayerBullet")) 
		{
			// Flash sprite on trigger activate.
			StartCoroutine (FlashSprite ());
			// Remove health.
			curHealth -= 35;

			if (curHealth > 0) { 
				return;
			} 
			// If the enemy is out of health.
			else if (curHealth <= 0) {
				// Explode if the enemy is out of health.
				PlayExplosion ();
				// Increase combo count.
				gameManager.GetComponent<ComboText>().AwardKill();
				// Show floating combat text.
				GetComponent<FloatingCombatText>().ShowCombatText();
				// Add points to the score.
				scoreUIText.GetComponent<GameScore>().Score += pointsValue;
				//Destroy the enemy.
				Destroy (gameObject);
			}
		}
	}
	// Coroutine to flash the material color and create a hit effect.
	IEnumerator FlashSprite () {
		spriteRenderer.material.color = new Color (255f,225f,255f,255f);
		yield return new WaitForSeconds(0.1f);
		spriteRenderer.material.color = Color.white;
		yield return new WaitForSeconds(0.1f);
	}

	// Function to instantiate an explosion.
	void PlayExplosion()
	{
		GameObject explosion = (GameObject)Instantiate (Explosion);
		// Set the position of the explosion.
		explosion.transform.position = transform.position;
	}
}