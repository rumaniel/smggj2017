using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

	// This is our healthBar image.
	public Image healthBar;
	// Set our maxHealth value.
	static float maxHealth= 100f;
	// This is public, as we access it from our playerControl script.
	public float curHealth = 0f;
	// Reference to the HealthUIText.
	public Text healthUIText;

	void Start () {
		// Set our curHealth to equal our set maxHealth value.
		curHealth = maxHealth;
		// Update the UI Text.
		healthUIText.text = curHealth.ToString ();
	}

	void Update () {
		// If we ever go over our maxHealth value, cap it at our original maxHealth value.
		if (curHealth > maxHealth) {
			curHealth = maxHealth;
		} 
		// If we go under our minHealth value.
		else if (curHealth < 0) {
			curHealth = 0;
		}
	}

	public void TakeDamage (float damage) {
		// Remove health equal to the 'damage' float called inside the playerControl script.
		curHealth -= damage;
		// Create our new float based on the difference between our curHealth and maxHealth.
		float calcHealth = curHealth / maxHealth;
		// Call our setHealth function.
		SetHealth (calcHealth);
	}

	public void GiveHealth (float health) {
		// Add health equal the the 'health' float called inside the playerControl script.
		curHealth += health;
		// Create our new float based on the difference between our curHealth and maxHealth.
		float calcHealth = curHealth / maxHealth;
		// Call our setHealth function.
		SetHealth (calcHealth);
	}

	void SetHealth(float myhealth) {
		// Increase/Decrease the fill of the healthBar image depending on our curHealth.
		healthBar.fillAmount = myhealth;
		// Update the UI Text.
		healthUIText.text = curHealth.ToString ();
	}
}