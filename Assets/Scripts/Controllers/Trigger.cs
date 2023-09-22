using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public ParticleSystem particleSystemPrefab;
    public delegate void OnPerfectPosition();
    public static OnPerfectPosition onPerfectPosition;
    public delegate void OnGoodPosition();
    public static OnGoodPosition onGoodPosition;
    public delegate void OnIngredientFall();
    public static OnIngredientFall onIngredientFall;
    private int triggerFlag;
    public bool IsPlateTrigger;
        
    private void Awake()
    {
        triggerFlag = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsPlateTrigger) return;
        if (other.gameObject.name.Contains("Trigger") && other.isTrigger && triggerFlag == 0)
        {
            ParticleSystem particleInstance = Instantiate(particleSystemPrefab, transform.position, Quaternion.identity);
            particleSystemPrefab.Play();
            onPerfectPosition?.Invoke();
            triggerFlag++;
        }
        if (other.gameObject.name.Contains("IndredientDropCollider") && other.isTrigger)
        {
            onIngredientFall?.Invoke();
            Destroy(gameObject.transform.parent.gameObject);
        }
       
       
    }

    public void TriggerFlag()
    {
        if (IsPlateTrigger) return;
        if (triggerFlag == 0)
        {
            onGoodPosition?.Invoke();
            triggerFlag++;
        }
    }
}
