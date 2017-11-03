using UnityEngine;
using System.Collections;

public class MovingTarget : MonoBehaviour {

	// This script attaches to an invisible game object below the 
	// screen area and acts as a target for the enemy ships rather than the player.
	private bool dirRight = true;
	public float moveSpeed = 2.5f;
	
	void Update () 
	{
		if (dirRight)
			transform.Translate (Vector2.right * moveSpeed * Time.deltaTime);
		else
			transform.Translate (-Vector2.right * moveSpeed * Time.deltaTime);
		
		if (transform.position.x >= 3.0f) {
			dirRight = false;
		}
		if (transform.position.x <= -3) {
			dirRight = true;
		}
	}
}