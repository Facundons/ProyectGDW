using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public ParticleSystem particleSystemPrefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Trigger") && other.isTrigger)
        {
            ParticleSystem particleInstance = Instantiate(particleSystemPrefab, transform.position, Quaternion.identity);
            particleSystemPrefab.Play();
        }
    }
}
