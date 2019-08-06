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
        // TODO: Redefines with class or struct
        MakeStars(maxFGStars, FGStars, starColors, FGStarEndAction);
        MakeStars(maxBGStars, BGStars, new[] { new Color(1f, 1f, 1f) }, BGStarEndAction);
	}

    private void MakeStars(int starCount, MonoObjectPool[] poolList, Color[] starColor, System.Action<BaseBGObject> action)
    {
        for (int i = 0; i < starCount; ++i)
        {
            var star = poolList[UnityEngine.Random.Range(0, poolList.Length)].GetObject();

			star.GetComponent<UnityEngine.UI.Image>().color = starColor[i % starColor.Length];

			var bgObject = star.GetComponent<BaseBGObject>();
            bgObject.moveSpeed = -(4.5f * Random.value + 0.5f);
            bgObject.InitializeObject(action);

			star.transform.parent = transform;
        }
    }

    private void FGStarEndAction(BaseBGObject bgObject)
    {
        // TODO: to lamda func
        int totalActiveCount = 0;
        foreach (var monoPooledObject in FGStars)
        {
            totalActiveCount += monoPooledObject.GetActiveObjectCount();
        }

        for (int i = 0; i < maxFGStars - totalActiveCount; ++i)
        {
            MakeStars(maxFGStars, FGStars, starColors, FGStarEndAction);
        }

    }

    // TODO: merge duplicated function
    private void BGStarEndAction(BaseBGObject bgObject)
    {
        // TODO: to lamda func
        int totalActiveCount = 0;
        foreach (var monoPooledObject in BGStars)
        {
            totalActiveCount += monoPooledObject.GetActiveObjectCount();
        }

        for (int i = 0; i < maxBGStars - totalActiveCount; ++i)
        {
            MakeStars(maxBGStars, BGStars, new[] { new Color(1f, 1f, 1f) }, BGStarEndAction);
        }
    }
}