using UnityEngine;

public class StarGenerator : MonoBehaviour
{
	public bool enableBackgroundStars;
	public MonoObjectPool[] FGStars;
	public MonoObjectPool[] BGStars;
	public int maxFGStars;
	public int maxBGStars;

    [SerializeField]
	private Color[] starColors =
    {
		new Color (0.5f, 0.5f, 1f),
		new Color (0, 1f, 1f),
		new Color (1f, 1f, 0),
		new Color (1f, 0, 0),
		new Color (1f, 1f, 1f),
	};

// TODO: Design model. Decide to restrict the stars in the scene
	private void Start ()
	{
		for (int i = 0; i < maxFGStars; ++i)
		{
			MonoPooledObject star = FGStars[UnityEngine.Random.Range(0, FGStars.Length)].GetObject();

			star.GetComponent<UnityEngine.UI.Image>().color = starColors[i % starColors.Length];
			var bgObject = star.GetComponent<BaseBGObject>();
            bgObject.moveSpeed = -(4.5f * Random.value + 0.5f);
            bgObject.InitializeObject(FGStarEndAction);

			star.transform.parent = transform;

		}

		for (int i = 0; i < maxBGStars; ++i)
		{
			MonoPooledObject bgStar = BGStars[UnityEngine.Random.Range(0, BGStars.Length)].GetObject();

			bgStar.GetComponent<UnityEngine.UI.Image>().color = Color.white;
			bgStar.GetComponent<BaseBGObject>().moveSpeed = -(0.1f * Random.value + 0.2f);
			bgStar.transform.parent = transform;
		}
	}

    private void FGStarEndAction(BaseBGObject bgObject)
    {

    }

    private void BGStarEndAction(BaseBGObject bgObject)
    {

    }

    [ContextMenu("SetupStars")]
    public void SetupStars()
    {

    }
}