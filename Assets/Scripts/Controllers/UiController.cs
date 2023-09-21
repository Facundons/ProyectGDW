using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiController : MonoBehaviour
{
    [SerializeField] private ScoreController scoreController;
    [SerializeField] private TextMeshProUGUI lvlText;
    private GameObject lvlSandwich;

    public void SetTextLvl(int lvl, GameObject LvlSandwich)
    {
        lvlText.text = "Lvl " + ++lvl;
        lvlSandwich = LvlSandwich;
    }

    public void CalculateScore(int totalIngredients)
    {
        scoreController.CalculateScoreStep(totalIngredients);
    }
     
}
