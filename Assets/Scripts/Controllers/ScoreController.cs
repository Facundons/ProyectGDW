using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int score = 0;
    private int totalStepScore;

    private void Start()
    {
        Trigger.onPerfectPosition += SumPerfectPositionScore;
        Trigger.onGoodPosition += SumGoodPositionScore;

    }

    private void SumPerfectPositionScore()
    {
        UpdateScoreText(totalStepScore);
    }

    private void SumGoodPositionScore()
    {
        UpdateScoreText(totalStepScore/2);
    }

    public void CalculateScoreStep(int totalIngredients)
    {
        totalStepScore = 100 / totalIngredients;
    }

    private void UpdateScoreText(int scoreSum)
    {
        score += scoreSum;
        if (score >= 98) scoreText.text = "100%";
        else scoreText.text = score + "%";
    }

}
