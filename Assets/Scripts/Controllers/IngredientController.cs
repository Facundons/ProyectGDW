using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientController : MonoBehaviour
{
    [SerializeField] private GameObject[] ingredientPrefabs;
    private GameObject currentIngredient;
    private List<GameObject> currentLvlIngridients = new List<GameObject>();
    private int amountOfIngredientsOnLvl;
    private int currentIngredientNumber = 0;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
     
            ReleaseIngredient();
        }
    }

    public int GetCurrentIngridientNumber()
    {
        return amountOfIngredientsOnLvl;
    }

    public void SetCurentLvl(GameObject currentLvl)
    {
        currentLvlIngridients.Clear();
        amountOfIngredientsOnLvl = currentLvl.transform.childCount;
        currentIngredientNumber = 0;

        for (int i = 0; i < amountOfIngredientsOnLvl; i++)
        {

            foreach (GameObject ingredient in ingredientPrefabs)
            {
                if (ingredient.name == currentLvl.transform.GetChild(i).name)
                {
                    currentLvlIngridients.Add(ingredient);
                }
            }
        }

    }

    void SpawnIngredient()
    {
        if (currentIngredientNumber < amountOfIngredientsOnLvl)
        {
            currentIngredient = Instantiate(currentLvlIngridients[currentIngredientNumber]);
            currentIngredient.transform.position = transform.position;
            currentIngredientNumber++;
        }
    }

    void ReleaseIngredient()
    {
        
        SpawnIngredient();
    }

}
