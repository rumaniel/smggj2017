using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeCounter : MonoBehaviour {

	Text timeUI; // Reference to the time counter UI text.

	float startTime; // The time when the user clicks play.
	float elapsedTime; // The time elapsed after the user clicks play.
	bool startCounter; // Flag to start the time counter.

	int minutes;
	int seconds;

	// Use this for initialization
	void Start () 
	{
		// Get the Text UI component from this gameObject.
		timeUI = GetComponent<Text> ();
		// Start the timer.
		startTime = Time.time;
		startCounter = true;
	}

	// Stop the time counter.
	public void StopTimeCounter()
	{
		startCounter = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (startCounter) 
		{
			// Compute the elapsed time
			elapsedTime = Time.time - startTime;

			minutes = (int)elapsedTime / 60; // Get the minutes
			seconds = (int)elapsedTime % 60; // Get the seconds.

			// Update the time counter UI text
			timeUI.text = string.Format("{0:00}:{1:00}", minutes, seconds);
		}
	}
}
