using UnityEngine;
using System.Collections.Generic;

public class BackgroundController : MonoBehaviour
{
    public float bgDeltaTime;
	public BaseBGObject[] BGObjects;
    public BaseBGObjectEvent callbackEvent;

	public List<BaseBGObject> availableObjects = new List<BaseBGObject>();

	private void Start()
	{
        foreach (var bgObject in BGObjects)
        {
            availableObjects.Add(bgObject);
        }
	}

    private float accumulatedTime = 0f;
    private void Update()
    {
        accumulatedTime += Time.deltaTime;
        if (accumulatedTime > bgDeltaTime)
        {
            accumulatedTime = 0f;
            MoveObjectDown();
        }
    }

    private void MoveObjectDown()
	{
    	if (availableObjects.Count == 0) return;

        // TODO : linq?
		var planet = RandomPopObject<BaseBGObject>(availableObjects);

        Debug.Log(planet.name + ":" + planet == null);

        planet.InitializeObject(callbackEvent);
	}

    private T RandomPopObject<T>(List<T> sourceList)
    {
        int index = Random.Range(0, sourceList.Count);
        T result = sourceList[index];
        sourceList.RemoveAt(index);

        return result;
    }

    public void ReturnToPoolObjects(BaseBGObject bgObject)
	{
        availableObjects.Add(bgObject);
	}
}