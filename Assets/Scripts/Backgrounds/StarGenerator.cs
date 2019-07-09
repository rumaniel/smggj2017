using UnityEngine;

public class StarGenerator : MonoBehaviour 
{
	public bool enableBackgroundStars; // Enable this to show layered stars.
	public MonoObjectPool[] FGStars; // These are your stars which will be more visible in the foreground.
	public MonoObjectPool[] BGStars; // These are your stars which will be visible in the background.
	public int maxFGStars; // This is the max number of stars visible of the screen.
	public int maxBGStars; // This is the max number of stars visible of the screen.
	public bool onlyWhiteStars; // Tick this in the inspector to limit all stars to 'White'.

	// Colour Array
	Color[] starColors = {
		// You can set as many or as little variants of star colors as you want here.
		new Color (0.5f, 0.5f, 1f), // Blue
		new Color (0, 1f, 1f), // Green
		/*new Color (1f, 1f, 0), // Yellow
		new Color (1f, 0, 0), // Red*/
		new Color (1f,1f,1f), // White
	};

	// Use this for initialization
	void Start () 
	{
		// This is the bottom left most part of the screen.
		Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));
		// This is the top right most part of the screen.
		Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));

		// Loop to create the star field.
		for (int i = 0; i < maxFGStars; ++i) 
		{
			MonoPooledObject star = FGStars[UnityEngine.Random.Range(0, FGStars.Length)].GetObject();

			if (onlyWhiteStars == true) {
				star.GetComponent<UnityEngine.UI.Image>().color = Color.white;
			} else {
				star.GetComponent<UnityEngine.UI.Image>().color = starColors[i % starColors.Length];
			}
			// Set the position of the star (random x and random y).
			star.transform.position = new Vector2(Random.Range (min.x, max.x), Random.Range(min.y, max.y));
			// Set a random speed for the star.
			star.GetComponent<Star> ().moveSpeed = -(4.5f * Random.value + 0.5f);
			// Make the star a child of the star generator.
			star.transform.parent = transform;
		}

		if (enableBackgroundStars == true) {
			BackgroundStarField ();
		} else
			return;
	}

	void BackgroundStarField ()
	{
		// This is the bottom left most part of the screen.
		Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));
		// This is the top right most part of the screen.
		Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));

		// Loop to create the star field.
		for (int i = 0; i < maxBGStars; ++i) 
		{
			MonoPooledObject bgStar = BGStars[UnityEngine.Random.Range(0, BGStars.Length)].GetObject();

			// Set the color.
			bgStar.GetComponent<UnityEngine.UI.Image>().color = Color.white;
			// Set the position of the star (random x and random y).
			bgStar.transform.position = new Vector2(Random.Range (min.x, max.x), Random.Range(min.y, max.y));
			// Set a random speed for the star.
			bgStar.GetComponent<Star> ().moveSpeed = -(0.1f * Random.value + 0.2f);
			// Make the star a child of the star generator.
			bgStar.transform.parent = transform;
		}
	}
}