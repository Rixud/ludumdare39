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
    private PlayerMotor playerM;
    public Text scoreText;

	// Use this for initialization
	void Start () {
        playerM = GetComponent<PlayerMotor>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!playerM.GetDeadFlag())
        {
            if (score >= scoreNeededNextLevel)
                NextLevel();

            score += Time.deltaTime * actuaDifficultyLevel;
            scoreText.text = "Score: " + ((int)score).ToString();
        }
        else
        {
            
        }
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
