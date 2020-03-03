using UnityEngine;
using UnityEngine.Events;
using System.Linq;

[System.Serializable]
public class BaseBGObjectEvent : UnityEvent<BaseBGObject> {}

[System.Serializable]
public class StarInformation
{
	public MonoObjectPool[] starPoolList;
    public int maxStarCount;
    public Color[] starColorList;
    public BaseBGObjectEvent callback;
}

public class StarGenerator : MonoBehaviour
{
    public float minStarSpeed = 0.5f;
    public float averageStarSpeed = 4.5f;
	public bool enableBackgroundStars;
    public StarInformation fgStarInfo;
    public StarInformation bgStarInfo;


// TODO: Design model. Decide to restrict the stars in the scene
	private void Start ()
	{
        MakeStars(fgStarInfo);
        MakeStars(bgStarInfo);

        // Debug.Log(CanvasRoot.Instance.RootCanvas.pixelRect);
	}

    private void MakeStars(StarInformation starInfo)
    {
        for (int i = 0; i < starInfo.maxStarCount; ++i)
        {
            var star = starInfo.starPoolList[UnityEngine.Random.Range(0, starInfo.starPoolList.Length)].GetObject();

			star.GetComponent<UnityEngine.UI.Image>().color = starInfo.starColorList[i % starInfo.starColorList.Length];

			var bgObject = star.GetComponent<BaseBGObject>();
            bgObject.moveSpeed = -(averageStarSpeed * Random.value + minStarSpeed);
            // TODO: check
            starInfo.callback = new BaseBGObjectEvent();
            bgObject.InitializeObject(starInfo.callback);

			star.transform.SetParent(transform);
        }
    }

// TODO: merge two callback
    public void FGStarEndAction(BaseBGObject bgObject)
    {
        int totalActiveCount = fgStarInfo.starPoolList.Sum(x => x.GetActiveObjectCount());

        for (int i = 0; i < fgStarInfo.maxStarCount - totalActiveCount; ++i)
        {
            MakeStars(fgStarInfo);
        }
    }

    public void BGStarEndAction(BaseBGObject bgObject)
    {
        var totalActiveCount = bgStarInfo.starPoolList.Sum(x => x.GetActiveObjectCount());

        for (int i = 0; i < bgStarInfo.maxStarCount - totalActiveCount; ++i)
        {
            MakeStars(bgStarInfo);
        }
    }
}