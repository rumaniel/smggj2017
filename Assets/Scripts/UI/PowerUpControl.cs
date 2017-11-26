using UnityEngine;
using System.Collections;

public class PowerUpControl : MonoBehaviour {
	
	public float moveSpeed; 
	public bool isMoving; 
	
	Vector2 min; // This is the bottom-left point of the screen.
	Vector2 max; // This is the top-right point of the screen.
	
	void Awake()
	{
		isMoving = true;
		
		// This is the bottom left most part of the screen.
		min = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));
		// This is the top right most part of the screen.
		max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));
		
		// Add the planet sprite half height to max y.
		max.y = max.y + GetComponent<SpriteRenderer> ().sprite.bounds.extents.y;
		// Subtract the planet sprite half height to min y.
		min.y = min.y - GetComponent<SpriteRenderer> ().sprite.bounds.extents.y;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!isMoving)
			return;
		
		// Get the current position.
		Vector2 position = transform.position;
		// Compute the new position.
		position = new Vector2 (position.x, position.y + -moveSpeed * Time.deltaTime);
		// Update the position.
		transform.position = position;
		
		// If we hit the minimum y position then destroy it.
		if (transform.position.y < min.y) 
		{
			isMoving = false;
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if ((col.tag == "PlayerShip"))
		{
			//Destroy the power-up.
			Destroy (gameObject);
		}
	}
}