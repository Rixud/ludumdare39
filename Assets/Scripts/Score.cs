using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    private float score = 0.0f;

    private int actuaDifficultyLevel = 1;
    private int maxDifficultyLevel = 10;
    private int scoreNeededNextLevel = 15;
    private float speedIncremetPerLevel = 5.0f;

    public Text scoreText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (score >= scoreNeededNextLevel)
            NextLevel();

        score += Time.deltaTime * actuaDifficultyLevel;
        scoreText.text = ((int)score).ToString();
	}

    void NextLevel()
    {
        if (actuaDifficultyLevel == maxDifficultyLevel)
            return;
        scoreNeededNextLevel = scoreNeededNextLevel + 2*scoreNeededNextLevel;
        actuaDifficultyLevel++;
        GetComponent<PlayerMotor>().SetLevelSpeed(speedIncremetPerLevel);

        Debug.Log(actuaDifficultyLevel);
    }
}
