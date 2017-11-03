using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameScore : MonoBehaviour {

	Text scoreTextUI;
	int score;

	public int Score {
		get {
			return this.score;
		} set {
			this.score = value;
			UpdateScoreTextUI();
		}
	}

	void Start () {
		score = 0;
		scoreTextUI = GetComponent<Text> ();
	}

	void UpdateScoreTextUI () {
		string scoreStr = string.Format ("{0:0000000}", score);
		scoreTextUI.text = scoreStr;
	}
}