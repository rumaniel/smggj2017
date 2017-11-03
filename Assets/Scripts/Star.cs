using UnityEngine;
using System.Collections;

public class Star : MonoBehaviour {

	public float moveSpeed; // The speed of the star.

	// Update is called once per frame
	void Update () 
	{
		// Get the current position of the star.
		Vector2 position = transform.position;
		// Compute the stars new position.
		position = new Vector2 (position.x, position.y + moveSpeed * Time.deltaTime);
		// Update the stars current position.
		transform.position = position;
		// This is the bottom left most part of the screen.
		Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));
		// This is the top right most part of the screen.
		Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));

		// If the star leaves the screen area on the bottom, then position it
		// at the top edge of the screen and randomly between the left and right
		// side of the screen area.
		if (transform.position.y < min.y) 
		{
			transform.position = new Vector2(Random.Range(min.x, max.x), max.y);
		}
	}
}