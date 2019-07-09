using UnityEngine;
using System.Collections.Generic;

public class BackgroundController : MonoBehaviour
{

	public GameObject[] BGObjects;

	Queue<GameObject> availableObjects = new Queue<GameObject>();

	void Start () 
	{
        foreach (var bgObject in BGObjects)
        {
            availableObjects.Enqueue(bgObject);
        }

		InvokeRepeating("MoveObjectDown", 0, 20f);	
	}

	void MoveObjectDown()
	{
		EnqueueObjects();

    	if (availableObjects.Count == 0) return;

		GameObject aPlanet = availableObjects.Dequeue();
		aPlanet.GetComponent<BGObject>().isMoving = true;
	}

	void EnqueueObjects()
	{
		foreach (GameObject bgObject in BGObjects) 
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