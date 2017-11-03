using UnityEngine;
using System.Collections;

public class PlayerBullet : MonoBehaviour {

	public float shotSpeed;
	public GameObject hitEffect;
	
	// Update is called once per frame
	void Update () 
	{
		// Get the bullets position.
		Vector3 pos = transform.position;
		// Compute the bullets new position.
		Vector3 velocity = new Vector3 (0, shotSpeed * Time.deltaTime, 0);
		pos += transform.rotation * velocity;
		// Update the bullet position.
		transform.position = pos;
		// This is the top right point of the screen.
		Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));

		// If the bullet leaves the screen area, destroy it.
		if (transform.position.y > max.y) 
		{
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		// Detect collision of the player bullet with an enemy ship or world hazard.
		if ((col.tag == "EnemyShip") || (col.tag == "WorldHazard")) 
		{
			// Play the bullet spark effect.
			PlayHitEffect ();
			// Destroy the player bullet..
			Destroy(gameObject);
		}
	}

	// Function to instantiate a particle effect.
	void PlayHitEffect()
	{
		GameObject hitSpark = (GameObject)Instantiate (hitEffect);
		hitSpark.transform.position = transform.position;
	}
}