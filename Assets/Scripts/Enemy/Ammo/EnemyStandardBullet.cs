using UnityEngine;
using System.Collections;

public class EnemyStandardBullet : MonoBehaviour {
	
	public float shotSpeed; // The bullets movement speed.
	
	bool isReady;
	
	void Awake ()
	{
		isReady = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (isReady) 
		{
			// Find the position of the projectile and calculate its movement path.
			Vector3 pos = transform.position;
			Vector3 velocity = new Vector3 (0, shotSpeed * Time.deltaTime, 0);
			
			pos += transform.rotation * velocity;
			
			transform.position = pos;
			
			// This is the top and bottom most point of the screen.
			Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));
			Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));
			
			// If the bullet leaves the screen area, destroy it.
			if ((transform.position.x < min.x) || (transform.position.x > max.x) ||
			    (transform.position.y < min.y) || (transform.position.y > max.y)) {
				Destroy (gameObject);
			}
		}
	}
	
	void OnTriggerEnter2D(Collider2D col)
	{
		// Detect collision of the enemy bullet with the player ship.
		if ((col.tag == "PlayerShip")) 
		{
			// Destroy the Enemy Bullet.
			Destroy(gameObject);
		}
	}
}