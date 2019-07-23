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

	private void Start ()
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
			star.transform.position = new Vector2(Random.Range (min.x, max.x), Random.Range(min.y, max.y));
			star.GetComponent<BaseBGObject>().moveSpeed = -(4.5f * Random.value + 0.5f);
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
		for (int i = 0; i < maxBGStars; ++i)
		{
			MonoPooledObject bgStar = BGStars[UnityEngine.Random.Range(0, BGStars.Length)].GetObject();

			bgStar.GetComponent<UnityEngine.UI.Image>().color = Color.white;
			bgStar.transform.position = new Vector2(Random.Range (min.x, max.x), Random.Range(min.y, max.y));
			bgStar.GetComponent<BaseBGObject> ().moveSpeed = -(0.1f * Random.value + 0.2f);
			bgStar.transform.parent = transform;
		}
	}
}