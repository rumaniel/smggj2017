using UnityEngine;

public class StarGenerator : MonoBehaviour
{
	public bool enableBackgroundStars;
	public MonoObjectPool[] FGStars;
	public MonoObjectPool[] BGStars;
	public int maxFGStars;
	public int maxBGStars;
	public bool onlyWhiteStars;

    [SerializeField]
	private Color[] starColors =
    {
		new Color (0.5f, 0.5f, 1f),
		new Color (0, 1f, 1f),
		new Color (1f, 1f, 0),
		new Color (1f, 0, 0),
		new Color (1f, 1f, 1f),
	};

    private Vector2 min;
    private Vector2 max;

	void Start ()
	{
		min = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));
		max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));

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
			star.GetComponent<BaseBGObject>().moveSpeed = -(4.5f * Random.value + 0.5f);
			// Make the star a child of the star generator.
			star.transform.parent = transform;
		}

		if (enableBackgroundStars == true)
        {
			BackgroundStarField ();
		}
        else
			return;
	}

	void BackgroundStarField ()
	{
		// Loop to create the star field.
		for (int i = 0; i < maxBGStars; ++i)
		{
			MonoPooledObject bgStar = BGStars[UnityEngine.Random.Range(0, BGStars.Length)].GetObject();

			// Set the color.
			bgStar.GetComponent<UnityEngine.UI.Image>().color = Color.white;
			// Set the position of the star (random x and random y).
			bgStar.transform.position = new Vector2(Random.Range (min.x, max.x), Random.Range(min.y, max.y));
			// Set a random speed for the star.
			bgStar.GetComponent<BaseBGObject> ().moveSpeed = -(0.1f * Random.value + 0.2f);
			// Make the star a child of the star generator.
			bgStar.transform.parent = transform;
		}
	}
}