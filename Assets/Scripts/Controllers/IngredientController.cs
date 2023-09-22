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
    private List<GameObject> ingredientsReleased = new List<GameObject>();
    public delegate void OnLvlComplete();
    public static OnLvlComplete onLvlComplete;

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
            ingredientsReleased.Add(currentIngredient);
            currentIngredientNumber++;
        }
        else
        {
            onLvlComplete?.Invoke();
            StartCoroutine(MoveIngridientsToPerson(0f));
        }
    }


    IEnumerator MoveIngridientsToPerson(float time) {

        yield return new WaitForSeconds(time);
        foreach (GameObject ingredient in ingredientsReleased)
        {  
            if (ingredient != null) {

                StartCoroutine(MoveObject(ingredient ,1 ));
            }
        }
    }


    IEnumerator MoveObject(GameObject ingredient, float time)
    {
        ingredient.GetComponent<Rigidbody>().isKinematic = true;
        var SandwichDestination = new Vector3(ingredient.transform.position.x, ingredient.transform.position.y + 2.5f, 3.38f);
        Vector3 startPosition = ingredient.transform.position;
        float startTime = Time.time;
        float endTime = startTime + time;

        while (Time.time < endTime)
        {
            float t = (Time.time - startTime) / time;
            ingredient.transform.position = Vector3.Lerp(startPosition, SandwichDestination, t);
            yield return null;
        }

        transform.position = SandwichDestination;
    }

    void ReleaseIngredient()
    {
        
        SpawnIngredient();
    }

}
