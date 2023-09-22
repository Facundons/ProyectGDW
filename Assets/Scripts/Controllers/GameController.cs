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

    private int lvlNumber;

    private void Start()
    {
        CameraController.onIngredientReachingTheTop += MoveElements;
        IngredientController.onLvlComplete += LvlComplete;
        lvlNumber = 1;
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
        

    }

    private void MoveElements()
    {
        pendlumController.MovePendlumUp();
        lvlController.MoveLvlSandwich();
    }

}
