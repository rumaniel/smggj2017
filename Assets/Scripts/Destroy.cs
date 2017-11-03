using UnityEngine;
using System.Collections;

public class Destroy : MonoBehaviour {

	public float destroyTime;

	void Start ()
	{
		// If the destroyTime float is NOT empty, it will run the DestroyGameObject function after the specified time, else it will do nothing unless called via an animation.
		if (destroyTime != 0) {
			Invoke ("DestroyGameObject", destroyTime);
		} else
			return;
	}

	// This function will destroy the gameObject is is attached to, this is called in the animation clip for that gameObject.
	void DestroyGameObject() {
		Destroy (gameObject);
	}
}