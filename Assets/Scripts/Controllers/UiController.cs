using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiController : MonoBehaviour
{
    [SerializeField] private ScoreController scoreController;
    [SerializeField] private TextMeshProUGUI lvlText;
    [SerializeField] private TextMeshProUGUI TotalScoreText;
    [SerializeField] private GameObject endLvlMenu;
    [SerializeField] private GameObject star25;
    [SerializeField] private GameObject star50;
    [SerializeField] private GameObject star75;
    [SerializeField] private TextMeshProUGUI comboText;

    public delegate void OnNextLvlClick();
    public static OnNextLvlClick onNextLvlClick;
    public delegate void OnCombo(float combo);
    public static OnCombo onCombo;
    private int combo = 0;

    private void Awake()
    {
        Trigger.onPerfectPosition += ShowCombo;
        Trigger.onGoodPosition += HideCombo;
        Trigger.onIngredientFall += HideCombo;
    }

    public void SetTextLvl(int lvl, GameObject LvlSandwich)
    {
        lvlText.text = "Lvl " + ++lvl;
    }

    public void CalculateScore(int totalIngredients)
    {
        scoreController.CalculateScoreStep(totalIngredients);
    }

    public void ShowEndScreenLvlScreen()
    {
        StartCoroutine(ScoreCountUp(scoreController.GetScore()));
    }

    private IEnumerator ScoreCountUp(int targetNumber)
    {
        yield return new WaitForSeconds(3f);
        scoreController.SetActiveScoreText(false);
        endLvlMenu.SetActive(true);
        for (int i = 0; i <= targetNumber; i++)
        {
            if(i == 25)
            {

                star25.SetActive(true);
                StarInAndOutAnimAnimation(star25);       
            }
            if (i == 50)
            {
                star50.SetActive(true);
                StarInAndOutAnimAnimation(star50);
            }
            if (i == 75)
            {
                star75.SetActive(true);
                StarInAndOutAnimAnimation(star75);
            }
            TotalScoreText.text = i + "%";
            yield return new WaitForSeconds(0.05f); 
        }
        yield return new WaitForSeconds(0.5f);
        endLvlMenu.transform.GetChild(1).gameObject.SetActive(true);
    }


    private void StarInAndOutAnimAnimation(GameObject Star)
    {
        var originalScale = Star.transform.localScale;
        var originalRotation = Star.transform.localEulerAngles;

        LeanTween.sequence()
            .append(LeanTween.scale(Star.gameObject, originalScale * 1.2f, 0.1f).setEase(LeanTweenType.easeOutQuad))
            .append(LeanTween.rotate(Star.gameObject, originalRotation + new Vector3(0f, 0f, 15f), 0.1f).setEase(LeanTweenType.easeOutQuad))
            .append(LeanTween.scale(Star.gameObject, originalScale, 0.1f).setEase(LeanTweenType.easeInQuad))
            .append(LeanTween.rotate(Star.gameObject, originalRotation, 0.1f).setEase(LeanTweenType.easeInQuad));
    }

    public void NextLvlClick()
    {
        combo = 0;
        comboText.gameObject.SetActive(false);
        star25.SetActive(false);
        star50.SetActive(false);
        star75.SetActive(false);
        scoreController.SetActiveScoreText(true);
        endLvlMenu.SetActive(false);   
        scoreController.ResetScore();
        endLvlMenu.transform.GetChild(1).gameObject.SetActive(false);
        onNextLvlClick?.Invoke();
    }

    public int GetScore()
    {
        return scoreController.GetScore();
    }

    private void ShowCombo()
    {
        combo++;
        if (combo != 1) StartCoroutine(ZoomAndTwistComboText());
    }

    private void HideCombo()
    {
        comboText.gameObject.SetActive(false);
        combo = 0;
    }

    private IEnumerator ZoomAndTwistComboText()
    {
        float zoomValue;
        if (combo == 2) zoomValue = 0.6f;
        else if (combo == 3) zoomValue = 0.8f;
        else zoomValue = 1f;
        comboText.text = "COMBO" + combo;
        comboText.color = new Color(Random.value, Random.value, Random.value);
        comboText.gameObject.SetActive(true);
        var zoomTween = LeanTween.scale(comboText.gameObject, Vector3.one * 2f * zoomValue, 0.5f).setEase(LeanTweenType.easeInOutCubic).setLoopPingPong();
        var twistTween = LeanTween.rotate(comboText.gameObject, new Vector3(0f, 0f, Random.Range(-45, +45)), 0.5f).setEase(LeanTweenType.easeInOutCubic).setLoopPingPong();
        onCombo?.Invoke(combo);
        yield return new WaitForSeconds(1f);
        LeanTween.cancel(zoomTween.uniqueId);
        LeanTween.cancel(twistTween.uniqueId);
    }




   
}
