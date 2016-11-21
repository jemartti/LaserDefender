using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreKeeper : MonoBehaviour
{
	public static int score = 0;
	private Text scoreLabel;

	void Start ()
	{
		scoreLabel = GetComponent<Text> ();
		Reset ();
	}

	public void Score (int points)
	{
		score += points;
		scoreLabel.text = score.ToString ();
	}

	public static void Reset ()
	{
		score = 0;
	}
}
