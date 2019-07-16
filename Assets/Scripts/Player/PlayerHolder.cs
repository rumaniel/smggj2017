using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHolder : MonoBehaviour
{
	// Update is called once per frame
	void Update ()
	{
		Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0,0));
		Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1,1));

		max.x = max.x - 0.225f;
		min.x = min.x + 0.225f;

		max.y = max.y - 0.285f;
		min.y = min.y + 0.285f;
		// This makes sure our player never leaves the screen area.
		GetComponent<Rigidbody2D>().position = new Vector2
			(
				Mathf.Clamp (GetComponent<Rigidbody2D>().position.x, min.x, max.x),  //X
				Mathf.Clamp (GetComponent<Rigidbody2D>().position.y, min.y, max.y)	 //Y
			);
	}
}
