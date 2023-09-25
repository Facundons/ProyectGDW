using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    [SerializeField] private GameState currentState;
    [SerializeField] private LvlController lvlController;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private IngredientController ingredientController;
    [SerializeField] private UiController uiController;
    [SerializeField] private PendlumController pendlumController;
    [SerializeField] private PersonController personController;
    private int lvlNumber;

    private void Start()
    {
        CameraController.onIngredientReachingTheTop += MoveElements;
        IngredientController.onLvlComplete += LvlComplete;
        UiController.onNextLvlClick += SetNextLvl;
        lvlNumber = 0;
        SetStartLvl();
    }

    IEnumerator ZoomOutAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        cameraController.ZoomCameraOut();
        lvlController.MoveLvlSandwichToTop();
    }

    private void SetStartLvl()
    {
        lvlController.ShowLvl(lvlNumber);
        ingredientController.SetCurentLvl(lvlController.getCurrentLvl());
        uiController.SetTextLvl(lvlNumber, lvlController.getCurrentLvl());
        uiController.CalculateScore(ingredientController.GetCurrentIngridientNumber());
        StartCoroutine(ZoomOutAfterTime(2.0f));
    }

    private void LvlComplete()
    {
        pendlumController.MoveYPendlum(5.5f);
        personController.ShowEmoji(uiController.GetScore());
        uiController.ShowEndScreenLvlScreen();
    }

    private void MoveElements()
    {
        pendlumController.MoveYPendlum(0.5f);
        lvlController.MoveLvlSandwich();
    }

    private void SetNextLvl()
    { 
        cameraController.ZoomCameraIn();   
        pendlumController.ResetPendlumForLvlStart();
        lvlController.DisableLastSandwichLvlOnUi();
        lvlNumber++;
        SetStartLvl();
    }
}
