using UnityEngine;
using System.Collections;

public class HazardBase : MonoBehaviour 
{
	GameObject player;
	GameObject gameManager;
	GameObject scoreUIText;
	
	public int maxHealth = 100;
	public int curHealth;
	int damageAmount;

	public float rotationSpeedMin = 25f;
	public float rotationSpeedMax = 75f;
	public int pointsValue;

	public AudioSource hazardDestroySound;
	public GameObject hazardExplosion;

	private SpriteRenderer spriteRenderer; // This is our spriteRenderer.

	void Start ()
	{
		// Set the current health to be equal to maxHealth on game start.
		curHealth = maxHealth;
		// Find the scoreText gameObject using its tag.
		scoreUIText = GameObject.FindGameObjectWithTag ("ScoreText");
		// Find gameObjects with the 'GameManager' tag.
		gameManager = GameObject.FindGameObjectWithTag ("GameManager");
		// Find the spriteRenderer component.
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	void Update()
	{
		transform.Rotate (0,0,Random.Range (rotationSpeedMin, rotationSpeedMax) * Time.deltaTime);

		//This is the bottom left most point of the screen.
		Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0,0));

		if (curHealth > maxHealth) {
			curHealth = maxHealth;
		} else if (curHealth <= 0) {
			// Increase combo count.
			gameManager.GetComponent<ComboText>().AwardKill();
			// Show floating combat text.
			GetComponent<FloatingCombatText>().ShowCombatText();
			// Add points to the score.
			scoreUIText.GetComponent<GameScore> ().Score += pointsValue;
			// Play the explosion.
			PlayExplosion ();
			// Destroy the object.
			Destroy (gameObject);
		}
		// If the enemy is outside the screen area, destroy it.
		if (transform.position.y < min.y) 
		{
			Destroy(gameObject);
		}
	}

	// Update is called once per frame
	void OnTriggerEnter2D(Collider2D col)
	{
		// Detect collision of the hazard with the player bullet.
		if ((col.tag == "PlayerBullet")) {
			
			// Flash sprite on trigger activate.
			StartCoroutine (FlashSprite ());

			curHealth -= 25; // Subtract 25 health.
		}
		// Detect collision of the hazard with the player ship.
		else if ((col.tag == "PlayerShip"))
		{
			// If we hit the player ship, we should just explode.
			curHealth = 0;
		}
	}
	// Coroutine to flash the material color and create a hit effect.
	IEnumerator FlashSprite () {
		spriteRenderer.material.color = new Color (255f,225f,255f,255f);
		yield return new WaitForSeconds(0.1f);
		spriteRenderer.material.color = Color.white;
		yield return new WaitForSeconds(0.1f);
	}

	void PlayExplosion ()
	{
		GameObject Explosion = (GameObject)Instantiate (hazardExplosion);
		Explosion.transform.position = transform.position;
		// Play power-up sound.
		hazardDestroySound.Play ();
	}
}