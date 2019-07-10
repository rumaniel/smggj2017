using UnityEngine;
using System.Collections.Generic;

public class BackgroundController : MonoBehaviour
{
    public float bgDeltaTime;
	public GameObject[] BGObjects;

	Queue<GameObject> availableObjects = new Queue<GameObject>();

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
		EnqueueObjects();

    	if (availableObjects.Count == 0) return;

		var planet = availableObjects.Dequeue();
		planet.GetComponent<BGObject>().isMoving = true;
	}

	private void EnqueueObjects()
	{
		foreach (var bgObject in BGObjects) 
		{
			if (bgObject.transform.position.y < 0
            && !bgObject.GetComponent<BGObject>().isMoving)
			{
				bgObject.GetComponent<BGObject>().ResetPosition();
				availableObjects.Enqueue(bgObject);
			}
		}
	}
}