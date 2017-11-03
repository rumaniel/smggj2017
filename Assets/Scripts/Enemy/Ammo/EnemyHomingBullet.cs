using UnityEngine;
using System.Collections;

public class EnemyHomingBullet : MonoBehaviour {

	public float moveSpeed; // The bullets movement speed.
	Vector2 _direction; // The bullets direction.
	bool isReady; // To know when the bullet direction is set.

	void Awake ()
	{
		isReady = false;
	}
	
	public void SetDirection(Vector2 direction)
	{
		_direction = direction.normalized;

		isReady = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (isReady) 
		{
			// Get the bullets current position.
			Vector2 position = transform.position;
			// Compute the bullets new position.
			position += _direction * moveSpeed * Time.deltaTime;
			// Update the position.
			transform.position = position;
			// This is the top and bottom most point of the screen.
			Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2 (0,0));
			Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2 (1,1));

			// If the bullet leaves the screen area, destroy it.
			if((transform.position.x < min.x) || (transform.position.x > max.x) ||
			   (transform.position.y < min.y) || (transform.position.y > max.y))
			{
				Destroy(gameObject);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		// Detect collision of the enemy bullet with the player ship.
		if (col.tag == "PlayerShip") 
		{
			// Destroy the bullet.
			Destroy(gameObject);
		}
	}
}