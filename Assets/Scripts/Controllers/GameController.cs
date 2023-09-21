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
    private int lvlNumber;

    private void Start()
    {
        lvlNumber = 0;
        SetStartLvl();
    }

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        cameraController.ZoomCameraOut();
       
    }

    private void SetStartLvl()
    {
        lvlController.ShowLvl(lvlNumber);
        ingredientController.SetCurentLvl(lvlController.getCurrentLvl());
        uiController.SetTextLvl(lvlNumber, lvlController.getCurrentLvl());
        uiController.CalculateScore(ingredientController.GetCurrentIngridientNumber());
        StartCoroutine(ExecuteAfterTime(5.0f));
    }


}
