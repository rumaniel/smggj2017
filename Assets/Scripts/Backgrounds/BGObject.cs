using UnityEngine;

public class BGObject : UnitBase
{

	public float moveSpeed;
	public bool isMoving;

	Vector2 min;
	Vector2 max;

	void Awake()
	{
		isMoving = false;

		// This is the bottom left most part of the screen.
		min = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));
		// This is the top right most part of the screen.
		max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));

		// Add the planet sprite half height to max y.
		max.y = max.y + GetComponent<UnityEngine.UI.Image> ().sprite.bounds.extents.y;
		// Subtract the planet sprite half height to min y.
		min.y = min.y - GetComponent<UnityEngine.UI.Image> ().sprite.bounds.extents.y;
	}
	// Update is called once per frame
	protected override void Update () 
	{
        base.Update();
		
		if (!isMoving) return;

		// Get the current position of the object.
		Vector2 position = transform.position;
		// Compute the objects new position.
		position = new Vector2 (position.x, position.y + moveSpeed * Time.deltaTime);
		// Update the objects position.
		transform.position = position;
		// If the object hits the minimum y position then stop moving it.
		if (transform.position.y < min.y) 
		{
			isMoving = false;
		}
	}

	// Function to reset the objects position.
	public void ResetPosition()
	{
		// Reset the position of the object to random x and max y.
		transform.position = new Vector2 (Random.Range (min.x, max.x), max.y);
	}
}