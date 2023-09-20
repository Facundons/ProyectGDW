using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientController : MonoBehaviour
{
    [SerializeField] private GameObject[] ingredientPrefabs;
    private GameObject currentIngredient;

    void Update()
    {
        // Check for player input to release the ingredient
        if (Input.GetMouseButtonDown(0))
        {
     
            ReleaseIngredient();
        }
    }

    // Spawn a new ingredient and attach it to the spawner
    void SpawnIngredient()
    {
        int randomIndex = Random.Range(0, ingredientPrefabs.Length);
        currentIngredient = Instantiate(ingredientPrefabs[randomIndex]);
        currentIngredient.transform.position = transform.position;
    }

    // Release the current ingredient and spawn a new one
    void ReleaseIngredient()
    {
        // Detach the ingredient from the spawner
       // currentIngredient.transform.parent = null;

        // Optionally, you can add additional logic here, like applying physics to the released ingredient

        // Spawn a new ingredient
        SpawnIngredient();
    }

}
