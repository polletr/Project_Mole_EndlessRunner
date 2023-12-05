using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private int pointsOverTime;

    private int points;
    private int highScore;
    private float fractionalPoints;

    [SerializeField]
    private TMP_Text PointsText;

    [SerializeField]
    private TMP_Text HighScoreText;

    // Start is called before the first frame update
    void Start()
    {
        points = 0;
        fractionalPoints = 0f;
        // Load the high score from PlayerPrefs
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateHighScoreText();
    }

    // Update is called once per frame
    void Update()
    {
        // Increase points over time with fractional accumulation
        fractionalPoints += pointsOverTime * Time.deltaTime;

        // Check if fractionalPoints reached a whole number
        if (fractionalPoints >= 1f)
        {
            int wholePoints = Mathf.FloorToInt(fractionalPoints);
            points += wholePoints;
            fractionalPoints -= wholePoints; // Remove the added whole points
        }

        // Update points text
        UpdatePoints();

        // Check and update high score
        CheckHighScore();
    }

   /* public void AddPoints(int foodPoints)
    {
        points += foodPoints;
        UpdatePoints();
    }*/

    public void UpdatePoints()
    {
        PointsText.text = points.ToString();
    }

    void CheckHighScore()
    {
        if (points > highScore)
        {
            // Update the high score and save it to PlayerPrefs
            highScore = points;
            PlayerPrefs.SetInt("HighScore", highScore);
            UpdateHighScoreText();
        }
    }

    void UpdateHighScoreText()
    {
        HighScoreText.text = $"Best: {highScore}";
    }
}