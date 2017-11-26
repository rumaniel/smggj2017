using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FloatingCombatText : MonoBehaviour {

	public bool isHazard;
	public TextMesh CBTPrefab; // This is the 'Floating Combat Text' prefab.

	int displayValue;
	
	void Start () 
	{
		// Check if we are a hazard or an enemy ship and access the script accordingly.
		if (isHazard == false) {
			displayValue = GetComponent<EnemyBase> ().pointsValue;
		} else
			displayValue = GetComponent<HazardBase> ().pointsValue;
	}

	public void ShowCombatText () 
	{
		/*Debug.Log ("Combat Text: " + displayValue + " !");*/

		// Create the combat text prefab and set the details.
		TextMesh combatText = (TextMesh)Instantiate (CBTPrefab);
		combatText.color = Color.yellow;
		combatText.text = displayValue.ToString ();
		combatText.transform.position = transform.position;
	}
}