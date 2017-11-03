using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ComboText : MonoBehaviour {

	public GameObject comboText;

	public Text comboUIText;
	public int comboCount;
	float comboTimer;

	void Update () 
	{
		// This controls the combo timer.
		if (comboTimer > 0f) {
			ComboTimerStart ();
		} else if (comboTimer < 0f) {
			ComboTimerStop ();
		}
	}

	// This is called from inside the enemyBaseControl and hazardBase scripts whenever they are destroyed.
	public void AwardKill ()
	{
		// Increase the combo count by 1.
		comboCount ++;
		// This is the amount of time you have to make a kill before the combo ends, adjust accordingly.
		comboTimer = 2.5f;
		// Update the combo count UI Text.
		comboUIText.text = comboCount.ToString ();
	}
	
	void ComboTimerStart ()
	{
		// Start the timer.
		comboTimer -= Time.deltaTime;
		// Show the combo text gameObject.
		comboText.SetActive (true);
		// Increase the combo count.
		PlayerPrefs.SetInt ("Combo_Counter", comboCount);
	}
	
	void ComboTimerStop ()
	{
		// Stop the timer and reset UI elements.
		comboTimer = 0f;
		// Hide the combo text gameObject.
		comboText.SetActive (false);
		// Set combo count back to 0.
		comboCount = 0;
		PlayerPrefs.SetInt ("Combo_Counter", comboCount);
	}
}