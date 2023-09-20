using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    [SerializeField] private GameObject trigger;

    private void Start()
    {
        var triggerGO = Instantiate(trigger);
        triggerGO.transform.SetParent(this.transform);
        triggerGO.transform.localPosition = new Vector3(0, 0.004f, 0); 
    }

}
