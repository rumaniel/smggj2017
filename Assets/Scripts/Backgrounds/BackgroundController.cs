using UnityEngine;
using System.Collections.Generic;

public class BackgroundController : MonoBehaviour
{
    public float bgDeltaTime;
	public BaseBGObject[] BGObjects;

    // TODO: Random pop
	Queue<BaseBGObject> availableObjects = new Queue<BaseBGObject>();

	private void Start()
	{
        foreach (var bgObject in BGObjects)
        {
            availableObjects.Enqueue(bgObject);
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

		var planet = availableObjects.Dequeue() as ManagedBGObject;
        planet.InitializeObject(EnqueueObjects);
	}

    private void EnqueueObjects(BaseBGObject bgObject)
	{
        availableObjects.Enqueue(bgObject);
	}
}