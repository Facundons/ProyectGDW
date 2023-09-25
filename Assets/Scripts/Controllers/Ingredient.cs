using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    [SerializeField] private GameObject trigger;

    private void Start()
    {
        UiController.onNextLvlClick += DestroyThisGO;
        var triggerGO = Instantiate(trigger);
        triggerGO.transform.SetParent(this.transform);
        triggerGO.transform.localPosition = new Vector3(0, 0.004f, 0); 
    }

    private void DestroyThisGO()
    {
        UiController.onNextLvlClick -= DestroyThisGO;
        Destroy(gameObject);
        if (gameObject.transform.GetChild(0).gameObject != null) Destroy(gameObject.transform.GetChild(0).gameObject);      
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (gameObject != null)
        {
            gameObject.transform.GetChild(0).GetComponent<Trigger>().TriggerFlag();
        }       
    }
}
